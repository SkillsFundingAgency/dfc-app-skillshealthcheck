using System;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Data.Enums;
using DFC.App.SkillsHealthCheck.Models;

using FakeItEasy;

using FluentAssertions;

using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

using Xunit;

namespace DFC.App.SkillsHealthCheck.IntegrationTests.ControllerTests
{
    [Trait("Category", "Webhooks Controller Integration Tests")]
    public class WebhooksControllerRouteTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private const string EventTypePublished = "published";
        private const string WebhookApiUrl = "/api/webhook/ReceiveEvents";

        private readonly CustomWebApplicationFactory<Startup> factory;

        public WebhooksControllerRouteTests(CustomWebApplicationFactory<Startup> factory)
        {
            this.factory = factory;
        }

        [Fact]
        public async Task WebhooksControllerRouteTestsSubscriptionValidationReturnsSuccess()
        {
            // Arrange
            string expectedValidationCode = Guid.NewGuid().ToString();
            var eventGridEvents = BuildValidEventGridEvent(EventTypes.EventGridSubscriptionValidationEvent, new SubscriptionValidationEventData(expectedValidationCode, "https://somewhere.com"));
            var uri = new Uri(WebhookApiUrl, UriKind.Relative);
            var client = this.factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            // Act
            var response = await client.PostAsJsonAsync(uri, eventGridEvents);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be(MediaTypeNames.Application.Json);
            response.Content.Headers.ContentType.CharSet.Should().Be(Encoding.UTF8.WebName);

            var data = await response.Content.ReadAsAsync<SubscriptionValidationResponse>();
            data.Should().NotBeNull();
            data.ValidationResponse.Should().Be(expectedValidationCode);
        }

        [Theory]
        [InlineData(HttpStatusCode.OK)]
        [InlineData(HttpStatusCode.Created)]
        [InlineData(HttpStatusCode.AlreadyReported)]
        public async Task WebhooksControllerRouteTestsPublishCreatePostReturnsSuccess(HttpStatusCode statusCode)
        {
            // Arrange
            A.CallTo(() => factory.FakeWebhookService.ProcessMessageAsync(A<WebhookCacheOperation>.Ignored, A<Guid>.Ignored, A<Guid>.Ignored, A<string>.Ignored))
                .Returns(statusCode);
            var eventGridEvents = BuildValidEventGridEvent(EventTypePublished, new EventGridEventData { ItemId = "edfc8852-9820-4f29-b006-9fbd46cab646", Api = "https://localhost:44354/home/edfc8852-9820-4f29-b006-9fbd46cab646", });
            var uri = new Uri(WebhookApiUrl, UriKind.Relative);
            var client = this.factory.CreateClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            // Act
            var response = await client.PostAsJsonAsync(uri, eventGridEvents);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        private static EventGridEvent[] BuildValidEventGridEvent<TModel>(string eventType, TModel data)
        {
            var models = new EventGridEvent[]
            {
                new EventGridEvent
                {
                    Id = Guid.NewGuid().ToString(),
                    Subject = "an-integration-test-name",
                    Data = data,
                    EventType = eventType,
                    EventTime = DateTime.Now,
                    DataVersion = "1.0",
                },
            };

            return models;
        }
    }
}
