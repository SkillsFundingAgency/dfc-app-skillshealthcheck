//using AutoMapper;
//using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
//using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
//using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
//using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Services;
//using FakeItEasy;
//using SkillsDocumentService;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Xunit;

//namespace DFC.App.SkillsHealthCheck.UnitTests.ServiceTests
//{
//    public class SkillsHealthCheckServiceTests
//    {
//        private IMapper _autoMapper;
//        private ISkillsCentralService _skillsCentralService;
//        private ISkillsHealthCheckService skillsHealthCheckService;

//        public SkillsHealthCheckServiceTests()
//        {
//            _autoMapper = A.Fake<IMapper>();
//            _skillsCentralService = A.Fake<ISkillsCentralService>();
//            skillsHealthCheckService = new SkillsHealthCheckService(_autoMapper, _skillsCentralService);
//        }

//        [Fact]
//        public void CreateSkillsDocumentResponseSuccess()
//        {
//            // Arrange
//            var createSkillsDocumentRequest = new CreateSkillsDocumentRequest
//            {
//                SkillsDocument = new Services.SkillsCentral.Models.SkillsDocument
//                {
//                    SkillsDocumentTitle = Constants.SkillsHealthCheck.DefaultDocumentName,
//                    SkillsDocumentType = Constants.SkillsHealthCheck.DocumentType,
//                    CreatedBy = Constants.SkillsHealthCheck.AnonymousUser,
//                    SkillsDocumentExpiry = SkillsDocumentExpiry.Physical,
//                    ExpiresTimespan = new TimeSpan(0, Constants.SkillsHealthCheck.SkillsDocumentExpiryTime, 0, 0),
//                },
//            };
//            createSkillsDocumentRequest.SkillsDocument.SkillsDocumentIdentifiers.Add(new Services.SkillsCentral.Models.SkillsDocumentIdentifier
//            {
//                ServiceName = Constants.SkillsHealthCheck.DocumentSystemIdentifierName,
//                Value = Guid.NewGuid().ToString(),
//            });

//            var aCallToSkillsCentralServiceInsertDocument = A.CallTo(() => _skillsCentralService.InsertDocument(A<SkillsDocument>.Ignored));
//            aCallToSkillsCentralServiceInsertDocument.Returns(123);


//            // Act
//            var response = skillsHealthCheckService.CreateSkillsDocument(createSkillsDocumentRequest);

//            // Assert
//            Assert.True(response.Success);
//            Assert.Equal(123, response.DocumentId);
//        }

//        [Fact]
//        public void CreateSkillsDocumentResponseException()
//        {
//            // Arrange
//            var createSkillsDocumentRequest = new CreateSkillsDocumentRequest
//            {
//                SkillsDocument = new Services.SkillsCentral.Models.SkillsDocument
//                {
//                    SkillsDocumentTitle = Constants.SkillsHealthCheck.DefaultDocumentName,
//                    SkillsDocumentType = Constants.SkillsHealthCheck.DocumentType,
//                    CreatedBy = Constants.SkillsHealthCheck.AnonymousUser,
//                    SkillsDocumentExpiry = SkillsDocumentExpiry.Physical,
//                    ExpiresTimespan = new TimeSpan(0, Constants.SkillsHealthCheck.SkillsDocumentExpiryTime, 0, 0),
//                },
//            };
//            createSkillsDocumentRequest.SkillsDocument.SkillsDocumentIdentifiers.Add(new Services.SkillsCentral.Models.SkillsDocumentIdentifier
//            {
//                ServiceName = Constants.SkillsHealthCheck.DocumentSystemIdentifierName,
//                Value = Guid.NewGuid().ToString(),
//            });

//            var aCallToSkillsCentralServiceInsertDocument = A.CallTo(() => _skillsCentralService.InsertDocument(A<SkillsDocument>.Ignored));
//            aCallToSkillsCentralServiceInsertDocument.Throws(new Exception("Test exception"));

//            // Act
//            var response = skillsHealthCheckService.CreateSkillsDocument(createSkillsDocumentRequest);

//            // Assert
//            Assert.False(response.Success);
//            Assert.Equal("Test exception", response.ErrorMessage);
//        }

//        [Fact]
//        public void GetAssessmentQuestionSuccess()
//        {
//            // Arrange
//            var getAssessmentQuestionRequest = new GetAssessmentQuestionRequest
//            {
//                Level = Services.SkillsCentral.Enums.Level.Level1,
//                Accessibility = Services.SkillsCentral.Enums.Accessibility.Accessible,
//                AsessmentType = Services.SkillsCentral.Enums.AssessmentType.Abstract,
//                QuestionNumber = 1,
//            };
//            var aCallToSkillsCentralServiceInsertDocument = A.CallTo(() => _skillsCentralService.GetSkillsHealthCheckQuestions(A<SkillsDocumentService.AssessmentType>.Ignored, A<int>.Ignored, A<SkillsDocumentService.Level>.Ignored, A<SkillsDocumentService.Accessibility>.Ignored));
//            aCallToSkillsCentralServiceInsertDocument.Returns(new Question
//            {
//                QuestionNumber = 1,
//                PossibleResponses = new List<Answer>(),
//            });

//            // Act
//            var response = skillsHealthCheckService.GetAssessmentQuestion(getAssessmentQuestionRequest);

//            // Assert
//            Assert.True(response.Success);
//        }

//        [Fact]
//        public void GetAssessmentQuestionException()
//        {
//            // Arrange
//            var getAssessmentQuestionRequest = new GetAssessmentQuestionRequest
//            {
//                Level = Services.SkillsCentral.Enums.Level.Level1,
//                Accessibility = Services.SkillsCentral.Enums.Accessibility.Accessible,
//                AsessmentType = Services.SkillsCentral.Enums.AssessmentType.Abstract,
//                QuestionNumber = 1,
//            };
//            var aCallToSkillsCentralServiceInsertDocument = A.CallTo(() => _skillsCentralService.GetSkillsHealthCheckQuestions(A<SkillsDocumentService.AssessmentType>.Ignored, A<int>.Ignored, A<SkillsDocumentService.Level>.Ignored, A<SkillsDocumentService.Accessibility>.Ignored));
//            aCallToSkillsCentralServiceInsertDocument.Throws(new Exception("Test exception"));

//            // Act
//            var response = skillsHealthCheckService.GetAssessmentQuestion(getAssessmentQuestionRequest);

//            // Assert
//            Assert.False(response.Success);
//            Assert.Equal("Test exception", response.ErrorMessage);
//        }

//        [Fact]
//        public async Task RequestDownloadAsyncSuccess()
//        {
//            // Arrange
//            var aCallToSkillsCentralServiceFormatDocumentMakeRequestAsync = A.CallTo(() => _skillsCentralService.FormatDocumentMakeRequestAsync(A<long>.Ignored, A<string>.Ignored, A<string>.Ignored));
//            aCallToSkillsCentralServiceFormatDocumentMakeRequestAsync.Returns(new FormatDocumentResponse
//            {
//                Status = FormatDocumentStatusEnum.Pending,
//            });

//            // Act
//            var response = await skillsHealthCheckService.RequestDownloadAsync(123, "pdf", "anonymous");

//            // Assert
//            Assert.Equal(DocumentStatus.Pending, response);
//        }

//        [Fact]
//        public async Task RequestDownloadAsyncFailsWithInvalidParams()
//        {
//            // Arrange
//            // Act

//            // Assert
//            var ex1 = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await skillsHealthCheckService.RequestDownloadAsync(0, "pdf", "anonymous"));
//            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'documentId')", ex1.Message);

//            var ex2 = await Assert.ThrowsAsync<ArgumentNullException>(async () => await skillsHealthCheckService.RequestDownloadAsync(1, string.Empty, "anonymous"));
//            Assert.Equal("Value cannot be null. (Parameter 'formatter')", ex2.Message);

//            var ex3 = await Assert.ThrowsAsync<ArgumentNullException>(async () => await skillsHealthCheckService.RequestDownloadAsync(1, "pdf", string.Empty));
//            Assert.Equal("Value cannot be null. (Parameter 'requestedBy')", ex3.Message);
//        }

//        [Fact]
//        public async Task QueryDownloadStatusAsyncSuccess()
//        {
//            // Arrange
//            var aCallToSkillsCentralServiceFFormatDocumentPollStatusAsync = A.CallTo(() => _skillsCentralService.FormatDocumentPollStatusAsync(A<long>.Ignored, A<string>.Ignored));
//            aCallToSkillsCentralServiceFFormatDocumentPollStatusAsync.Returns(new FormatDocumentResponse
//            {
//                Status = FormatDocumentStatusEnum.Creating,
//            });

//            // Act
//            var response = await skillsHealthCheckService.QueryDownloadStatusAsync(123, "pdf");

//            // Assert
//            Assert.Equal(DocumentStatus.Creating, response);
//        }

//        [Fact]
//        public async Task QueryDownloadStatusAsyncFailsWithInvalidParams()
//        {
//            // Arrange
//            // Act

//            // Assert
//            var ex1 = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () => await skillsHealthCheckService.QueryDownloadStatusAsync(0, "pdf"));
//            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'documentId')", ex1.Message);

//            var ex2 = await Assert.ThrowsAsync<ArgumentNullException>(async () => await skillsHealthCheckService.QueryDownloadStatusAsync(1, string.Empty));
//            Assert.Equal("Value cannot be null. (Parameter 'formatter')", ex2.Message);
//        }
//    }
//}
