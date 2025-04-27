using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace RestApiTests.Helpers
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var mockCartService = new Mock<ICartService>();
                mockCartService.Setup(service => service.GetCartInfo(It.IsAny<Guid>()))
                    .Returns(new CartDto { });

                services.AddScoped<ICartService>(_ => mockCartService.Object);
            });
        }
    }
}
