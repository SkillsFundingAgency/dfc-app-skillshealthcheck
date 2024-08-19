namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using DfE.SkillsCentral.Api.Application.Interfaces.Models;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Wordprocessing;
    //using IMS.SkillsCentral.HelperFunctions;
    using static System.Net.Mime.MediaTypeNames;
    using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

    internal static class OpenXMLDocumentBuilder
    {

        public const string CommandNameConditionalInclude = "ConditionalInclude";

        public const string CommandNameSubstituteContent = "SubstituteContent";

        public const string CommandNameSubstituteProperty = "SubstituteProperty";

        public const string CommandNameSubstituteConditionalProperty = "SCP";

        public const string WordTemplateNameXPath = "/InterpretedSkillsDocument/WordTemplateName";

        public static byte[] BuildWordDocumentFromTemplate(XmlDocument processedDoc, byte[] wordDocumentTemplate)
        {
            byte[] processedDocument = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(wordDocumentTemplate, 0, (int)wordDocumentTemplate.Length);
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(memoryStream, true))
                {
                    DuplicateSdtElementsBasedonInterpretedDocumentXML(wordDoc, processedDoc);
                    ConditionallyRemoveSdtElements(wordDoc, processedDoc);
                    SubstituteContentAndProperty(wordDoc, processedDoc);
                    DeleteContentControls(wordDoc);
                }

                processedDocument = memoryStream.ToArray();
            }

            return processedDocument;
        }

        public static string GetWordTemplateName(XmlDocument processedDoc)
        {
            return processedDoc.SelectSingleNode(WordTemplateNameXPath).InnerText.Trim();
        }

        public static XmlDocument InterpretSkillsDocument(XmlDocument documentXml, XslTransformation interpretationXslt)
        {
            XmlDocument result = new XmlDocument();
            using (XmlWriter writer = result.CreateNavigator().AppendChild())
            {
                interpretationXslt.TransformationFile.Transform(documentXml, interpretationXslt.TransformationArgumentList, writer);
            }

            return result;
        }

        private static void DuplicateSdtElementsBasedonInterpretedDocumentXML(WordprocessingDocument wordDoc, XmlDocument processedDoc)
        {
            var allsdtElements = (from element in wordDoc.MainDocumentPart.Document.Descendants<SdtElement>()
                                  select new
                                  {
                                      sdtElement = element,
                                      xPath = GetContainerBasedXPathAllNodes(element),
                                      currentNumberOfSibling = GetSdtElementSiblingCount(element)
                                  }).OrderBy(element => element.xPath).ToList();

            for (int j = 0; j < allsdtElements.Count(); j++)
            {
                var element = allsdtElements[j];
                XmlNodeList nodes = processedDoc.SelectNodes(element.xPath);

                if (nodes.Count > (element.currentNumberOfSibling + 1))
                {
                    int updatedNumberOfSibling = GetSdtElementSiblingCount(element.sdtElement);
                    if (nodes.Count > (updatedNumberOfSibling + 1))
                    {
                        int replicationCount = nodes.Count - (updatedNumberOfSibling + 1);
                        SdtElement prevElement = element.sdtElement;
                        for (int i = 0; i < replicationCount; i++)
                        {
                            SdtElement newElement = (SdtElement)prevElement.Clone();
                            prevElement.InsertAfterSelf<SdtElement>(newElement);

                            allsdtElements.Add(new
                            {
                                sdtElement = newElement,
                                xPath = GetContainerBasedXPathAllNodes(newElement),
                                currentNumberOfSibling = GetSdtElementSiblingCount(newElement)
                            });

                            var allsdtChildElements = (from childElement in newElement.Descendants<SdtElement>()
                                                       select new
                                                       {
                                                           sdtElement = childElement,
                                                           xPath = GetContainerBasedXPathAllNodes(childElement),
                                                           currentNumberOfSibling = GetSdtElementSiblingCount(childElement)
                                                       }).OrderBy(childElement => childElement.xPath).ToList();

                            for (int k = 0; k < allsdtChildElements.Count(); k++)
                            {
                                allsdtElements.Add(new
                                {
                                    sdtElement = allsdtChildElements[k].sdtElement,
                                    xPath = allsdtChildElements[k].xPath,
                                    currentNumberOfSibling = allsdtChildElements[k].currentNumberOfSibling
                                });
                            }

                            prevElement = (SdtElement)newElement;
                        }
                    }
                }
            }
        }
        private static void ConditionallyRemoveSdtElements(WordprocessingDocument wordDoc, XmlDocument processedDoc)
        {
            var allsdtElements = (from element in wordDoc.MainDocumentPart.Document.Descendants<SdtElement>()
                                  select new
                                  {
                                      sdtElement = element,
                                      xPath = GetContainerBasedXPathAllNodes(element),
                                      currentNumberOfSibling = GetSdtElementSiblingCount(element)
                                  }).OrderBy(element => element.xPath).ToList();

            for (int j = 0; j < allsdtElements.Count(); j++)
            {

                var element = allsdtElements[j];

                var alias = element.sdtElement.SdtProperties.Descendants<SdtAlias>();
                var tag = element.sdtElement.SdtProperties.Descendants<Tag>();

                if (alias.Count() == 1 && tag.Count() == 1)
                {
                    string commandName = alias.First().Val;
                    string commandParameter = tag.First().Val;

                    if (string.Compare(commandName, CommandNameConditionalInclude) == 0)
                    {
                        if (GetAncestorCount(element.sdtElement) > 0)
                        {
                            XmlNode node = processedDoc.SelectSingleNode(GetContainerBasedXPath(element.sdtElement));
                            if (node != null)
                            {
                                List<string> tokens = new List<string>();
                                tokens = ExpressionEvaluator.ExtracTokens(commandParameter);
                                string finalExpression = commandParameter;
                                foreach (string token in tokens)
                                {
                                    string commandValue = GetNodeValueFromProcessedXmlDoc(token, element.sdtElement, processedDoc);
                                    finalExpression = finalExpression.Replace("{" + token + "}", commandValue);
                                }

                                bool showElement = false;

                                    object obj = ExpressionEvaluator.Evaluate(finalExpression.ToLower());
                                    bool.TryParse(obj.ToString(), out showElement);


                                if (!showElement)
                                {
                                    if (element.sdtElement is SdtBlock)
                                    {
                                        if (element.sdtElement.Parent.Elements<SdtBlock>().Count() <= 1)
                                        {
                                            element.sdtElement.InsertBeforeSelf(new Paragraph(new Run(new Text(string.Empty))));
                                        }
                                    }

                                    element.sdtElement.RemoveAllChildren();
                                    element.sdtElement.Remove();
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void SubstituteContentAndProperty(WordprocessingDocument wordDoc, XmlDocument processedDoc)
        {
            var allsdtElements = (from element in wordDoc.MainDocumentPart.Document.Descendants<SdtElement>()
                                  select new
                                  {
                                      sdtElement = element,
                                      xPath = GetContainerBasedXPathAllNodes(element),
                                      currentNumberOfSibling = GetSdtElementSiblingCount(element)
                                  }).OrderBy(element => element.xPath).ToList();

            for (int j = 0; j < allsdtElements.Count(); j++)
            {
                var element = allsdtElements[j];

                var alias = element.sdtElement.SdtProperties.Descendants<SdtAlias>();
                var tag = element.sdtElement.SdtProperties.Descendants<Tag>();

                if (alias.Count() == 1 && tag.Count() == 1)
                {
                    string commandName = alias.First().Val;
                    string commandParameter = tag.First().Val;

                    if (string.Compare(commandName, CommandNameSubstituteProperty) == 0)
                    {
                        XmlNode node = processedDoc.SelectSingleNode(GetContainerBasedXPath(element.sdtElement));
                        SubstituteProperty(element.sdtElement, BuildExpressionContext(node), commandParameter);
                    }
                    else if (string.Compare(commandName, CommandNameSubstituteContent) == 0)
                    {
                        XmlNode node = processedDoc.SelectSingleNode(GetContainerBasedXPath(element.sdtElement));
                        if (node != null && node.ParentNode != null)
                        {
                            SubstituteContent(element.sdtElement, BuildExpressionContext(node), commandParameter);
                        }
                    }
                    else if (commandName.StartsWith(CommandNameSubstituteConditionalProperty))
                    {
                        XmlNode node = processedDoc.SelectSingleNode(GetContainerBasedXPath(element.sdtElement));
                        SustituteConditionalProperty(element.sdtElement, BuildExpressionContext(node), commandParameter, commandName);
                    }
                }
            }
        }

        private static void SubstituteProperty(SdtElement sdtElement, Dictionary<string, string> expressionContext, string commandParameter)
        {
            string[] commandParameters = commandParameter.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
            if (commandParameters.Length == 3)
            {
                foreach (OpenXmlElement element in sdtElement.Descendants())
                {
                    if (string.Compare(element.GetType().FullName, commandParameters[0], true) == 0)
                    {
                        var elementAtributes = element.GetAttributes();
                        for (int i = 0; i < elementAtributes.Count; i++)
                        {
                            var atribute = elementAtributes[i];
                            if (string.Compare(atribute.LocalName, commandParameters[1], true) == 0)
                            {
                                atribute.Value = expressionContext[commandParameters[2]].ToString();
                                element.SetAttribute(atribute);
                            }
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException(string.Format("The format of the command parameter is incorect {0}", string.IsNullOrEmpty(commandParameter) ? "Empty" : commandParameter));
            }
        }

        private static void SustituteConditionalProperty(SdtElement sdtElement, Dictionary<string, string> expressionContext, string commandParameter, string commandName)
        {
            string[] commandNames = commandName.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
            if (commandName.Length < 2)
            {
                throw new ArgumentException(string.Format("The format of the command name is incorect {0} for SustituteConditionalProperty", string.IsNullOrEmpty(commandParameter) ? "Empty" : commandName));
            }

            string[] commandParameters = commandParameter.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
            if (commandParameters.Length >= 4)
            {
                foreach (OpenXmlElement element in sdtElement.Descendants())
                {
                    if (string.Compare(element.GetType().FullName, commandNames[1]) == 0)
                    {
                        var elementAtributes = element.GetAttributes();
                        for (int i = 0; i < elementAtributes.Count; i++)
                        {
                            var atribute = elementAtributes[i];
                            if (string.Compare(atribute.LocalName, commandParameters[0], true) == 0)
                            {
                                List<string> tokens = new List<string>();
                                tokens = ExpressionEvaluator.ExtracTokens(commandParameters[1]);
                                string finalExpression = commandParameters[1];
                                foreach (string token in tokens)
                                {
                                    string commandValue = expressionContext[token];
                                    finalExpression = finalExpression.Replace("{" + token + "}", commandValue);
                                }

                                bool showElement = false;
                                object obj = ExpressionEvaluator.Evaluate(finalExpression.ToLower());
                                bool.TryParse(obj.ToString(), out showElement);
                                if (showElement)
                                {
                                    atribute.Value = expressionContext[commandParameters[2]].ToString();
                                    element.SetAttribute(atribute);
                                }
                                else
                                {
                                    if (commandParameters.Length > 3 && !string.IsNullOrEmpty(commandParameters[3]))
                                    {
                                        atribute.Value = expressionContext[commandParameters[3]].ToString();
                                        element.SetAttribute(atribute);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException(string.Format("The format of the command parameter is incorect {0} for SustituteConditionalProperty", string.IsNullOrEmpty(commandParameter) ? "Empty" : commandParameter));
            }
        }

        private static void SubstituteContent(SdtElement sdtElement, Dictionary<string, string> expressionContext, string commandParameter)
        {
            if (expressionContext.ContainsKey(commandParameter))
            {
                if (sdtElement is SdtBlock)
                {
                    ((SdtBlock)sdtElement).SdtContentBlock.InnerXml = expressionContext[commandParameter].ToString();
                }
                else if (sdtElement is SdtRun)
                {
                    ((SdtRun)sdtElement).SdtContentRun.InnerXml = expressionContext[commandParameter].ToString();
                }
                else
                {
                    throw new Exception("The template should not have other SdtElement types.");
                }
            }
        }
        private static void DeleteContentControls(WordprocessingDocument wordDoc)
        {
            var allsdtElements = (from element in wordDoc.MainDocumentPart.Document.Descendants<SdtElement>()
                                  select new
                                  {
                                      sdtElement = element,
                                      xPath = GetContainerBasedXPathAllNodes(element),
                                      currentNumberOfSibling = GetSdtElementSiblingCount(element)
                                  }).OrderBy(element => element.xPath).ToList();

            for (int j = 0; j < allsdtElements.Count(); j++)
            {
                OpenXmlElement[] elements;
                var element = allsdtElements[j];
                if (element.sdtElement is SdtBlock)
                {
                    SdtBlock sdtBlock = (SdtBlock)element.sdtElement;

                    OpenXmlElement blockParent = sdtBlock.Parent;

                    elements = sdtBlock.SdtContentBlock.ChildElements.ToArray();

                    OpenXmlElement InsertElement;

                    for (int i = elements.Count() - 1; i >= 0; i--)
                    {
                        InsertElement = elements[i].CloneNode(true);
                        blockParent.InsertAfter(InsertElement, sdtBlock);
                    }

                    sdtBlock.Remove();
                }
                else if (element.sdtElement is SdtRun)
                {
                    SdtRun sdtBlock = (SdtRun)element.sdtElement;

                    OpenXmlElement blockParent = sdtBlock.Parent;

                    elements = sdtBlock.SdtContentRun.ChildElements.ToArray();

                    OpenXmlElement InsertElement;

                    for (int i = elements.Count() - 1; i >= 0; i--)
                    {
                        InsertElement = elements[i].CloneNode(true);
                        blockParent.InsertAfter(InsertElement, sdtBlock);
                    }

                    sdtBlock.Remove();
                }
                else if (element.sdtElement is SdtRow)
                {
                    SdtRow sdtBlock = (SdtRow)element.sdtElement;

                    OpenXmlElement blockParent = sdtBlock.Parent;

                    elements = sdtBlock.SdtContentRow.ChildElements.ToArray();

                    OpenXmlElement InsertElement;

                    for (int i = elements.Count() - 1; i >= 0; i--)
                    {
                        InsertElement = elements[i].CloneNode(true);
                        blockParent.InsertAfter(InsertElement, sdtBlock);
                    }

                    sdtBlock.Remove();
                }
                else if (element.sdtElement is SdtCell)
                {
                    SdtCell sdtBlock = (SdtCell)element.sdtElement;

                    OpenXmlElement blockParent = sdtBlock.Parent;

                    elements = sdtBlock.SdtContentCell.ChildElements.ToArray();

                    OpenXmlElement InsertElement;

                    for (int i = elements.Count() - 1; i >= 0; i--)
                    {
                        InsertElement = elements[i].CloneNode(true);
                        blockParent.InsertAfter(InsertElement, sdtBlock);
                    }

                    sdtBlock.Remove();
                }
            }

            allsdtElements = (from element in wordDoc.MainDocumentPart.Document.Descendants<SdtElement>()
                              select new
                              {
                                  sdtElement = element,
                                  xPath = GetContainerBasedXPathAllNodes(element),
                                  currentNumberOfSibling = GetSdtElementSiblingCount(element)
                              }).OrderBy(element => element.xPath).ToList();
            if (allsdtElements != null && allsdtElements.Count > 0)
            {
                DeleteContentControls(wordDoc);
            }
        }

        private static Dictionary<string, string> BuildExpressionContext(XmlNode node)
        {
            Dictionary<string, string> context = new Dictionary<string, string>();
            if (node != null)
            {
                BuildExpressionContextRecursive(node, context);
            }

            return context;
        }

        private static void BuildExpressionContextRecursive(XmlNode node, Dictionary<string, string> context)
        {
            foreach (XmlNode childNode in node.ChildNodes)
            {
                var nonTextElements = childNode.ChildNodes.OfType<XmlElement>();

                if (nonTextElements.Count() == 0)
                {
                    if (!context.ContainsKey(childNode.Name))
                    {
                        string nodeValue = childNode.InnerText.Trim();
                        context.Add(childNode.Name, GetNodeValueInCorrectFormat(nodeValue).ToString());
                    }
                }
            }

            if (node.ParentNode != null)
            {
                BuildExpressionContextRecursive(node.ParentNode, context);
            }
        }

        private static object GetNodeValueInCorrectFormat(string nodeValue)
        {
            object variableValue = nodeValue;
            double doubleValue;
            DateTime dateTimeValue;
            bool boolValue;

            if (double.TryParse(nodeValue, out doubleValue))
            {
                variableValue = doubleValue;
            }
            else if (DateTime.TryParse(nodeValue, out dateTimeValue))
            {
                variableValue = dateTimeValue;
            }
            else if (bool.TryParse(nodeValue, out boolValue))
            {
                variableValue = boolValue;
            }

            return variableValue;
        }
        private static int GetAncestorCount(SdtElement element)
        {
            var ancestors = element.Ancestors<SdtElement>();
            IEnumerable<SdtAlias> alias;
            IEnumerable<Tag> tag;
            int counter = 0;
            for (int i = ancestors.Count() - 1; i >= 0; i--)
            {
                alias = ancestors.ElementAt(i).SdtProperties.Descendants<SdtAlias>();
                tag = ancestors.ElementAt(i).SdtProperties.Descendants<Tag>();

                if (alias.Count() == 1 && tag.Count() == 1
                    && string.Compare(alias.First().Val, CommandNameSubstituteContent) == 0)
                {
                    counter++;
                }
            }

            return counter;
        }

        private static string GetContainerBasedXPathAllNodes(SdtElement sdtElement)
        {
            StringBuilder expression = new StringBuilder();
            var ancestors = sdtElement.Ancestors<SdtElement>();
            IEnumerable<SdtAlias> alias;
            IEnumerable<Tag> tag;
            for (int i = ancestors.Count() - 1; i >= 0; i--)
            {
                alias = ancestors.ElementAt(i).SdtProperties.Descendants<SdtAlias>();
                tag = ancestors.ElementAt(i).SdtProperties.Descendants<Tag>();

                if (alias.Count() == 1 && tag.Count() == 1
                    && string.Compare(alias.First().Val, CommandNameSubstituteContent) == 0)
                {
                    expression.Append(string.Format("/{0}[{1}]", tag.First().Val, GetSdtElementLocalIndex(ancestors.ElementAt(i)).ToString()));
                }
            }

            alias = sdtElement.SdtProperties.Descendants<SdtAlias>();
            tag = sdtElement.SdtProperties.Descendants<Tag>();

            if (alias.Count() == 1
                && tag.Count() == 1
                && string.Compare(alias.First().Val, CommandNameSubstituteContent) == 0)
            {
                expression.Append(string.Format("/{0}", tag.First().Val));
            }

            return expression.ToString();
        }

        private static string GetContainerBasedXPath(SdtElement sdtElement)
        {
            StringBuilder expression = new StringBuilder();
            expression.Append(GetContainerBasedXPathAllNodes(sdtElement));
            expression.Append(string.Format("[{0}]", GetSdtElementLocalIndex(sdtElement).ToString()));

            return expression.ToString();
        }

        private static int GetSdtElementLocalIndex(SdtElement sdtElement)
        {
            int index = 1;
            var elementAlias = sdtElement.SdtProperties.Descendants<SdtAlias>();
            var elementTag = sdtElement.SdtProperties.Descendants<Tag>();

            if (elementAlias.Count() == 1 && elementTag.Count() == 1)
            {
                SdtElement previous = sdtElement;
                while ((previous = previous.PreviousSibling<SdtElement>()) != null)
                {
                    var alias = previous.SdtProperties.Descendants<SdtAlias>();
                    var tag = previous.SdtProperties.Descendants<Tag>();

                    if (alias.Count() == 1 && tag.Count() == 1
                        && string.Compare(alias.First().Val, elementAlias.First().Val) == 0
                        && string.Compare(tag.First().Val, elementTag.First().Val) == 0)
                    {
                        index++;
                    }
                }
            }

            return index;
        }

        private static int GetSdtElementSiblingCount(SdtElement sdtElement)
        {
            int index = 0;
            var elementAlias = sdtElement.SdtProperties.Descendants<SdtAlias>();
            var elementTag = sdtElement.SdtProperties.Descendants<Tag>();

            if (elementAlias.Count() == 1 && elementTag.Count() == 1)
            {
                SdtElement node = sdtElement.Parent.GetFirstChild<SdtElement>();

                while ((node = node.NextSibling<SdtElement>()) != null)
                {
                    var alias = node.SdtProperties.Descendants<SdtAlias>();
                    var tag = node.SdtProperties.Descendants<Tag>();

                    if (alias.Count() == 1 && tag.Count() == 1
                        && string.Compare(alias.First().Val, elementAlias.First().Val) == 0
                        && string.Compare(tag.First().Val, elementTag.First().Val) == 0)
                    {
                        index++;
                    }
                }
            }

            return index;
        }

        private static string GetPropertyValueFromInterpretedDocumentInContext(string propertyName, XmlDocument processedDoc, SdtElement sdtElement)
        {
            string xpathExpressionString = GetContainerBasedXPath(sdtElement);
            XmlNode sdtElementMappingNode = processedDoc.SelectSingleNode(xpathExpressionString);
            return GetPropertyValueFromInterpretedDocument(propertyName, sdtElementMappingNode);
        }

        private static string GetPropertyValueFromInterpretedDocument(string propertyName, XmlNode sdtElementMappingNode)
        {
            if (sdtElementMappingNode != null)
            {
                XmlNode propertyNode = sdtElementMappingNode.SelectSingleNode(propertyName);

                if (propertyNode != null)
                {
                    return propertyNode.InnerText.Trim();
                }
                else
                {
                    return GetPropertyValueFromInterpretedDocument(propertyName, sdtElementMappingNode.ParentNode);
                }
            }

            return string.Empty;
        }

        private static string GetNodeValueFromProcessedXmlDoc(string nodeName, SdtElement element, XmlDocument processedDoc)
        {
            StringBuilder ContainerBasedXPath = new StringBuilder();
            ContainerBasedXPath.Append(GetContainerBasedXPathAllNodes(element));
            ContainerBasedXPath.Append(string.Format("[{0}]", GetSdtElementLocalIndex(element).ToString()));
            ContainerBasedXPath.Append(string.Format("/{0}", nodeName));
            XmlNode tokenNode = processedDoc.SelectSingleNode(ContainerBasedXPath.ToString());
            if (tokenNode != null)
            {
                return tokenNode.InnerXml.Trim();
            }

            return string.Empty;
        }

    }

}
