﻿using DFC.SkillsCentral.Api.Domain.Models;

namespace DFC.SkillsCentral.Api.Application.Interfaces.Services
{
    public interface IAssessmentsService
    {
        Task<AssessmentQuestions?> GetAssessmentQuestions(string assessmentType);

        Task<QuestionAnswers> GetSingleQuestion(int questionNumber, string assessmentType);
    }
}