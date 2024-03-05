
namespace DfE.SkillsCentral.Api.Application.Interfaces.Models
{
    using System.Xml.Xsl;

    /// <summary>
    /// A container used for storing XSLT configuration
    /// </summary>
    public class XslTransformation
    {
        /// <summary>
        /// Gets or sets the loaded xslt transformation file
        /// </summary>
        public XslCompiledTransform TransformationFile { get; set; }

        /// <summary>
        /// Gets or sets transformation related extension objects configuration
        /// </summary>
        public XsltArgumentList TransformationArgumentList { get; set; }
    }
}
