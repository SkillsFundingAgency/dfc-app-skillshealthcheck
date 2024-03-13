using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DFC.SkillsCentral.Api.Application.Interfaces.Repositories;
using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DFC.SkillsCentral.Api.Infrastructure.Repositories;
using DFC.SkillsCentral.Api.Infrastructure;
using DfE.SkillsCentral.Api.Application.Interfaces.Repositories;
using DfE.SkillsCentral.Api.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DfE.SkillsCentral.Api.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            SqlMapper.AddTypeHandler(new DictionaryTypeHandler());
            services.AddScoped<DatabaseContext>();

            services.AddScoped<IAssessmentsRepository, AssessmentsRepository>();
            services.AddScoped<IQuestionsRepository, QuestionsRepository>();
            services.AddScoped<IAnswersRepository, AnswersRepository>();
            services.AddScoped<ISkillsDocumentsRepository, SkillsDocumentsRepository>();
            services.AddScoped<IJobFamiliesRepository, JobFamiliesRepository>();
        }
    }
}
