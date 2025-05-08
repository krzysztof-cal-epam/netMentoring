using CatalogService.Application.Dto;
using CatalogService.Application.Interfaces;
using CatalogService.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestApi.Controllers.V1;

namespace RestApiTests.UnitTests
{
    public class CartV1ControllerTests
    {
        private readonly Mock<ICartService> _cartServiceMock;
        private readonly Mock<IRabbitMqProducer> _rabbitMqProducer;
        private readonly CartV1Controller _controller;

        public CartV1ControllerTests()
        {
            _cartServiceMock = new Mock<ICartService>();
            _rabbitMqProducer = new Mock<IRabbitMqProducer>();
            _controller = new CartV1Controller(_cartServiceMock.Object, _rabbitMqProducer.Object);
        }

        [Fact]
        public void GetCartInfo_Should_ReturnOkObjectResult()
        {
            // Arrange:
            var cartId = Guid.NewGuid();
            var mockCart = new CartDto
            {
                CartId = cartId,
                Items = new List<CartItemDto>
                {
                    new CartItemDto { Id = 1, Name = "Laptop", Quantity = 1, Price = 999.0M }
                }
            };

            _cartServiceMock.Setup(service => service.GetCartInfo(cartId))
                .Returns(mockCart);

            // Act:
            var result = _controller.GetCartInfo(cartId);

            // Assert:
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCart = Assert.IsType<CartDto>(okResult.Value);
            Assert.Equal(cartId, returnedCart.CartId);
            Assert.Single(returnedCart.Items);
        }
    }
}
