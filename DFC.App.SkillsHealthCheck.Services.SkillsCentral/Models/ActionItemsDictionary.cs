using System;
using System.Xml.Serialization;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models
{
    [XmlType(AnonymousType = true)]
    [XmlRoot(ElementName = "dictionary", Namespace = "", IsNullable = false)]
    public class ActionItemsDictionary
    {
        [System.Xml.Serialization.XmlElementAttribute("item")]
        public ActionItemsDictionaryEntry[] item { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class ActionItemsDictionaryEntry
    {
        [System.Xml.Serialization.XmlElementAttribute("key")]
        public ActionItemsDictionaryEntryKey key { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("value")]
        public ActionItemsDictionaryEntryValue value { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class ActionItemsDictionaryEntryKey
    {
        [System.Xml.Serialization.XmlElementAttribute("long")]
        public byte @long { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class ActionItemsDictionaryEntryValue
    {
        [System.Xml.Serialization.XmlElementAttribute("Action")]
        public ActionItemsDictionaryEntryValueAction Action { get; set; }
    }

    [XmlType(AnonymousType = true)]
    public class ActionItemsDictionaryEntryValueAction
    {
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string Responsibility { get; set; }
        public string ActionPlanActionStatus { get; set; }
    }
}
