using Microsoft.Extensions.Configuration;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using DFC.SkillsCentral.Api.Domain.Models;
using DfE.SkillsCentral.Api.Domain.Models;
using Dapper;

namespace DfE.SkillsCentral.Api.Infrastructure.UnitTests
{
    /*
    public class UnitTest1
    {         
        

        
        //[Fact]
        //public async Task Test1()
        //{
        //    SqlMapper.AddTypeHandler(new DataValuesTypeHandler());

        //    var mockConfSection = new Mock<IConfigurationSection>();
        //    mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "DBConnection")]).Returns("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Skills;Integrated Security=True;Persist Security Info=False;Pooling=False;Multiple Active Result Sets=False;Encrypt=True;Trust Server Certificate=False;Command Timeout=0");

        //    var mockConfiguration = new Mock<IConfiguration>();
        //    mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);

        //    var repo = new SkillsDocumentsRepository(new DFC.SkillsCentral.Api.Infrastructure.DatabaseContext(mockConfiguration.Object));
        //    Console.WriteLine(mockConfiguration.Object.GetConnectionString("DBConnection")); // prints "mock value"

        //    var testDoc = new SkillsDocument
        //    {

        //        CreatedAt = DateTime.Now,
        //        CreatedBy ="larry",
                
        //            DataValueKeys = new DataValues
        //            {
        //                SkillAreas = new SkillAreas
        //                {
        //                    ExcludedJobFamilies = null,
        //                    Answers = null,
        //                    Complete = null
        //                },
        //                Interest = new Assessment
        //                {
        //                    Answers = new List<string> { "a", "b" },
        //                    Complete = false
        //                }
        //            },
                
        //        ReferenceCode = "test"
                
        //    };

        //    //var testDoc = new SkillsDocument();
        //    //testDoc.ReferenceCode = "test";
           
        //    await repo.AddAsync(testDoc);

        //    var skillsDocument = await repo.GetByReferenceCodeAsync("test");
        //    Assert.NotNull(skillsDocument);
        //}
    }
    */
}
