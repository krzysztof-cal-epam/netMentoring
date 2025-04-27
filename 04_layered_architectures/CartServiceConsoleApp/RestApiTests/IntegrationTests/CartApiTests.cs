using Microsoft.AspNetCore.Mvc.Testing;
using RestApiTests.Helpers;
using System.Net;

namespace RestApiTests.IntegrationTests
{
    public class CartApiTests : IDisposable
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public CartApiTests()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Theory]
        [InlineData("v1")]
        [InlineData("v2")]
        public async Task GetCartInfo_Should_ReturnOk(string version)
        {
            // Arrange
            var validCartId = Guid.NewGuid();

            // Act
            var response = await _client.GetAsync($"/api/{version}/cart/{validCartId}");

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
