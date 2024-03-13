namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Contains methods used for evaluating expressions
    /// </summary>
    public class ExpressionEvaluator
    {

        /// <summary>
        /// Evaluates the expression passed in.
        /// Accepts the following operators +, -, *, /, %, >, >=, =, and , or        
        /// Note: logical and/or are case sensitive, so please use lower case only.
        /// </summary>
        /// <param name="expression">Expression to be evaluated</param>
        /// <returns>return the evaulated value as object</returns>
        public static object Evaluate(string expression)
        {
            object result;
            try
            {
                using (StringReader sr = new StringReader("<r/>"))
                {
                    expression = expression.Replace("/", " div ").Replace("%", " mod ");
                    expression = expression.Replace("true", " true() ").Replace("false", " false() ");
                    System.Xml.XPath.XPathDocument document = new System.Xml.XPath.XPathDocument(sr);
                    System.Xml.XPath.XPathNavigator navigator = document.CreateNavigator();
                    result = navigator.Evaluate(expression);
                }
            }
            catch (Exception ex)
            {
                //Logger.LogException("Unable to Evaluate the Expression.", ex);
                throw;
            }

            return result;
        }

        /// <summary>
        /// Extracts tokens from the string expression.
        /// string enclosed by braces eg- {token}
        /// </summary>
        /// <param name="expression">expression to be evaluated</param>
        /// <returns>List of strings</returns>
        public static List<string> ExtracTokens(string expression)
        {
            List<string> keys = new List<string>();
            try
            {
                string[] items = Regex.Split(expression, @"(?<=\{)([^\}]+)(?=\})");
                keys = items.ToList().FindAll(delegate (string each) { return !each.Contains("{") && !each.Contains("}"); });
            }
            catch (Exception ex)
            {
                //Logger.LogException("Unable to Extract tokens from the Expression.", ex);
                throw;
            }

            return keys;
        }

    }
}
