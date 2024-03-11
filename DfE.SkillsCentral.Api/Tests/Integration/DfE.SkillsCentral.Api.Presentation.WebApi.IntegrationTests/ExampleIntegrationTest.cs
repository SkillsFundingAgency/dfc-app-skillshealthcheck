using Microsoft.AspNetCore.Mvc.Testing;

namespace DfE.SkillsCentral.Api.Presentation.WebApi.IntegrationTests
{
    public class ExampleIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client;

        public ExampleIntegrationTest(WebApplicationFactory<Program> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task Example1()
        {
            var response = await client.GetAsync("/api/Assessment/SkillAreas");
            Assert.NotNull(response);
        }
    }
}