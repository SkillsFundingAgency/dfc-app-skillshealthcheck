using AutoMapper;
using DFC.App.SkillsHealthCheck.Filters;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services;
using DFC.App.SkillsHealthCheck.Services.GovNotify;
using DFC.App.SkillsHealthCheck.Services.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentralAPI;
using DFC.App.SkillsHealthCheck.Services.SkillsCentralAPI.Services;
using DFC.Common.SharedContent.Pkg.Netcore;
using DFC.Common.SharedContent.Pkg.Netcore.Infrastructure;
using DFC.Common.SharedContent.Pkg.Netcore.Infrastructure.Strategy;
using DFC.Common.SharedContent.Pkg.Netcore.Interfaces;
using DFC.Common.SharedContent.Pkg.Netcore.Model.ContentItems.SharedHtml;
using DFC.Common.SharedContent.Pkg.Netcore.RequestHandler;
using DFC.Compui.Cosmos.Contracts;
using DFC.Compui.Sessionstate;
using DFC.Compui.Subscriptions.Pkg.Netstandard.Extensions;
using DFC.Compui.Telemetry;
using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.ServiceModel;
using Microsoft.Extensions.Logging;
using System.Threading;
using Microsoft.Extensions.Caching.Memory;

namespace DFC.App.SkillsHealthCheck
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        private const string CosmosDbSessionStateConfigAppSettings = "Configuration:CosmosDbConnections:SessionState";
        private const string RedisCacheConnectionStringAppSettings = "Cms:RedisCacheConnectionString";
        private const string GraphApiUrlAppSettings = "Cms:GraphApiUrl";
        private const string WorkerThreadsConfigAppSettings = "ThreadSettings:WorkerThreads";
        private const string IocpThreadsConfigAppSettings = "ThreadSettings:IocpThreads";
        private IConfiguration configuration;
        private IWebHostEnvironment env;
        private readonly ILogger<Startup> logger;

        public Startup(IConfiguration configuration, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            this.configuration = configuration;
            this.env = env;
            this.logger = logger;
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
            ConfigureMinimumThreads();
            services.Configure<SkillsServiceOptions>(configuration.GetSection(nameof(SkillsServiceOptions)));
            services.Configure<GovNotifyOptions>(configuration.GetSection(nameof(GovNotifyOptions)));
            services.Configure<SessionStateOptions>(configuration.GetSection(nameof(SessionStateOptions)));
            services.AddStackExchangeRedisCache(options => { options.Configuration = configuration.GetSection(RedisCacheConnectionStringAppSettings).Get<string>(); });

            services.AddHttpClient();
            var graphQLConnection = configuration["Cms:GraphApiUrl"];
            services.AddSingleton<IGraphQLClient>(s =>
            {
                var option = new GraphQLHttpClientOptions()
                {
                    EndPoint = new Uri(graphQLConnection ?? throw new ArgumentNullException()),
                    HttpMessageHandler = new CmsRequestHandler(
                        s.GetService<IHttpClientFactory>(),
                        s.GetService<IConfiguration>(),
                        s.GetService<IHttpContextAccessor>(),
                        s.GetService<IMemoryCache>()),
                };
                var client = new GraphQLHttpClient(option, new NewtonsoftJsonSerializer());
                return client;
            });
            services.AddSingleton<ISharedContentRedisInterfaceStrategy<SharedHtml>, SharedHtmlQueryStrategy>();

            services.AddSingleton<ISharedContentRedisInterfaceStrategyFactory, SharedContentRedisStrategyFactory>();
            services.AddScoped<ISharedContentRedisInterface, SharedContentRedis>();
            services.Configure<SkillsCentralSettings>(configuration.GetSection(nameof(SkillsCentralSettings)));

            var cosmosRetryOptions = new RetryOptions { MaxRetryAttemptsOnThrottledRequests = 20, MaxRetryWaitTimeInSeconds = 60 };
            var cosmosDbConnectionSessionState = configuration.GetSection(CosmosDbSessionStateConfigAppSettings).Get<CosmosDbConnection>();
            services.AddSessionStateServices<SessionDataModel>(cosmosDbConnectionSessionState, env.IsDevelopment());
            services.AddSingleton<RestClient>(new RestClient(new RestClientOptions()));

            services.AddApplicationInsightsTelemetry();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddHostedServiceTelemetryWrapper();
            services.AddSubscriptionBackgroundService(configuration);

            var policyRegistry = services.AddPolicyRegistry();

            RegisterSkillsHealthCheckServices(services);

            services.AddMvc(config =>
            {
                config.RespectBrowserAcceptHeader = true;
                config.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson();
        }

        private void RegisterSkillsHealthCheckServices(IServiceCollection services)
        {
            services.AddTransient<ISkillsHealthCheckService, SkillsHealthCheckService>();
            services.AddTransient<IYourAssessmentsService, YourAssessmentsService>();
            services.AddTransient<IQuestionService, QuestionService>();
            services.AddTransient<IGovUkNotifyClientProxy, GovUkNotifyClientProxy>();
            services.AddTransient<IGovNotifyService, GovNotifyService>();
            services.AddScoped<SessionStateFilter>();
        }

        private void ConfigureMinimumThreads()
        {
            var workerThreads = Convert.ToInt32(configuration[WorkerThreadsConfigAppSettings]);

            var iocpThreads = Convert.ToInt32(configuration[IocpThreadsConfigAppSettings]);

            if (ThreadPool.SetMinThreads(workerThreads, iocpThreads))
            {
                logger.LogInformation(
                    "ConfigureMinimumThreads: Minimum configuration value set. IOCP = {0} and WORKER threads = {1}",
                    iocpThreads,
                    workerThreads);
            }
            else
            {
                logger.LogWarning("ConfigureMinimumThreads: The minimum number of threads was not changed");
            }
        }

    }
}