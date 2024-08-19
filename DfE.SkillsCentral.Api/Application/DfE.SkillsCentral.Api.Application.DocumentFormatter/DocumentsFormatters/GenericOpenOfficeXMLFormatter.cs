

using DfE.SkillsCentral.Api.Application.DocumentFormatter.Resources;

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
    using DfE.SkillsCentral.Api.Application.Interfaces.Repositories;

    public static class GenericOpenOfficeXMLFormatter
    {

        public static byte[] FormatDocumentWithATemplate(SkillsDocument document, string xsltTemplateName, IJobFamiliesRepository jobFamiliesRepository)
        {
            XmlDocument documentXml = GetSkillsDocumentXml(document);
            XslTransformation xslt = GetXslTransformationObjects(xsltTemplateName, jobFamiliesRepository);

            XmlDocument processedDoc = OpenXMLDocumentBuilder.InterpretSkillsDocument(documentXml, xslt);

            string wordTemplateFileName = OpenXMLDocumentBuilder.GetWordTemplateName(processedDoc);
            byte[] wordDocumentTemplate = GetWordDocumentTemplate(wordTemplateFileName);

            byte[] processedWordDocument = OpenXMLDocumentBuilder.BuildWordDocumentFromTemplate(processedDoc, wordDocumentTemplate);

            return processedWordDocument;
        }

        private static byte[] GetWordDocumentTemplate(string templateName)
        {
            var bytes = ResourceHelper.GetResource(templateName);

            return bytes.Length == 0
                ? throw new Exception($"Unable to load {templateName} from local resources")
                : bytes;
        }

        private static XslTransformation GetXslTransformationObjects(string xsltTemplateName, IJobFamiliesRepository jobFamiliesRepository)
        {
            var result = new XslTransformation();
            var bytes = ResourceHelper.GetResource("SHCFullReport.xsl");

            if (bytes.Length > 0)
            {
                var xsltTemplate = Encoding.UTF8.GetString(bytes);
                result.TransformationFile =
                    GetTransformationFileObjectFromByteArr(Encoding.UTF8.GetBytes(xsltTemplate));
                var xsltArgs = new XsltArgumentList();
                xsltArgs.AddExtensionObject("urn:https://nationalcareers.service.gov.uk",
                    new SkillsReportXsltExtension(jobFamiliesRepository));
                result.TransformationArgumentList = xsltArgs;
            }
            else
            {
                throw new Exception("Unable to load XSLT Template from resources");
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


                transformationFile = new XslCompiledTransform(false);

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
            xmlString = xmlString.Replace("xmlns:i", "xmlns:xsd");
            xmlString = xmlString.Replace("xmlns=", "xmlns:xsi=");
            xmlString = xmlString.Replace("i:nil", "xsi:nil");
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

