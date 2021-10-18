using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models
{
    [Serializable]
    [KnownType(typeof(DocumentTypeCode))]
    [KnownType(typeof(DocumentFormatter))]
    [KnownType(typeof(List<DocumentFormatter>))]
    public class DocumentType
    {
        public String Title { get; set; }
        public DocumentTypeCode TypeCode { get; set; }
        public List<String> DocumentTypes { get; set; }
        public IList<DocumentFormatter> Formatters { get; set; }
    }

    [Serializable]
    public enum DocumentTypeCode
    {
        [Description("Adviser Created Action Plan")]
        [Display(Name = "Adviser created action plan")]
        ACAP,
        [Description("Curriculum vitae")]
        [Display(Name = "CV")]
        CV,
        [Description("Course search")]
        [Display(Name = "Course search")]
        CS,
        [Description("Skills Health Check")]
        [Display(Name = "Skills health check")]
        SHC,
        [Description("Personal Action Plan")]
        [Display(Name = "Personal action plan")]
        PAP
    }

    [Serializable]
    public class DocumentFormatter
    {
        public String Title { get; set; }
        public String ContentType { get; set; }
        public String FileExtension { get; set; }
        public String FormatterName { get; set; }
    }
    public static class DocumentFileExtensions
    {
        public static readonly String Pdf = ".pdf";
        public static readonly String Docx = ".docx";
    }

    public static class DocumentContentTypes
    {
        public static readonly String Pdf = "application/pdf";
        public static readonly String Docx = "application/docx";
    }

}
