using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.SkillsCentral.Api.Domain.Models;
using FakeItEasy;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ServiceTests
{
    public class YourAssessmentsServiceTests
    {
        private readonly IYourAssessmentsService yourAssessmentsService;
        private readonly ISkillsHealthCheckService skillsHealthCheckService;
        private readonly IQuestionService questionService;

        public YourAssessmentsServiceTests()
        {
            skillsHealthCheckService = A.Fake<ISkillsHealthCheckService>();
            questionService = A.Fake<QuestionService>();
            yourAssessmentsService = new YourAssessmentsService(skillsHealthCheckService, questionService);
        }

        /*[Fact]
        public void GetFormatterWithPdfDocumentTypeReturnsPdfFormatter()
        {
            // Arrange

            // Act
            var formatter = yourAssessmentsService.GetFormatter(Services.SkillsCentral.Enums.DownloadType.Pdf);

            // Assert
            Assert.Equal(formatter.Title, DocumentTitle.Pdf);
            Assert.Equal(formatter.FileExtension, DocumentFileExtensions.Pdf);
            Assert.Equal(formatter.ContentType, DocumentContentTypes.Pdf);
            Assert.Equal(formatter.FormatterName, DocumentFormatName.ShcFullPdfFormatter);
        }

        [Fact]
        public void GetFormatterWithWordDocumentTypeReturnsWordFormatter()
        {
            // Arrange

            // Act
            var formatter = yourAssessmentsService.GetFormatter(Services.SkillsCentral.Enums.DownloadType.Word);

            // Assert
            Assert.Equal(formatter.Title, DocumentTitle.Word);
            Assert.Equal(formatter.FileExtension, DocumentFileExtensions.Docx);
            Assert.Equal(formatter.ContentType, DocumentContentTypes.Docx);
            Assert.Equal(formatter.FormatterName, DocumentFormatName.ShcFullDocxFormatter);
        }

        [Fact]
        public async Task GetDownloadDocumentAsyncForReturnSuccess()
        {
            // Arrange
            var formatter = yourAssessmentsService.GetFormatter(DownloadType.Pdf);
            var sessionDataModel = new SessionDataModel
            {
                DocumentId = 123,
            };
            var aCallToSHCServiceGetSkillsDocument = A.CallTo(() => skillsHealthCheckService.GetSkillsDocument((int)sessionDataModel.DocumentId));
            aCallToSHCServiceGetSkillsDocument.Returns(new SkillsDocument());
            var aCallToSHCServicevarDownloadDocument = A.CallTo(() => skillsHealthCheckService.GenerateWordDoc((int)sessionDataModel.DocumentId));
            aCallToSHCServicevarRequestDownloadAsync.Returns(DocumentStatus.Pending);
            aCallToSHCServicevarQueryDownloadStatusAsync.Returns(DocumentStatus.Creating).Once().Then.Returns(DocumentStatus.Created);
            aCallToSHCServicevarDownloadDocument.Returns(new DownloadDocumentResponse
            {
                Success = true,
                DocumentBytes = new byte[] { },
            });

            // Act
            //var response = await yourAssessmentsService.GetDownloadDocumentAsync(sessionDataModel, formatter, new List<string>());
            var responseTask = Task.FromResult(response);

            // Assert
            Assert.True(response != null);
            aCallToSHCServiceGetSkillsDocument.MustHaveHappenedOnceExactly();
            aCallToSHCServicevarRequestDownloadAsync.MustHaveHappenedOnceExactly();
            aCallToSHCServicevarQueryDownloadStatusAsync.MustHaveHappenedTwiceExactly();
            aCallToSHCServicevarDownloadDocument.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetDownloadDocumentAsyncForReturnError()
        {
            // Arrange
            var formatter = yourAssessmentsService.GetFormatter(DownloadType.Pdf);
            var sessionDataModel = new SessionDataModel
            {
                DocumentId = 123,
            };
            var aCallToSHCServiceGetSkillsDocument = A.CallTo(() => skillsHealthCheckService.GetSkillsDocument(A<GetSkillsDocumentRequest>.Ignored));
            aCallToSHCServiceGetSkillsDocument.Returns(new GetSkillsDocumentResponse
            {
                Success = true,
                SkillsDocument = new SkillsDocument
                {
                    SkillsDocumentTitle = "Skills Health Check",
                    SkillsDocumentDataValues = new List<SkillsDocumentDataValue>(),
                },
            });

            var aCallToSHCServicevarRequestDownloadAsync = A.CallTo(() => skillsHealthCheckService.RequestDownloadAsync(A<long>.Ignored, A<string>.Ignored, A<string>.Ignored));
            var aCallToSHCServiceSaveQuestionAnswer = A.CallTo(() => skillsHealthCheckService.SaveQuestionAnswer(A<SaveQuestionAnswerRequest>.Ignored));
            aCallToSHCServicevarRequestDownloadAsync.Returns(DocumentStatus.Error);
            aCallToSHCServiceSaveQuestionAnswer.Returns(new SaveQuestionAnswerResponse
            {
                Success = true,
            });

            // Act
            var response = await yourAssessmentsService.GetDownloadDocumentAsync(sessionDataModel, formatter, new List<string>());

            // Assert
            Assert.False(response.Success);
            Assert.Equal("Skills Health Check", response.DocumentName);
            aCallToSHCServiceGetSkillsDocument.MustHaveHappenedOnceExactly();
            aCallToSHCServicevarRequestDownloadAsync.MustHaveHappenedTwiceExactly();
            aCallToSHCServiceSaveQuestionAnswer.MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GetSkillsDocumentIDByReferenceAndStoreSuccess()
        {
            // Arrange
            var sessionDataModel = new SessionDataModel
            {
                DocumentId = 123,
            };

            var aCallToSHCServiceGetSkillsDocumentByIdentifier = A.CallTo(() => skillsHealthCheckService.GetSkillsDocumentByIdentifier(A<string>.Ignored));
            aCallToSHCServiceGetSkillsDocumentByIdentifier.Returns(new GetSkillsDocumentIdResponse
            {
                Success = true,
                DocumentId = 456,
            });

            // Act
            var response = await yourAssessmentsService.GetSkillsDocumentIDByReferenceAndStore(sessionDataModel, "abc");

            // Assert
            Assert.True(response);
            Assert.Equal(456, sessionDataModel.DocumentId);
        }

        [Fact]
        public async Task GetSkillsDocumentIDByReferenceAndStoreError()
        {
            // Arrange
            var sessionDataModel = new SessionDataModel
            {
                DocumentId = 123,
            };

            var aCallToSHCServiceGetSkillsDocumentByIdentifier = A.CallTo(() => skillsHealthCheckService.GetSkillsDocumentByReferenceCode(A<string>.Ignored));
            aCallToSHCServiceGetSkillsDocumentByIdentifier.Returns(new GetSkillsDocumentIdResponse
            {
                Success = false,
            });

            // Act
            var response = await yourAssessmentsService.GetSkillsDocumentIDByReferenceAndStore(sessionDataModel, "abc");

            // Assert
            Assert.False(response);
            Assert.Equal(123, sessionDataModel.DocumentId);
        }

        [Fact]
        public async Task GetSkillsDocumentIDByReferenceAndStoreInvalidDocId()
        {
            // Arrange
            var sessionDataModel = new SessionDataModel
            {
                DocumentId = 123,
            };

            var aCallToSHCServiceGetSkillsDocumentByIdentifier = A.CallTo(() => skillsHealthCheckService.GetSkillsDocumentByIdentifier(A<string>.Ignored));
            aCallToSHCServiceGetSkillsDocumentByIdentifier.Returns(new GetSkillsDocumentIdResponse
            {
                Success = true,
                DocumentId = 0,
            });

            // Act
            var response = await yourAssessmentsService.GetSkillsDocumentIDByReferenceAndStore(sessionDataModel, "abc");

            // Assert
            Assert.False(response);
            Assert.Equal(123, sessionDataModel.DocumentId);
        }

        [Fact]
        public void GetAssessmentListViewModelSkillsAssessmentComplete()
        {
            // Arrange
            var documentId = 123;
            var aCallToSHCServiceGetSkillsDocument = A.CallTo(() => skillsHealthCheckService.GetSkillsDocument(A<GetSkillsDocumentRequest>.Ignored));
            aCallToSHCServiceGetSkillsDocument.Returns(new GetSkillsDocumentResponse
            {
                Success = true,
                SkillsDocument = new SkillsDocument
                {
                    CreatedAt = new System.DateTime(2021,1,1),
                    DocumentId = documentId,
                    SkillsDocumentTitle = "Skills Health Check",
                    SkillsDocumentDataValues = new List<SkillsDocumentDataValue>
                    {
                        new SkillsDocumentDataValue
                        {
                            Title = "SkillAreas.Complete",
                            Value = "true",
                        },
                        new SkillsDocumentDataValue
                        {
                            Title = "Motivation.Complete",
                            Value = "false",
                        },
                    },
                },
            });

            // Act
            var viewModel = yourAssessmentsService.GetAssessmentListViewModel(documentId, new List<string>());

            // Assert
            Assert.True(viewModel.SkillsAssessmentComplete);
            Assert.Equal(6, viewModel.AssessmentsActivity.Count);
            Assert.Equal(4, viewModel.AssessmentsPersonal.Count);
            Assert.Equal(1, viewModel.AssessmentsCompleted.Count);
            Assert.Equal(1, viewModel.AssessmentsStarted.Count);
        }
        /*[Fact]
        public void GetAssessmentListViewModelSkillsAssessmentNoComplete()
        {
            // Arrange
            var documentId = 123;
            var aCallToSHCServiceGetSkillsDocument = A.CallTo(() => skillsHealthCheckService.GetSkillsDocument(A<GetSkillsDocumentRequest>.Ignored));
            aCallToSHCServiceGetSkillsDocument.Returns(new GetSkillsDocumentResponse
            {
                Success = true,
                SkillsDocument = new SkillsDocument
                {
                    CreatedAt = new System.DateTime(2021, 1, 1),
                    DocumentId = documentId,
                    SkillsDocumentTitle = "Skills Health Check",
                    SkillsDocumentDataValues = new List<SkillsDocumentDataValue>
                    {
                        new SkillsDocumentDataValue
                        {
                            Title = "SkillAreas.Complete",
                            Value = "false",
                        },
                        new SkillsDocumentDataValue
                        {
                            Title = "Motivation.Complete",
                            Value = "false",
                        },
                    },
                },
            });

            // Act
            var viewModel = yourAssessmentsService.GetAssessmentListViewModel(documentId, new List<string>());

            // Assert
            Assert.False(viewModel.SkillsAssessmentComplete);
            Assert.Equal(6, viewModel.AssessmentsActivity.Count);
            Assert.Equal(4, viewModel.AssessmentsPersonal.Count);
            Assert.Equal(0, viewModel.AssessmentsCompleted.Count);
            Assert.Equal(2, viewModel.AssessmentsStarted.Count);
        }*/

        /*[Fact]
        public void GetAssessmentListViewModelError()
        {
            // Arrange
            var documentId = 123;
            var aCallToSHCServiceGetSkillsDocument = A.CallTo(() => skillsHealthCheckService.GetSkillsDocument(documentId));
            aCallToSHCServiceGetSkillsDocument.Returns(null);

            // Act
            var viewModel = yourAssessmentsService.GetAssessmentListViewModel(documentId, new List<string>());

            // Assert
            Assert.True(viewModel.InValidDocumentId);
            Assert.True(viewModel.IsAPiError);
        }*/
    }
}
