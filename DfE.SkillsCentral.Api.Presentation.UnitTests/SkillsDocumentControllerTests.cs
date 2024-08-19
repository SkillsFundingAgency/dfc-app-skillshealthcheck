using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Domain.Models;
using DfE.SkillsCentral.Api.Application.Services.Services;
using DfE.SkillsCentral.Api.Presentation.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DfE.SkillsCentral.Api.Presentation.UnitTests
{
    public class SkillsDocumentControllerTests
    {
        private readonly ILogger<SkillsDocumentController> logger;

        [Fact]
        public async Task GetSkillsDocumentByReferenceCode_NoCodeProvided()
        {
            //Arrange
            Mock<ISkillsDocumentsService> _skillsDocumentsService = new Mock<ISkillsDocumentsService>();

            var sut = new SkillsDocumentController(_skillsDocumentsService.Object, logger);
            var skillsDocument = new SkillsDocument();
            skillsDocument.Id = 1;
            skillsDocument.ReferenceCode = Guid.NewGuid().ToString();

            _skillsDocumentsService.Setup(x => x.GetSkillsDocumentByReferenceCode(skillsDocument.ReferenceCode)).ReturnsAsync(skillsDocument);

            //Act

            var result = await sut.GetSkillsDocumentByReferenceCode(null);

            //Assert
            var response = result.Result as ObjectResult;
            Assert.Equal(400, response?.StatusCode);
        }

        [Fact]
        public async Task GetSkillsDocumentByReferenceCode_ValidCodeProvided()
        {
            //Arrange
            Mock<ISkillsDocumentsService> _skillsDocumentsService = new Mock<ISkillsDocumentsService>();

            var sut = new SkillsDocumentController(_skillsDocumentsService.Object, logger);
            var skillsDocument = new SkillsDocument();
            skillsDocument.Id = 1;
            skillsDocument.ReferenceCode = Guid.NewGuid().ToString();

            _skillsDocumentsService.Setup(x => x.GetSkillsDocumentByReferenceCode(skillsDocument.ReferenceCode)).ReturnsAsync(skillsDocument);

            //Act

            var result = await sut.GetSkillsDocumentByReferenceCode(skillsDocument.ReferenceCode);

            //Assert
            var response = result.Result as ObjectResult;
            Assert.Equal(200, response?.StatusCode);
        }

        [Fact]
        public async Task GetSkillsDocumentByReferenceCode_InvalidCodeProvided()
        {
            //Arrange
            Mock<ISkillsDocumentsService> _skillsDocumentsService = new Mock<ISkillsDocumentsService>();

            var sut = new SkillsDocumentController(_skillsDocumentsService.Object, logger);
            var skillsDocument = new SkillsDocument();
            skillsDocument.Id = 1;
            skillsDocument.ReferenceCode = Guid.NewGuid().ToString();

            _skillsDocumentsService.Setup(x => x.GetSkillsDocumentByReferenceCode(skillsDocument.ReferenceCode)).ReturnsAsync(skillsDocument);

            //Act

            var result = await sut.GetSkillsDocumentByReferenceCode("not a guid");

            //Assert
            var response = result.Result as ObjectResult;
            Assert.Equal(400, response?.StatusCode);
        }

        [Fact]
        public async Task CreateSkillsDocument_NoDataProvided()
        {
            //Arrange
            Mock<ISkillsDocumentsService> _skillsDocumentsService = new Mock<ISkillsDocumentsService>();

            var sut = new SkillsDocumentController(_skillsDocumentsService.Object, logger);
            var skillsDocument = new SkillsDocument();
            skillsDocument.Id = 1;
            skillsDocument.ReferenceCode = Guid.NewGuid().ToString();

            _skillsDocumentsService.Setup(x => x.CreateSkillsDocument(skillsDocument)).ReturnsAsync(skillsDocument);

            //Act

            var result = await sut.CreateSkillsDocument(null);

            //Assert
            var response = result.Result as ObjectResult;
            Assert.Equal(400, response?.StatusCode);
        }

        [Fact]
        public async Task CreateSkillsDocument_ValidDataProvided()
        {
            //Arrange
            Mock<ISkillsDocumentsService> _skillsDocumentsService = new Mock<ISkillsDocumentsService>();

            var sut = new SkillsDocumentController(_skillsDocumentsService.Object, logger);
            var skillsDocument = new SkillsDocument();
            skillsDocument.Id = 1;
            skillsDocument.ReferenceCode = Guid.NewGuid().ToString();

            _skillsDocumentsService.Setup(x => x.CreateSkillsDocument(skillsDocument)).ReturnsAsync(skillsDocument);

            //Act

            var result = await sut.CreateSkillsDocument(skillsDocument);

            //Assert
            var response = result.Result as ObjectResult;
            Assert.Equal(200, response?.StatusCode);
        }

        [Fact]
        public async Task SaveSkillsDocument_NoDataProvided()
        {
            //Arrange
            Mock<ISkillsDocumentsService> _skillsDocumentsService = new Mock<ISkillsDocumentsService>();

            var sut = new SkillsDocumentController(_skillsDocumentsService.Object, logger);
            var skillsDocument = new SkillsDocument();
            skillsDocument.Id = 1;
            skillsDocument.ReferenceCode = Guid.NewGuid().ToString();

            _skillsDocumentsService.Setup(x => x.SaveSkillsDocument(skillsDocument)).ReturnsAsync(skillsDocument);

            //Act

            var result = await sut.SaveSkillsDocument(null);

            //Assert
            var response = result as ObjectResult;
            Assert.Equal(400, response?.StatusCode);
        }

        [Fact]
        public async Task SaveSkillsDocument_ValidDataProvided()
        {
            //Arrange
            Mock<ISkillsDocumentsService> _skillsDocumentsService = new Mock<ISkillsDocumentsService>();

            var sut = new SkillsDocumentController(_skillsDocumentsService.Object, logger);
            var skillsDocument = new SkillsDocument();
            skillsDocument.Id = 1;
            skillsDocument.ReferenceCode = Guid.NewGuid().ToString();

            _skillsDocumentsService.Setup(x => x.SaveSkillsDocument(skillsDocument)).ReturnsAsync(skillsDocument);

            //Act

            var result = await sut.SaveSkillsDocument(skillsDocument);

            //Assert
            var response = result as ObjectResult;
            Assert.Equal(200, response?.StatusCode);
        }
    }
}