using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

using DFC.App.SkillsHealthCheck.Data.Contracts;
using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.GovNotify;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.Compui.Cosmos.Contracts;
using DFC.Compui.Sessionstate;

using FakeItEasy;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DFC.App.SkillsHealthCheck.IntegrationTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        public CustomWebApplicationFactory()
        {
            this.MockCosmosRepo = A.Fake<ICosmosRepository<SharedContentItemModel>>();
            FakeWebhookService = A.Fake<IWebhooksService>();
            FakeSessionStateService = A.Fake<ISessionStateService<SessionDataModel>>();
            FakeGovNotifyService = A.Fake<IGovNotifyService>();
            FakeSkillsHealthCheckService = A.Fake<ISkillsHealthCheckService>();
        }

        internal ICosmosRepository<SharedContentItemModel> MockCosmosRepo { get; }

        internal ISessionStateService<SessionDataModel> FakeSessionStateService { get; }

        internal IGovNotifyService FakeGovNotifyService { get; }

        internal ISkillsHealthCheckService FakeSkillsHealthCheckService { get; }

        internal IWebhooksService FakeWebhookService { get; }

        internal new HttpClient CreateClient()
        {
            var opts = new WebApplicationFactoryClientOptions { AllowAutoRedirect = false };
            return CreateClient(opts);
        }

        internal void SetSkillsDocument()
        {
            var skillsDocumentIdentifier = new SkillsDocumentIdentifier
            {
                ServiceName = Constants.SkillsHealthCheck.DocumentSystemIdentifierName,
                Value = "Code",
            };
            var skillsDocumentDataValue = new SkillsDocumentDataValue
            {
                Title = Constants.SkillsHealthCheck.SkillsAssessmentComplete,
                Value = bool.FalseString,
            };
            var skillsDocument = new SkillsDocument
            {
                DocumentId = 123,
                SkillsDocumentTitle = "some document",
                CreatedAt = DateTime.UtcNow,
                SkillsDocumentDataValues = new List<SkillsDocumentDataValue> { skillsDocumentDataValue },
                SkillsDocumentIdentifiers = new List<SkillsDocumentIdentifier> { skillsDocumentIdentifier },
            };

            A.CallTo(() => FakeSkillsHealthCheckService.GetSkillsDocument(A<GetSkillsDocumentRequest>.Ignored))
                .Returns(new GetSkillsDocumentResponse { Success = true, SkillsDocument = skillsDocument });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            _ = builder ?? throw new System.ArgumentNullException(nameof(builder));
            builder.ConfigureServices(services =>
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                services.AddSingleton<IConfiguration>(configuration);
            });

            builder.ConfigureTestServices(services =>
            {
                var hostedServices = services.Where(descriptor =>
                    descriptor.ServiceType == typeof(IHostedService) ||
                    descriptor.ServiceType == typeof(ICosmosRepository<>) ||
                    descriptor.ServiceType == typeof(IWebhooksService) ||
                    descriptor.ServiceType == typeof(ISessionStateService<>) ||
                    descriptor.ServiceType == typeof(IGovNotifyService) ||
                    descriptor.ServiceType == typeof(IWebhooksService) ||
                    descriptor.ServiceType == typeof(ISkillsHealthCheckService))

                .ToList();
                foreach (var service in hostedServices)
                {
                    services.Remove(service);
                }

                services.AddTransient(sp => MockCosmosRepo);
                services.AddTransient(sp => FakeWebhookService);
                services.AddTransient(sp => FakeSessionStateService);
                services.AddTransient(sp => FakeGovNotifyService);
                services.AddTransient(sp => FakeSkillsHealthCheckService);
            });
        }
    }
}
