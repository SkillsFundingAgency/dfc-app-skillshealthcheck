using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using FakeItEasy;
using System.Threading.Tasks;
using Xunit;

namespace DFC.App.SkillsHealthCheck.Services.UnitTests
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

        [Fact]
        public async Task GetFormatterWithPdfDocumentTypeReturnsPdfFormatter()
        {
            // Arrange

            // Act
            var formatter = yourAssessmentsService.GetFormatter(SkillsCentral.Enums.DownloadType.Pdf);

            // Assert
            Assert.Equal(formatter.Title, DocumentTitle.Pdf);
            Assert.Equal(formatter.FileExtension, DocumentFileExtensions.Pdf);
            Assert.Equal(formatter.ContentType, DocumentContentTypes.Pdf);
            Assert.Equal(formatter.FormatterName, DocumentFormatName.ShcFullPdfFormatter);
        }

        [Fact]
        public async Task GetFormatterWithWordDocumentTypeReturnsWordFormatter()
        {
            // Arrange

            // Act
            var formatter = yourAssessmentsService.GetFormatter(SkillsCentral.Enums.DownloadType.Word);

            // Assert
            Assert.Equal(formatter.Title, DocumentTitle.Word);
            Assert.Equal(formatter.FileExtension, DocumentFileExtensions.Docx);
            Assert.Equal(formatter.ContentType, DocumentContentTypes.Docx);
            Assert.Equal(formatter.FormatterName, DocumentFormatName.ShcFullDocxFormatter);
        }
    }
}
