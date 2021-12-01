using System.Diagnostics.CodeAnalysis;
using System.ServiceModel;

using AutoMapper;

using DFC.App.SkillsHealthCheck.Data.Contracts;
using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Filters;
using DFC.App.SkillsHealthCheck.HostedServices;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services;
using DFC.App.SkillsHealthCheck.Services.CacheContentService;
using DFC.App.SkillsHealthCheck.Services.GovNotify;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Services;
using DFC.Compui.Cosmos;
using DFC.Compui.Cosmos.Contracts;
using DFC.Compui.Sessionstate;
using DFC.Compui.Subscriptions.Pkg.Netstandard.Extensions;
using DFC.Compui.Telemetry;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;
using DFC.Content.Pkg.Netcore.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SkillsDocumentService;

namespace DFC.App.SkillsHealthCheck
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string CosmosDbSharedContentConfigAppSettings = "Configuration:CosmosDbConnections:SharedContent";
        private const string CosmosDbSessionStateConfigAppSettings = "Configuration:CosmosDbConnections:SessionState";

        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper mapper)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();

                // add the default route
                endpoints.MapControllerRoute("default", "{controller=Health}/{action=Ping}");
            });
            mapper?.ConfigurationProvider.AssertConfigurationIsValid();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(configuration.GetSection(nameof(CmsApiClientOptions)).Get<CmsApiClientOptions>() ?? new CmsApiClientOptions());
            services.Configure<SkillsServiceOptions>(configuration.GetSection(nameof(SkillsServiceOptions)));
            services.Configure<GovNotifyOptions>(configuration.GetSection(nameof(GovNotifyOptions)));
            services.Configure<SessionStateOptions>(configuration.GetSection(nameof(SessionStateOptions)));

            var cosmosRetryOptions = new RetryOptions { MaxRetryAttemptsOnThrottledRequests = 20, MaxRetryWaitTimeInSeconds = 60 };
            var cosmosDbConnectionSharedContent = configuration.GetSection(CosmosDbSharedContentConfigAppSettings).Get<CosmosDbConnection>();
            var cosmosDbConnectionSessionState = configuration.GetSection(CosmosDbSessionStateConfigAppSettings).Get<CosmosDbConnection>();
            services.AddDocumentServices<SharedContentItemModel>(cosmosDbConnectionSharedContent, env.IsDevelopment(), cosmosRetryOptions);
            services.AddSessionStateServices<SessionDataModel>(cosmosDbConnectionSessionState, env.IsDevelopment());

            services.AddApplicationInsightsTelemetry();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddHostedServiceTelemetryWrapper();
            services.AddHostedService<SharedContentCacheReloadBackgroundService>();
            services.AddSubscriptionBackgroundService(configuration);

            var policyRegistry = services.AddPolicyRegistry();

            services.AddApiServices(configuration, policyRegistry);

            RegisterSkillsHealthCheckServices(services);

            services.AddMvc(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            })
                .AddNewtonsoftJson()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        private void RegisterSkillsHealthCheckServices(IServiceCollection services)
        {
            services.AddTransient<ISharedContentCacheReloadService, SharedContentCacheReloadService>();
            services.AddTransient<IWebhooksService, WebhooksService>();
            services.AddTransient<ISkillsCentralService>(sp =>
            {
                var svc = new SkillsCentralServiceClient();
                svc.ChannelFactory.Endpoint.Address = new EndpointAddress(configuration.GetValue<string>("SkillsCentralServiceEndpoint"));
                return svc;
            });
            services.AddTransient<ISkillsHealthCheckService, SkillsHealthCheckService>();
            services.AddTransient<IYourAssessmentsService, YourAssessmentsService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IGovUkNotifyClientProxy, GovUkNotifyClientProxy>();
            services.AddTransient<IGovNotifyService, GovNotifyService>();
            services.AddScoped<SessionStateFilter>();
        }
    }
}