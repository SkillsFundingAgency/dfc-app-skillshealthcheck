

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Xsl;
    using System.Security;
    using DFC.SkillsCentral.Api.Domain.Models;
    using DfE.SkillsCentral.Api.Application.Interfaces.Models;
    using System.Xml.Linq;
    using System.Runtime.Serialization;

    public static class GenericOpenOfficeXMLFormatter
    {

        public static byte[] FormatDocumentWithATemplate(SkillsDocument document, string xsltTemplateName)
        {
            XmlDocument documentXml = GetSkillsDocumentXml(document);
            XslTransformation xslt = GetXslTransformationObjects(xsltTemplateName);

            XmlDocument processedDoc = OpenXMLDocumentBuilder.InterpretSkillsDocument(documentXml, xslt);

            string wordTemplateFileName = OpenXMLDocumentBuilder.GetWordTemplateName(processedDoc);
            byte[] wordDocumentTemplate = GetWordDocumentTemplate(wordTemplateFileName);

            byte[] processedWordDocument = OpenXMLDocumentBuilder.BuildWordDocumentFromTemplate(processedDoc, wordDocumentTemplate);

            return processedWordDocument;
        }

        private static byte[] GetWordDocumentTemplate(string templateName)
        {
            byte[] result;
            if (Directory.Exists("Templates"))
            {
                string filePath = Path.Combine("Templates", templateName);
                if (File.Exists(filePath))
                {
                    result = File.ReadAllBytes(filePath);
                }
                else
                {
                    throw new Exception(string.Format("Unable to find the word template. Path {0}", filePath));
                }
            }
            else
            {
                throw new Exception(string.Format("Unable to find the word document configuration directory. Path {0}", "Templates"));
            }

            return result;
        }

        private static XslTransformation GetXslTransformationObjects(string xsltTemplateName)
        {
            XslTransformation result = new XslTransformation();
            if (Directory.Exists("Templates"))
            {
                string filePath = Path.Combine("Templates", xsltTemplateName + ".xsl");
                string extensionObjectConfigurationFilePath = Path.Combine("Templates", xsltTemplateName + "_Extensions.txt");

                if (File.Exists(filePath) && File.Exists(extensionObjectConfigurationFilePath))
                {
                    result.TransformationFile = GetTransformationFileObjectFromByteArr(File.ReadAllBytes(filePath));
                    var xsltArgs = new XsltArgumentList();
                    xsltArgs.AddExtensionObject("urn:https://nationalcareers.service.gov.uk", new SkillsReportXsltExtension());
                    result.TransformationArgumentList = xsltArgs;
                }
                else
                {
                    throw new Exception(string.Format("Unable to find the XSLT file. Path {0} Path2: {1}", filePath, extensionObjectConfigurationFilePath));
                }
            }
            else
            {
                throw new Exception(string.Format("Unable to find the XSLT configuration directory. Path {0}", "Templates"));
            }

            return result;
        }

        private static XsltArgumentList GetTransformationArgumentList(string typeNames)
        {
            XsltArgumentList result = new XsltArgumentList();

            if (!string.IsNullOrEmpty(typeNames))
            {
                string[] typeNamesArray = typeNames.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string typeName in typeNamesArray)
                {
                    Type typeSpecified = Type.GetType(typeName);
                    if (typeSpecified != null)
                    {
                        object extensionObject = Activator.CreateInstance(typeSpecified);
                        result.AddExtensionObject(string.Format("{2}{0}#{1}", typeSpecified.Module.Name, typeSpecified.Name, "DocumentsFormatters"), extensionObject);
                    }
                    else
                    {
                        throw new ArgumentException(string.Format("Unable to load specified extension type: {0}", typeSpecified));
                    }
                }
            }

            return result;
        }

        private static XslCompiledTransform GetTransformationFileObjectFromByteArr(byte[] byteArray)
        {
            XslCompiledTransform transformationFile = null;
            using (MemoryStream mem = new MemoryStream())
            {
                mem.Write(byteArray, 0, (int)byteArray.Length);

                mem.Seek(0, SeekOrigin.Begin);
                XmlReader r = XmlReader.Create(mem);
#if DEBUG
                transformationFile = new XslCompiledTransform(true);
#else
                        transformationFile = new XslCompiledTransform(false);
#endif
                transformationFile.Load(r);
            }

            return transformationFile;
        }

        private static XmlDocument GetSkillsDocumentXml(SkillsDocument document)
        {
            string xmlString;
            XmlDocument doc = new XmlDocument();
            foreach (var key in document.DataValueKeys.Keys)
            {
                try
                {
                    XmlDocument escapeDocItems = new XmlDocument();
                    var ourString = $"{document.DataValueKeys[key]}";
                    escapeDocItems.LoadXml(ourString);
                    EscapeSpecialCharacters(escapeDocItems.ChildNodes);
                    document.DataValueKeys[key] = escapeDocItems.OuterXml;
                }
                catch (Exception ex)
                {
                    string Val = SecurityElement.Escape(document.DataValueKeys[key]);

                    document.DataValueKeys[key] = Val.Replace("\n", "</w:t><w:br/><w:t>");
                }
            }

            System.Runtime.Serialization.DataContractSerializer x = new System.Runtime.Serialization.DataContractSerializer(document.GetType());
            StringWriter sw = new StringWriter();
            using (XmlTextWriter writer = new XmlTextWriter(sw))
            {
                x.WriteObject(writer, document);
                writer.Flush();
                xmlString = sw.ToString();
            }

            xmlString = xmlString.Replace("d2p1:Key", "Title");
            xmlString = xmlString.Replace("TitleValueOfstringstring", "SkillsDocumentDataValue");
            xmlString = xmlString.Replace("d2p1:Value", "Value");
            xmlString = xmlString.Replace(" xmlns:d2p1=\"http://schemas.microsoft.com/2003/10/Serialization/Arrays\"", "");

            doc.LoadXml(xmlString);
          

            return doc;
        }

        private static void EscapeSpecialCharacters(XmlNodeList nodes)
        {
            foreach (XmlNode node in nodes)
            {
                if (node.HasChildNodes)
                {
                    EscapeSpecialCharacters(node.ChildNodes);
                }
                else if (node.NodeType != XmlNodeType.XmlDeclaration)
                {
                    string Val = SecurityElement.Escape(node.InnerText);
                    node.InnerText = Val.Replace("\n", "</w:t><w:br/><w:t>");
                }
            }
        }

    }
}

