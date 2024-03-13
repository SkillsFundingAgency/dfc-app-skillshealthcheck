using Dapper;
using DfE.SkillsCentral.Api.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DfE.SkillsCentral.Api.Application.Services.IntegrationTests;

public static class TestSetup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        services.AddSingleton<IConfiguration>(config);

        services.AddInfrastructureServices();
        services.AddApplicationServices();
    }
}