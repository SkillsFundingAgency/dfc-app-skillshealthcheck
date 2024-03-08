

namespace DfE.SkillsCentral.Api.Application.Services.DocumentsFormatters
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Xsl;
    using System.Security;
    using DFC.SkillsCentral.Api.Domain.Models;
    using DfE.SkillsCentral.Api.Application.Interfaces.Models;

    public static class GenericOpenOfficeXMLFormatter
    {
        /*
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
                    result.TransformationArgumentList = GetTransformationArgumentList(File.ReadAllText(extensionObjectConfigurationFilePath));
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
            XmlDocument doc = null;

            // TODO: Need update to GenerateXML from SkillsDocument for Word generation 
            //foreach (var dv in document.DataValueKeys)
            //{
            //    try
            //    {
            //        XmlDocument escapeDocItems = new XmlDocument();
            //        escapeDocItems.LoadXml(dv.Value);
            //        EscapeSpecialCharacters(escapeDocItems.ChildNodes);
            //        dv.Value = escapeDocItems.OuterXml;
            //    }
            //    catch (Exception ex)
            //    {
            //        string Val = SecurityElement.Escape(dv.Value);

            //        dv.Value = Val.Replace("\n", "</w:t><w:br/><w:t>");
            //    }
            //}

            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(document.GetType());
            StringBuilder builder = new StringBuilder();
            using (TextWriter writer = new StringWriter(builder))
            {
                x.Serialize(writer, document);
                doc = new XmlDocument();
                doc.LoadXml(builder.ToString());
            }

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
        */
    }
}
