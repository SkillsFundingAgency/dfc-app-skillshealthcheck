using DFC.SkillsCentral.Api.Application.Interfaces.Services;
using DfE.SkillsCentral.Api.Application.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DfE.SkillsCentral.Api.Application.Services
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAssessmentsService, AssessmentsService>();
            services.AddScoped<ISkillsDocumentsService, SkillsDocumentsService>();
            services.AddScoped<IDocumentsGenerationService, DocumentsGenerationService>();

        }
    }
}

