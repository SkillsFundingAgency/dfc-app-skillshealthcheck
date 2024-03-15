using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;
//using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Common;
//using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Entity;
//using IMS.SkillsCentral.XmlExtensionObjects.SkillsReport.Resources;

namespace DfE.SkillsCentral.Api.Application.DocumentsFormatters
{
    public class SkillsReportXsltExtension
    {
        const string SHCFullReportCurrentDateTag = "<CurrentDate>{0}</CurrentDate>";
        const string SHCFullReportDatetimeFormat = "dddd, MMMM dd, yyyy";
        public XPathNavigator GetSHCReportResult(XPathNodeIterator iterator)
        {
            
            iterator.MoveNext();
            XDocument doc = XDocument.Parse(iterator.Current.OuterXml);

            string qualificationLevel = GetElementValue(doc, Constant.QualificationLevel);

            SkillAreasResult skills = new SkillAreasResult(qualificationLevel,
                                                           GetElementValue(doc, Constant.SkillAreasType),
                                                           GetElementValue(doc, Constant.SkillAreasAnswers),
                                                           GetElementValue(doc, Constant.SkillAreasComplete));

            InterestResult interest = new InterestResult(qualificationLevel,
                                                         GetElementValue(doc, Constant.InterestType),
                                                         GetElementValue(doc, Constant.InterestAnswers),
                                                         GetElementValue(doc, Constant.InterestComplete));

            PersonalStyleResult personalStyle = new PersonalStyleResult(qualificationLevel,
                                                         GetElementValue(doc, Constant.PersonalType),
                                                         GetElementValue(doc, Constant.PersonalAnswers),
                                                         GetElementValue(doc, Constant.PersonalComplete));

            MotivationResult motivation = new MotivationResult(qualificationLevel,
                                                               GetElementValue(doc, Constant.MotivationType),
                                                               GetElementValue(doc, Constant.MotivationAnswers),
                                                               GetElementValue(doc, Constant.MotivationComplete));

            NumericResult numeric = new NumericResult(qualificationLevel,
                                                     GetElementValue(doc, Constant.NumericEase),
                                                     GetElementValue(doc, Constant.NumericTiming),
                                                     GetElementValue(doc, Constant.NumericType),
                                                     GetElementValue(doc, Constant.NumericAnswers),
                                                     GetElementValue(doc, Constant.NumericComplete));

            VerbalResult verbal = new VerbalResult(qualificationLevel,
                                                   GetElementValue(doc, Constant.VerbalEase),
                                                   GetElementValue(doc, Constant.VerbalTiming),
                                                   GetElementValue(doc, Constant.VerbalType),
                                                   GetElementValue(doc, Constant.VerbalAnswers),
                                                   GetElementValue(doc, Constant.VerbalComplete));

            CheckingResult checking = new CheckingResult(qualificationLevel,
                                                         GetElementValue(doc, Constant.CheckingEase),
                                                         GetElementValue(doc, Constant.CheckingTiming),
                                                         GetElementValue(doc, Constant.CheckingType),
                                                         GetElementValue(doc, Constant.CheckingAnswers),
                                                         GetElementValue(doc, Constant.CheckingComplete),
                                                         GetElementValue(doc, Constant.CheckingEnjoyment));

            MechanicalResult mechanical = new MechanicalResult(qualificationLevel,
                                                               GetElementValue(doc, Constant.MechanicalEase),
                                                               GetElementValue(doc, Constant.MechanicalTiming),
                                                               GetElementValue(doc, Constant.MechanicalType),
                                                               GetElementValue(doc, Constant.MechanicalAnswers),
                                                               GetElementValue(doc, Constant.MechanicalComplete),
                                                               GetElementValue(doc, Constant.MechanicalEnjoyment));

            ShapesResult shapes = new ShapesResult(qualificationLevel,
                                                   GetElementValue(doc, Constant.SpatialEase),
                                                   GetElementValue(doc, Constant.SpatialTiming),
                                                   GetElementValue(doc, Constant.SpatialType),
                                                   GetElementValue(doc, Constant.SpatialAnswers),
                                                   GetElementValue(doc, Constant.SpatialComplete),
                                                   GetElementValue(doc, Constant.SpatialEnjoyment));

            AbstractResult abstractResult = new AbstractResult(qualificationLevel,
                                                               GetElementValue(doc, Constant.AbstractEase),
                                                               GetElementValue(doc, Constant.AbstractTiming),
                                                               GetElementValue(doc, Constant.AbstractType),
                                                               GetElementValue(doc, Constant.AbstractAnswers),
                                                               GetElementValue(doc, Constant.AbstractComplete),
                                                               GetElementValue(doc, Constant.AbstractEnjoyment));

            JobSuggestionResult jobSuggestions = new JobSuggestionResult(qualificationLevel,
                                                               GetElementValue(doc, Constant.SkillAreasExcludedJobFamilies1),
                                                               GetElementValue(doc, Constant.SkillAreasExcludedJobFamilies2),
                                                               GetElementValue(doc, Constant.SkillAreasExcludedJobFamilies3));

            StringBuilder resultXML = new StringBuilder();
            resultXML.Append(Constant.RootStart);

            resultXML.Append(string.Format(SHCFullReportCurrentDateTag, DateTime.Now.ToString(SHCFullReportDatetimeFormat)));
            resultXML.Append(skills.GetResult());
            resultXML.Append(interest.GetResult());
            resultXML.Append(personalStyle.GetResult());
            resultXML.Append(motivation.GetResult());
            resultXML.Append(numeric.GetResult());
            resultXML.Append(verbal.GetResult());
            resultXML.Append(checking.GetResult());
            resultXML.Append(mechanical.GetResult());
            resultXML.Append(shapes.GetResult());
            resultXML.Append(abstractResult.GetResult());
            jobSuggestions.RankedSkillsCategories = skills.RankedSkillCategories;
            jobSuggestions.InterestCategories = interest.InterestCategories;
            jobSuggestions.VerbalComplete = GetboolValue(verbal.Complete);
            jobSuggestions.NumericComplete = GetboolValue(numeric.Complete);
            jobSuggestions.CheckingComplete = GetboolValue(checking.Complete);
            jobSuggestions.SpatialComplete = GetboolValue(shapes.Complete);
            jobSuggestions.AbstractComplete = GetboolValue(abstractResult.Complete);
            jobSuggestions.MechanicalComplete = GetboolValue(mechanical.Complete);
            jobSuggestions.Complete = skills.Complete;
            resultXML.Append(jobSuggestions.GetResult());
            resultXML.Append(Constant.RootEnd);

            return XDocument.Parse(resultXML.ToString()).CreateNavigator();
        }



        private string GetElementValue(XDocument doc, string name)
        {
            string returnValue = string.Empty;
            var items = from item in doc.Descendants(Constant.SkillsDocumentDataValue)
                        where item.Descendants(Constant.Title).FirstOrDefault().Value == name
                        select item.Descendants(Constant.Value).FirstOrDefault().Value;
            if (items.Count() > 0)
            {
                returnValue = items.FirstOrDefault().ToString();
            }

            return returnValue;
        }

        private bool GetboolValue(string value)
        {
            bool returnval = false;
            bool.TryParse(value, out returnval);
            return returnval;
        }
    }
}
