using AutoMapper;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Services;
using FakeItEasy;
using SkillsDocumentService;
using System;
using System.Collections.Generic;
using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ServiceTests
{
    public class SkillsHealthCheckServiceTests
    {
        private IMapper _autoMapper;
        private ISkillsCentralService _skillsCentralService;
        private ISkillsHealthCheckService skillsHealthCheckService;

        public SkillsHealthCheckServiceTests()
        {
            _autoMapper = A.Fake<IMapper>();
            _skillsCentralService = A.Fake<ISkillsCentralService>();
            skillsHealthCheckService = new SkillsHealthCheckService(_autoMapper, _skillsCentralService);
        }

        [Fact]
        public void CreateSkillsDocumentResponseSuccess()
        {
            // Arrange
            var createSkillsDocumentRequest = new CreateSkillsDocumentRequest
            {
                SkillsDocument = new Services.SkillsCentral.Models.SkillsDocument
                {
                    SkillsDocumentTitle = Constants.SkillsHealthCheck.DefaultDocumentName,
                    SkillsDocumentType = Constants.SkillsHealthCheck.DocumentType,
                    CreatedBy = Constants.SkillsHealthCheck.AnonymousUser,
                    SkillsDocumentExpiry = SkillsDocumentExpiry.Physical,
                    ExpiresTimespan = new TimeSpan(0, Constants.SkillsHealthCheck.SkillsDocumentExpiryTime, 0, 0),
                },
            };
            createSkillsDocumentRequest.SkillsDocument.SkillsDocumentIdentifiers.Add(new Services.SkillsCentral.Models.SkillsDocumentIdentifier
            {
                ServiceName = Constants.SkillsHealthCheck.DocumentSystemIdentifierName,
                Value = Guid.NewGuid().ToString(),
            });

            var aCallToSkillsCentralServiceInsertDocument = A.CallTo(() => _skillsCentralService.InsertDocument(A<SkillsDocument>.Ignored));
            aCallToSkillsCentralServiceInsertDocument.Returns(123);


            // Act
            var response = skillsHealthCheckService.CreateSkillsDocument(createSkillsDocumentRequest);

            // Assert
            Assert.True(response.Success);
            Assert.Equal(123, response.DocumentId);
        }

        [Fact]
        public void CreateSkillsDocumentResponseException()
        {
            // Arrange
            var createSkillsDocumentRequest = new CreateSkillsDocumentRequest
            {
                SkillsDocument = new Services.SkillsCentral.Models.SkillsDocument
                {
                    SkillsDocumentTitle = Constants.SkillsHealthCheck.DefaultDocumentName,
                    SkillsDocumentType = Constants.SkillsHealthCheck.DocumentType,
                    CreatedBy = Constants.SkillsHealthCheck.AnonymousUser,
                    SkillsDocumentExpiry = SkillsDocumentExpiry.Physical,
                    ExpiresTimespan = new TimeSpan(0, Constants.SkillsHealthCheck.SkillsDocumentExpiryTime, 0, 0),
                },
            };
            createSkillsDocumentRequest.SkillsDocument.SkillsDocumentIdentifiers.Add(new Services.SkillsCentral.Models.SkillsDocumentIdentifier
            {
                ServiceName = Constants.SkillsHealthCheck.DocumentSystemIdentifierName,
                Value = Guid.NewGuid().ToString(),
            });

            var aCallToSkillsCentralServiceInsertDocument = A.CallTo(() => _skillsCentralService.InsertDocument(A<SkillsDocument>.Ignored));
            aCallToSkillsCentralServiceInsertDocument.Throws(new Exception("Test exception"));

            // Act
            var response = skillsHealthCheckService.CreateSkillsDocument(createSkillsDocumentRequest);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Test exception", response.ErrorMessage);
        }

        [Fact]
        public void GetAssessmentQuestionSuccess()
        {
            // Arrange
            var getAssessmentQuestionRequest = new GetAssessmentQuestionRequest
            {
                Level = Services.SkillsCentral.Enums.Level.Level1,
                Accessibility = Services.SkillsCentral.Enums.Accessibility.Accessible,
                AsessmentType = Services.SkillsCentral.Enums.AssessmentType.Abstract,
                QuestionNumber = 1,
            };
            var aCallToSkillsCentralServiceInsertDocument = A.CallTo(() => _skillsCentralService.GetSkillsHealthCheckQuestions(A<SkillsDocumentService.AssessmentType>.Ignored, A<int>.Ignored, A<SkillsDocumentService.Level>.Ignored, A<SkillsDocumentService.Accessibility>.Ignored));
            aCallToSkillsCentralServiceInsertDocument.Returns(new Question
            {
                QuestionNumber = 1,
                PossibleResponses = new List<Answer>(),
            });

            // Act
            var response = skillsHealthCheckService.GetAssessmentQuestion(getAssessmentQuestionRequest);

            // Assert
            Assert.True(response.Success);
        }

        [Fact]
        public void GetAssessmentQuestionException()
        {
            // Arrange
            var getAssessmentQuestionRequest = new GetAssessmentQuestionRequest
            {
                Level = Services.SkillsCentral.Enums.Level.Level1,
                Accessibility = Services.SkillsCentral.Enums.Accessibility.Accessible,
                AsessmentType = Services.SkillsCentral.Enums.AssessmentType.Abstract,
                QuestionNumber = 1,
            };
            var aCallToSkillsCentralServiceInsertDocument = A.CallTo(() => _skillsCentralService.GetSkillsHealthCheckQuestions(A<SkillsDocumentService.AssessmentType>.Ignored, A<int>.Ignored, A<SkillsDocumentService.Level>.Ignored, A<SkillsDocumentService.Accessibility>.Ignored));
            aCallToSkillsCentralServiceInsertDocument.Throws(new Exception("Test exception"));

            // Act
            var response = skillsHealthCheckService.GetAssessmentQuestion(getAssessmentQuestionRequest);

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Test exception", response.ErrorMessage);
        }
    }
}
