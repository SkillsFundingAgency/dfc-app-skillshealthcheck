using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Infrastructure;
using DFC.SkillsCentral.Api.Infrastructure.Repositories;
using DfE.SkillsCentral.Api.Application.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DfE.SkillsCentral.Api.Application.Services.IntegrationTests
{
    public static class TestSetup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            SqlMapper.AddTypeHandler(new DataValuesTypeHandler());
            services.AddSingleton<IConfiguration>(config);
            services.AddScoped<DatabaseContext>();

            services.AddScoped<IAssessmentsRepository, AssessmentsRepository>();
            services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            services.AddScoped<IAnswersRepository, AnswersRepository>();
            services.AddScoped<ISkillsDocumentsRepository, SkillsDocumentsRepository>();

            services.AddScoped<IAssessmentsService, AssessmentsService>();
            services.AddScoped<ISkillsDocumentsService, SkillsDocumentsService>();        
        }
    }
}
