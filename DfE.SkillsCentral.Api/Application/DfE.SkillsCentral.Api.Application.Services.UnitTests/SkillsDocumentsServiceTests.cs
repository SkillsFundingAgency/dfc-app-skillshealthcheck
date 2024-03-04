using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Domain.Models;
using DFC.SkillsCentral.Api.Infrastructure.Repositories;
using DfE.SkillsCentral.Api.Application.Interfaces.Models;
using DfE.SkillsCentral.Api.Application.Services.Services;
using Moq;

namespace DfE.SkillsCentral.Api.Application.Services.UnitTests
{
    public class SkillsDocumentsServiceTests
    {
        private readonly Mock<ISkillsDocumentsRepository> _skillsDocumentsRepository = new Mock<ISkillsDocumentsRepository>();
        private readonly SkillsDocumentsService _documentsService;
        readonly int validId = 1;
        readonly int invalidId = 2;
        static readonly string validReferenceCode = "valid";
        readonly string invalidReferenceCode = "invalid";
        SkillsDocument skillsDocument = new SkillsDocument() { ReferenceCode = validReferenceCode};
       
        public SkillsDocumentsServiceTests() 
        {
            _skillsDocumentsRepository.Setup(x => x.GetByIdAsync(validId)).ReturnsAsync(skillsDocument);
            _skillsDocumentsRepository.Setup(x => x.GetByIdAsync(invalidId)).ReturnsAsync(default(SkillsDocument));
            _skillsDocumentsRepository.Setup(x => x.GetByReferenceCodeAsync(validReferenceCode)).ReturnsAsync(skillsDocument);
            _skillsDocumentsRepository.Setup(x => x.GetByReferenceCodeAsync(invalidReferenceCode)).ReturnsAsync(default(SkillsDocument));

            _documentsService = new SkillsDocumentsService(_skillsDocumentsRepository.Object);

        }
        [Fact]
        public async Task GetSkillsDocument_ShouldReturnSkillsDocument_WhenValidIdProvided()
        {
            var document = await _documentsService.GetSkillsDocument(validId);
            Assert.True(document.Equals(skillsDocument));
        }


        [Fact]
        public async Task GetSkillsDocument_ShouldReturnDefault_WhenInvalidIdProvided()
        {
            var document = await _documentsService.GetSkillsDocument(invalidId);
            Assert.Null(document);
        }

        [Fact]
        public async Task GetSkillsDocumentByReferenceCode_ShouldReturnSkillsDocument_WhenValidReferenceCodeProvided()
        {
            var document = await _documentsService.GetSkillsDocumentByReferenceCode(validReferenceCode);
            Assert.True(document.Equals(skillsDocument));
        }


        [Fact]
        public async Task GetSkillsDocumentByReferenceCode_ShouldReturnDefault_WhenInvalidReferenceCodeProvided()
        {
            var document = await _documentsService.GetSkillsDocumentByReferenceCode(invalidReferenceCode);
            Assert.Null(document);
        }

        [Fact]
        public async Task CreateSkillsDocument_ShouldReturnSkillsDocument_WhenSuccessful()
        {
            var document = await _documentsService.CreateSkillsDocument(skillsDocument);
            Assert.True(document.Equals(skillsDocument));
        }

    }
}