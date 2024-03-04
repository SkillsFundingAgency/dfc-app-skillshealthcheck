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

       
        public SkillsDocumentsServiceTests() 
        {


            
        }
        [Fact]
        public async Task GetSkillsDocument_ShouldReturnSkillsDocument_WhenValidIdProvided()
        {
      
        }


        [Fact]
        public async Task GetSkillsDocument_ShouldReturnDefault_WhenInvalidIdProvided()
        {

        }

        [Fact]
        public async Task GetSkillsDocumentByReferenceCode_ShouldReturnSkillsDocument_WhenValidReferenceCodeProvided()
        {

        }


        [Fact]
        public async Task GetSkillsDocumentByReferenceCode_ShouldReturnDefault_WhenInvalidReferenceCodeProvided()
        {

        }
    }
}