using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.SkillsCentral.Api.Domain.Models;
using Newtonsoft.Json.Linq;
using SkillsDocument = DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models.SkillsDocument;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers
{
    /// <summary>
    /// Does Skills document helper operations
    /// </summary>
    public static class SkillsHealthChecksHelper
    {

        public static bool AssessmentNotCompleted(this SkillsDocument skillsDocument, AssessmentType asessmentType)
        {
            var assessmentCompDataValue =
                skillsDocument.SkillsDocumentDataValues.FirstOrDefault(
                    docValue => docValue.Title.Equals($"{asessmentType}.Complete", StringComparison.OrdinalIgnoreCase));

            if (assessmentCompDataValue != null)
            {
                return !assessmentCompDataValue.Value.Equals("true", StringComparison.OrdinalIgnoreCase);
            }

            return true;
        }

        /// <summary>
        /// Updates the data values.
        /// </summary>
        /// <param name="skillsDocument">The SKLLLS document.</param>
        /// <param name="answers">The answers.</param>
        /// <param name="isCompleted">if set to <c>true</c> [is completed].</param>
        /// <param name="asessmentType">Type of the asessment.</param>
        /// <param name="asessmentTypeTotalNumberLessFeedback"></param>
        /// <param name="currentAnsweredQuestionNumber">Current question we are sending</param>
        /// <returns></returns>
        public static DFC.SkillsCentral.Api.Domain.Models.SkillsDocument UpdateDataValues(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, string answers,
            bool isCompleted, AssessmentType asessmentType, int asessmentTypeTotalNumberLessFeedback, int currentAnsweredQuestionNumber)
        {
            answers = answers.Trim();
            //make generic
            foreach (var dataValue in skillsDocument.DataValueKeys)
            {
                //if (dataValue.Title.Equals($"{asessmentType}.Type", StringComparison.OrdinalIgnoreCase))
                //{
                //    dataValue.Value = GetDataValueByAssessmentType(asessmentType);
                //}
                if (dataValue.Key.Equals($"{asessmentType}.Answers", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(dataValue.Value))
                    {
                        skillsDocument.DataValueKeys[dataValue.Key] = answers;
                    }
                    else
                    {
                        var answeredQuestionsTotal = dataValue.Value.Split(',').Length;
                        // Safe guard having too many answers supplied
                        if (answeredQuestionsTotal < asessmentTypeTotalNumberLessFeedback && currentAnsweredQuestionNumber - answeredQuestionsTotal == 1)
                        {
                            skillsDocument.DataValueKeys[dataValue.Key] = $"{dataValue.Value},{answers}";
                        }
                    }
                }
                else if (dataValue.Key.Equals($"{asessmentType}.Complete", StringComparison.OrdinalIgnoreCase))
                {
                    skillsDocument.DataValueKeys[dataValue.Key] = isCompleted.ToString();
                }
            }
            return skillsDocument;
        }


        /// <summary>
        /// Updates the job family data value.
        /// </summary>
        /// <param name="skillsDocument">The SKLLLS document.</param>
        /// <param name="jobNumber">The job number.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DFC.SkillsCentral.Api.Domain.Models.SkillsDocument UpdateJobFamilyDataValue(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, int jobNumber,
            string value)
        {
            
            if (!skillsDocument.DataValueKeys.ContainsKey(string.Format(Constants.SkillsHealthCheck.JobFamilyTitle, jobNumber)))
            {
                skillsDocument.DataValueKeys.Add(string.Format(Constants.SkillsHealthCheck.JobFamilyTitle, jobNumber), value);
            }
            else
            {
                skillsDocument.DataValueKeys[string.Format(Constants.SkillsHealthCheck.JobFamilyTitle, jobNumber)] = value;
            }
            
            return skillsDocument;
        }

        /// <summary>
        /// Updates the specific data value.
        /// </summary>
        /// <param name="skillsDocument">The skills document.</param>
        /// <param name="documentValueTitle">The document value title.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static DFC.SkillsCentral.Api.Domain.Models.SkillsDocument UpdateSpecificDataValue(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument,
            string documentValueTitle,
            string value)
        {

            if (!skillsDocument.DataValueKeys.ContainsKey(documentValueTitle))
            {
                skillsDocument.DataValueKeys.Add(documentValueTitle, value);
            }
            else
            {
                skillsDocument.DataValueKeys[documentValueTitle] = value;
            }

            return skillsDocument;
        }


        /// <summary>
        /// Updates the elimination data values.
        /// </summary>
        /// <param name="skillsDocument">The SKLLLS document.</param>
        /// <param name="answers">The answers.</param>
        /// <param name="isCompleted">if set to <c>true</c> [is completed].</param>
        /// <param name="asessmentType">Type of the asessment.</param>
        /// <param name="questionNumber">The question number.</param>
        /// <param name="totalQuestionsNumber">Total number of questions in the assessment type</param>
        /// <param name="actualAssessmentMaxQuestionsNumber">Set number of questions for assessment</param>
        /// <param name="actualQuestionNumberAnswered">the current displayed question number</param>
        /// <returns></returns>
        public static DFC.SkillsCentral.Api.Domain.Models.SkillsDocument UpdateEliminationDataValues(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, string answers,
            bool isCompleted, AssessmentType asessmentType, int questionNumber, int totalQuestionsNumber, int actualAssessmentMaxQuestionsNumber, int actualQuestionNumberAnswered)
        {
            answers = answers.Trim();
            //make generic
            foreach (var dataValue in skillsDocument.DataValueKeys)
            {
                //if (dataValue.Title.Equals($"{asessmentType}.Type", StringComparison.OrdinalIgnoreCase))
                //{
                //    dataValue.Value = GetDataValueByAssessmentType(asessmentType);
                //}
                if (dataValue.Key.Equals($"{asessmentType}.Answers", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(dataValue.Value))
                    {
                        skillsDocument.DataValueKeys[dataValue.Key] = $"{answers},-1,-1";
                    }
                    else
                    {
                        var answerList = dataValue.Value.Split(',').ToList();
                        var expectedQnumber = skillsDocument.GetCurrentNumberEliminationQuestions(asessmentType,
                            questionNumber);

                        var suppliedAnswerCount = answerList.Count(x => !x.Equals("-1"));
                        // Only add to list if are not yet maximum answer list
                        if (suppliedAnswerCount < actualAssessmentMaxQuestionsNumber && actualQuestionNumberAnswered == expectedQnumber)
                        {
                            string currentAnswersList;

                            var answerIndex = (questionNumber - 1) * 3;

                            var listWithoutCurrentQuestion = new List<string>();

                            int currentIndex = 0;
                            foreach (var answer in answerList)
                            {
                                if (currentIndex == answerIndex)
                                {
                                    break;
                                }

                                listWithoutCurrentQuestion.Add(answer);
                                currentIndex++;
                            }
                            var existingAnswers = string.Join(",", listWithoutCurrentQuestion);

                            if (answerList.Count - 1 > answerIndex)
                            {
                                var currentAnswers = answerList.GetRange(answerIndex, 3);

                                currentAnswers[1] = answers;
                                currentAnswers[2] =
                                    GetMissingValue(new List<string> { currentAnswers[0], currentAnswers[1] });

                                currentAnswersList = string.Join(",", currentAnswers);
                            }
                            else
                            {
                                currentAnswersList = $"{answers},-1,-1";
                            }

                            skillsDocument.DataValueKeys[dataValue.Key] = !string.IsNullOrWhiteSpace(existingAnswers)
                                ? $"{existingAnswers},{currentAnswersList}"
                                : currentAnswersList;
                        }
                    }
                }
                else if (dataValue.Key.Equals($"{asessmentType}.Complete", StringComparison.OrdinalIgnoreCase))
                {
                    skillsDocument.DataValueKeys[dataValue.Key] = isCompleted.ToString();
                }
            }
            return skillsDocument;
        }

        /// <summary>
        /// Updates the multiple answer data values.
        /// </summary>
        /// <param name="skillsDocument">The SKLLLS document.</param>
        /// <param name="answers">The answers.</param>
        /// <param name="isCompleted">if set to <c>true</c> [is completed].</param>
        /// <param name="asessmentType">Type of the asessment.</param>
        /// <param name="currentQuestion">The current question.</param>
        /// <param name="currentQuestionIndex">Index of the current question.</param>
        /// <param name="numberOfAnswers">The number of answers.</param>
        /// <param name="assessmentQuestionsOverView">The assessment questions over view.</param>
        /// <param name="actualQuestionNumber">Displayed question number and actual in the saved results</param>
        /// <returns></returns>
        public static DFC.SkillsCentral.Api.Domain.Models.SkillsDocument UpdateMultipleAnswerDataValues(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, string answers,
            bool isCompleted, AssessmentType asessmentType, int currentQuestion, int currentQuestionIndex,
            int numberOfAnswers, AssessmentQuestionsOverView assessmentQuestionsOverView, int actualQuestionNumber)
        {
            answers = answers.Trim();
            //make generic
            foreach (var dataValue in skillsDocument.DataValueKeys)
            {
                //if (dataValue.Key.Equals($"{asessmentType}.Type", StringComparison.OrdinalIgnoreCase))
                //{
                //    dataValue[].Value = GetDataValueByAssessmentType(asessmentType);
                //}
                if (dataValue.Key.Equals($"{asessmentType}.Answers", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(dataValue.Value))
                    {
                        var answersArray = new string[numberOfAnswers];

                        int answerIndex = 0;
                        answersArray[0] = answers;
                        foreach (var answer in answersArray)
                        {
                            if (answerIndex != 0)
                            {
                                answersArray[answerIndex] = "-1";
                            }
                            answerIndex++;
                        }

                        skillsDocument.DataValueKeys[dataValue.Key] = string.Join(",", answersArray);
                    }
                    else
                    {
                        var answersList = dataValue.Value.Split(',').ToList();

                        var suppliedAnswerCount = answersList.Count(x => !x.Equals("-1"));
                        // Only add to list if are not yet maximum answer list
                        if (suppliedAnswerCount < assessmentQuestionsOverView.ActualQuestionsNumber && actualQuestionNumber - suppliedAnswerCount == 1)
                        {
                            string currentAnswersList;

                            var questionTally = 0;
                            var currentSubQuestions = 0;
                            var answerIndex = 0;
                            var rangeFromIndex = 0;
                            foreach (var questionOverview in assessmentQuestionsOverView.QuestionOverViewList)
                            {
                                currentSubQuestions = questionOverview.SubQuestions;
                                questionTally = questionTally + questionOverview.SubQuestions;
                                rangeFromIndex = questionOverview.SubQuestions;
                                if (questionOverview.QuestionNumber == currentQuestion)
                                {
                                    break;
                                }
                                answerIndex = questionTally;
                            }

                            var listWithoutCurrentQuestion = new List<string>();

                            int currentIndex = 0;
                            foreach (var answer in answersList)
                            {
                                if (currentIndex >= answerIndex)
                                {
                                    break;
                                }

                                listWithoutCurrentQuestion.Add(answer);
                                currentIndex++;
                            }
                            var existingAnswers = string.Join(",", listWithoutCurrentQuestion);


                            if (answersList.Count - 1 > answerIndex)
                            {
                                var currentAnswers = answersList.GetRange(answerIndex, rangeFromIndex);

                                currentAnswers[currentQuestionIndex] = answers;

                                currentAnswersList = string.Join(",", currentAnswers);
                            }
                            else
                            {
                                var newList = new string[currentSubQuestions];
                                var newListIndex = 0;
                                newList[0] = answers;
                                foreach (var answer in newList)
                                {
                                    if (newListIndex != 0)
                                    {
                                        newList[newListIndex] = "-1";
                                    }
                                    newListIndex++;
                                }
                                currentAnswersList = string.Join(",", newList);
                            }
                            
                            skillsDocument.DataValueKeys[dataValue.Key] = !string.IsNullOrWhiteSpace(existingAnswers)
                                ? $"{existingAnswers},{currentAnswersList}"
                                : currentAnswersList; 
                        }
                    }
                }
                else if (dataValue.Key.Equals($"{asessmentType}.Complete", StringComparison.OrdinalIgnoreCase))
                {
                    skillsDocument.DataValueKeys[dataValue.Key] = isCompleted.ToString();
                }
            }
            return skillsDocument;
        }

        /// <summary>
        /// Gets the missing value.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        private static string GetMissingValue(List<string> list)
        {
            if (!list.Exists(x => x.Equals("0")))
                return "0";
            if (!list.Exists(x => x.Equals("1")))
                return "1";
            if (!list.Exists(x => x.Equals("2")))
                return "2";

            return "-1";
        }

        /// <summary>
        /// Gets the assessment next elimination question number.
        /// </summary>
        /// <param name="skillsDocument">The SKLLLS document.</param>
        /// <param name="asessmentType">Type of the asessment.</param>
        /// <returns></returns>
        public static int GetAssessmentNextEliminationQuestionNumber(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument,
            AssessmentType asessmentType)
        {
            int nextQuestion = 0;

            foreach (var dataValue in skillsDocument.DataValueKeys)
            {
                if (dataValue.Key.Equals($"{asessmentType}.Answers", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(dataValue.Value))
                    {
                        nextQuestion = 1;
                    }
                    else
                    {
                        var answers = dataValue.Value.Split(',').ToList();
                        var lastQuestion = answers.Count / 3;
                        var answerIndex = (lastQuestion - 1) * 3;
                        var currentAnswers = answers.GetRange(answerIndex, 3);
                        return currentAnswers.Exists(x => x.Equals("-1")) ? lastQuestion : lastQuestion + 1;
                    }
                    break;
                }
            }
            return nextQuestion;
        }

        /// <summary>
        /// Gets the assessment next multiple question number.
        /// </summary>
        /// <param name="skillsDocument">The SKLLLS document.</param>
        /// <param name="asessmentType">Type of the asessment.</param>
        /// <param name="assessmentQuestionOverview">The assessment question overview.</param>
        /// <returns></returns>
        public static int GetAssessmentNextMultipleQuestionNumber(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument,
            AssessmentType asessmentType, AssessmentQuestionsOverView assessmentQuestionOverview)
        {
            int nextQuestion = 0;

            foreach (var dataValue in skillsDocument.DataValueKeys)
            {
                if (dataValue.Key.Equals($"{asessmentType}.Answers", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(dataValue.Value))
                    {
                        nextQuestion = 1;
                        break;
                    }
                    var answers = dataValue.Value.Split(',').ToList();

                    var questionTally = 0;
                    var currentQuestion = 0;
                    var answerIndex = 0;
                    var rangeFromIndex = 0;
                    foreach (var questionOverview in assessmentQuestionOverview.QuestionOverViewList)
                    {
                        currentQuestion = questionOverview.QuestionNumber;
                        questionTally = questionTally + questionOverview.SubQuestions;
                        rangeFromIndex = questionOverview.SubQuestions;
                        if (questionTally == answers.Count)
                        {
                            break;
                        }
                        answerIndex = questionTally;
                    }

                    var currentAnswers = answers.GetRange(answerIndex, rangeFromIndex);
                    return currentAnswers.Exists(x => x.Equals("-1")) ? currentQuestion : currentQuestion + 1;
                }
            }
            return nextQuestion;
        }

        /// <summary>
        /// Gets the already selected answer.
        /// </summary>
        /// <param name="skillsDocument">The SKLLLS document.</param>
        /// <param name="asessmentType">Type of the asessment.</param>
        /// <param name="currentQuestion">The current question.</param>
        /// <returns></returns>
        public static int GetAlreadySelectedAnswer(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, AssessmentType asessmentType,
            int currentQuestion)
        {
            var selectedAnswer = -1;

            foreach (var dataValue in skillsDocument.DataValueKeys)
            {
                if (dataValue.Key.Equals($"{asessmentType}.Answers", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrEmpty(dataValue.Value))
                    {
                        var answers = dataValue.Value.Split(',').ToList();
                        var answerIndex = (currentQuestion - 1) * 3;
                        if (answers.Count - 1 > answerIndex)
                        {
                            var currentAnswers = answers.GetRange(answerIndex, 3);

                            var existingAnswer = currentAnswers.FirstOrDefault(x => !x.Equals("-1"));
                            if (existingAnswer != null)
                            {
                                selectedAnswer = Convert.ToInt32(existingAnswer);
                            }
                        }
                    }
                }
            }
            return selectedAnswer;
        }


        /// <summary>
        /// Gets the current number elimination questions.
        /// </summary>
        /// <param name="skillsDocument">The SKLLLS document.</param>
        /// <param name="asessmentType">Type of the asessment.</param>
        /// <param name="currentQuestion">The current question.</param>
        /// <returns></returns>
        public static int GetCurrentNumberEliminationQuestions(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument,
            AssessmentType asessmentType, int currentQuestion)
        {
            var selectedAnswer = -1;

            foreach (var dataValue in skillsDocument.DataValueKeys)
            {
                if (dataValue.Key.Equals($"{asessmentType}.Answers", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrEmpty(dataValue.Value))
                    {
                        var answers = dataValue.Value.Split(',').ToList();
                        var answerIndex = (currentQuestion - 1) * 3;
                        if (answers.Count - 1 > answerIndex)
                        {
                            var currentAnswers = answers.GetRange(answerIndex, 3);

                            var existingAnswer = currentAnswers.FirstOrDefault(x => !x.Equals("-1"));
                            if (existingAnswer != null)
                            {
                                selectedAnswer = Convert.ToInt32(existingAnswer);
                            }
                        }
                    }
                    break;
                }
            }
            return selectedAnswer == -1 ? currentQuestion * 2 - 1 : currentQuestion * 2;
        }

        /// <summary>
        /// Gets the current sub question answer.
        /// </summary>
        /// <param name="skillsDocument">The SKLLLS document.</param>
        /// <param name="asessmentType">Type of the asessment.</param>
        /// <param name="currentQuestion">The current question.</param>
        /// <param name="assessmentQuestionOverview">The assessment question overview.</param>
        /// <returns></returns>
        public static int GetCurrentSubQuestionAnswer(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument, AssessmentType asessmentType,
            int currentQuestion, AssessmentQuestionsOverView assessmentQuestionOverview)
        {
            var subQuestion = 1;

            foreach (var dataValue in skillsDocument.DataValueKeys)
            {
                if (dataValue.Key.Equals($"{asessmentType}.Answers", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrEmpty(dataValue.Value))
                    {
                        var answers = dataValue.Value.Split(',').ToList();
                        var questionTally = 0;

                        var answerIndex = 0;
                        var rangeFromIndex = 0;
                        foreach (var questionOverview in assessmentQuestionOverview.QuestionOverViewList)
                        {
                            questionTally = questionTally + questionOverview.SubQuestions;
                            rangeFromIndex = questionOverview.SubQuestions;
                            if (currentQuestion == questionOverview.QuestionNumber)
                            {
                                break;
                            }
                            answerIndex = questionTally;
                        }
                        if (answers.Count - 1 > answerIndex)
                        {
                            var currentAnswers = answers.GetRange(answerIndex, rangeFromIndex);

                            subQuestion = currentAnswers.IndexOf("-1") + 1;
                        }
                        else
                        {
                            subQuestion = 1;
                        }
                    }
                }
            }
            return subQuestion;
        }


        /// <summary>
        /// Gets the current multiple answer question number.
        /// </summary>
        /// <param name="skillsDocument">The SKLLLS document.</param>
        /// <param name="asessmentType">Type of the asessment.</param>
        /// <returns></returns>
        public static int GetCurrentMultipleAnswerQuestionNumber(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument,
            AssessmentType asessmentType)
        {
            var currentQuestionNumber = 1;

            foreach (var dataValue in skillsDocument.DataValueKeys)
            {
                if (dataValue.Key.Equals($"{asessmentType}.Answers", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrEmpty(dataValue.Value))
                    {
                        var answers = dataValue.Value.Split(',').ToList();
                        currentQuestionNumber = answers.IndexOf("-1") + 1;
                        if (currentQuestionNumber == 0)
                        {
                            currentQuestionNumber = answers.Count + 1;
                        }
                        break;
                    }
                }
            }
            return currentQuestionNumber;
        }

        /// <summary>
        /// Gets the assessment next question number.
        /// </summary>
        /// <param name="skillsDocument">The SKLLLS document.</param>
        /// <param name="asessmentType">Type of the asessment.</param>
        /// <returns></returns>
        public static int GetAssessmentNextQuestionNumber(this DFC.SkillsCentral.Api.Domain.Models.SkillsDocument skillsDocument,
            AssessmentType asessmentType)
        {
            int nextQuestion = 0;

            foreach (var dataValue in skillsDocument.DataValueKeys)
            {
                if (dataValue.Key.Equals($"{asessmentType}.Answers", StringComparison.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrEmpty(dataValue.Value))
                    {
                        nextQuestion = 1;
                    }
                    else
                    {
                        var answers = dataValue.Value.Split(',');
                        return answers.Length + 1;
                    }
                    break;
                }
            }
            return nextQuestion;
        }

        /// <summary>
        /// Gets the type of the daat value by assessment.
        /// </summary>
        /// <param name="assessmentType">Type of the assessment.</param>
        /// <returns></returns>
        //public static string GetDataValueByAssessmentType(AssessmentType assessmentType)
        //{
        //    switch (assessmentType)
        //    {
        //        case AssessmentType.Abstract:
        //            return Constants.SkillsHealthCheck.AbstractAssessmentDataValue;
        //        case AssessmentType.Checking:
        //            return Constants.SkillsHealthCheck.CheckingAssessmentDataValue;
        //        case AssessmentType.Interest:
        //            return Constants.SkillsHealthCheck.InterestsAssessmentDataValue;
        //        case AssessmentType.Mechanical:
        //            return Constants.SkillsHealthCheck.MechanicalAssessmentDataValue;
        //        case AssessmentType.Motivation:
        //            return Constants.SkillsHealthCheck.MotivationAssessmentDataValue;
        //        case AssessmentType.Numeric:
        //            return Constants.SkillsHealthCheck.NumericAssessmentDataValue;
        //        case AssessmentType.Personal:
        //            return Constants.SkillsHealthCheck.PersonalAssessmentDataValue;
        //        case AssessmentType.SkillAreas:
        //            return Constants.SkillsHealthCheck.SkillsAssessmentDataValue;
        //        case AssessmentType.Spatial:
        //            return Constants.SkillsHealthCheck.SpatialAssessmentDataValue;
        //        case AssessmentType.Verbal:
        //            return Constants.SkillsHealthCheck.VerbalAssessmentDataValue;
        //        default:
        //            return Constants.SkillsHealthCheck.SkillsAssessmentDataValue;
        //    }
        //}

        public static string GetAnswerTotal(IEnumerable<string> modelAnswerSelection)
        {
            var questionTotal = 0;

            foreach (var selection in modelAnswerSelection)
            {
                questionTotal = questionTotal + GetTabularSelectionValue(selection);
            }

            return Convert.ToString(questionTotal);
        }

        private static int GetTabularSelectionValue(string selection)
        {
            switch (selection.ToLower())
            {
                case "a":
                    return 1;
                case "b":
                    return 2;
                case "c":
                    return 4;
                case "d":
                    return 8;
                case "e":
                    return 16;
                default:
                    return 0;
            }
        }

        public static IEnumerable<Image> GetDataImages(this string dataText, AssessmentType assessmentType)
        {
            var images = new List<Image>();

            if (string.IsNullOrWhiteSpace(dataText) ||
                dataText.IndexOf("images", StringComparison.OrdinalIgnoreCase) == -1)
            {
                return images;
            }

            try
            {
                var document = new XmlDocument();
                var assessmentTypeString = assessmentType == AssessmentType.Numeric
                    ? "Numerical"
                    : assessmentType.ToString();

                document.LoadXml(dataText);

                if (document.HasChildNodes)
                {
                    var imagesNode = document.FirstChild;

                    foreach (XmlNode node in imagesNode.ChildNodes)
                    {
                        if (node.Attributes != null)
                            images.Add(new Image
                            {
                                Alttext = node.Attributes.GetAttributeValue("alttext"),
                                Src =
                                    $"{assessmentTypeString}/{node.Attributes.GetAttributeValue("src")}",
                                Title = node.Attributes.GetAttributeValue("src")
                            });
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return images;
        }

        /// <summary>
        /// Gets the attribute value.
        /// </summary>
        /// <param name="attributeCollection">The attribute collection.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns></returns>
        public static string GetAttributeValue(this XmlAttributeCollection attributeCollection, string attributeName)
        {
            var value = string.Empty;

            var requestedAttribute = attributeCollection[attributeName];
            return requestedAttribute != null ? requestedAttribute.Value : value;
        }

        public static FeedbackQuestion GetFeedbackQuestionByAssessmentType(AssessmentType assessmentType,
            int feedbackIndex)
        {
            var feedBackQuestion = new FeedbackQuestion { AssessmentType = assessmentType };

            switch (feedbackIndex)
            {
                case 1:
                    feedBackQuestion.Question = Constants.SkillsHealthCheck.HowLongToCompleteAssessment;
                    switch (assessmentType)
                    {
                        case AssessmentType.Interest:
                        case AssessmentType.Motivation:
                        case AssessmentType.Personal:
                        case AssessmentType.SkillAreas:
                            break;
                        case AssessmentType.Abstract:
                            feedBackQuestion.FeedbackAnswers = new List<FeedbackAnswer>
                            {
                                new FeedbackAnswer
                                {
                                    AnswerText = "I did not time myself",
                                    AnswerValue = "1"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "Less than 12 mins (less than recommended time)",
                                    AnswerValue = "2"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "12 to 18 mins (about the recommended time)",
                                    AnswerValue = "3"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "18 to 25 mins (a bit more than the recommended time)",
                                    AnswerValue = "4"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "25 mins or more (much more than the recommended time)",
                                    AnswerValue = "5"
                                }
                            };
                            break;
                        case AssessmentType.Checking:
                            feedBackQuestion.FeedbackAnswers = new List<FeedbackAnswer>
                            {
                                new FeedbackAnswer
                                {
                                    AnswerText = "I did not time myself ",
                                    AnswerValue = "1"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "Less than 6 mins (less than recommended time)",
                                    AnswerValue = "2"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "6 to 9 mins (about the recommended time)",
                                    AnswerValue = "3"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "9 to 15 mins (a bit more than the recommended time)",
                                    AnswerValue = "4"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "15 mins or more (much more than the recommended time)",
                                    AnswerValue = "5"
                                }
                            };
                            break;
                        case AssessmentType.Mechanical:
                            feedBackQuestion.FeedbackAnswers = new List<FeedbackAnswer>
                            {
                                new FeedbackAnswer
                                {
                                    AnswerText = "I did not time myself ",
                                    AnswerValue = "1"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "Under 5 mins (less than recommended time)",
                                    AnswerValue = "2"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "5 to 7 mins (about the recommended time)",
                                    AnswerValue = "3"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "7 to 10 mins (a bit more than the recommended time)",
                                    AnswerValue = "4"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "10 mins or more (much more than the recommended time)",
                                    AnswerValue = "5"
                                }
                            };
                            break;
                        case AssessmentType.Numeric:
                            feedBackQuestion.FeedbackAnswers = new List<FeedbackAnswer>
                            {
                                new FeedbackAnswer
                                {
                                    AnswerText = "Under 12 mins (less than recommended time)",
                                    AnswerValue = "1"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "12 to 18 mins (about the recommended time)",
                                    AnswerValue = "2"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "18 to 25 mins (a bit more than the recommended time)",
                                    AnswerValue = "3"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "25 mins or more (much more than the recommended time)",
                                    AnswerValue = "4"
                                }
                            };
                            break;

                        case AssessmentType.Spatial:
                            feedBackQuestion.FeedbackAnswers = new List<FeedbackAnswer>
                            {
                                new FeedbackAnswer
                                {
                                    AnswerText = "I did not time myself ",
                                    AnswerValue = "1"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "Under 5 mins (less than recommended time)",
                                    AnswerValue = "2"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "5 to 7 mins (about the recommended time)",
                                    AnswerValue = "3"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "7 to 10 mins (a bit more than the recommended time)",
                                    AnswerValue = "4"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "10 mins or more (much more than the recommended time)",
                                    AnswerValue = "5"
                                }
                            };
                            break;
                        case AssessmentType.Verbal:
                            feedBackQuestion.FeedbackAnswers = new List<FeedbackAnswer>
                            {
                                new FeedbackAnswer
                                {
                                    AnswerText = "I did not time myself ",
                                    AnswerValue = "1"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "Less than 6 mins (less than recommended time)",
                                    AnswerValue = "2"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "6 to 9 mins (about the recommended time)",
                                    AnswerValue = "3"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "9 to 15 mins (a bit more than the recommended time)",
                                    AnswerValue = "3"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "15 mins or more (much more than the recommended time)",
                                    AnswerValue = "4"
                                }
                            };
                            break;
                    }
                    break;
                case 2:
                    feedBackQuestion.Question = Constants.SkillsHealthCheck.HowEasyToCompleteAssessment;
                    switch (assessmentType)
                    {
                        case AssessmentType.Interest:
                        case AssessmentType.Motivation:
                        case AssessmentType.Personal:
                        case AssessmentType.SkillAreas:
                            break;
                        case AssessmentType.Abstract:
                        case AssessmentType.Checking:
                        case AssessmentType.Mechanical:
                        case AssessmentType.Numeric:
                        case AssessmentType.Spatial:
                        case AssessmentType.Verbal:
                            feedBackQuestion.FeedbackAnswers = new List<FeedbackAnswer>
                            {
                                new FeedbackAnswer
                                {
                                    AnswerText = "Easier than expected",
                                    AnswerValue = "1"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "As expected",
                                    AnswerValue = "2"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "Harder than expected",
                                    AnswerValue = "3"
                                }
                            };
                            break;
                    }
                    break;
                case 3:
                    feedBackQuestion.Question = Constants.SkillsHealthCheck.HowEnjoyableToCompleteAssessment;
                    switch (assessmentType)
                    {
                        case AssessmentType.Interest:
                        case AssessmentType.Motivation:
                        case AssessmentType.Personal:
                        case AssessmentType.SkillAreas:
                        case AssessmentType.Verbal:
                        case AssessmentType.Numeric:
                            break;
                        case AssessmentType.Abstract:
                        case AssessmentType.Checking:
                        case AssessmentType.Mechanical:
                        case AssessmentType.Spatial:
                            feedBackQuestion.FeedbackAnswers = new List<FeedbackAnswer>
                            {
                                new FeedbackAnswer
                                {
                                    AnswerText = "I enjoyed it very much",
                                    AnswerValue = "3"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "I enjoyed it a little",
                                    AnswerValue = "2"
                                },
                                new FeedbackAnswer
                                {
                                    AnswerText = "I did not enjoy it at all",
                                    AnswerValue = "1"
                                }
                            };
                            break;
                    }
                    break;
            }

            return feedBackQuestion;
        }

    }
}
