using CatalogService.DataAccess.Databases;
using CatalogService.DataAccess.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Repositories;

namespace CatalogService.Tests.IntegrationTests
{
    public class CartServiceIntegrationTests
    {
        private readonly CartService _cartService;
        private readonly CartRepository _cartRepository;
        private readonly string _testDbName;

        public CartServiceIntegrationTests()
        {
            _testDbName = "TestECommerce.db";
            var inMemoryCartDatabase = new InMemoryCartDatabase();

            _cartRepository = new CartRepository(inMemoryCartDatabase);
            _cartService = new CartService(_cartRepository);
        }

        [Fact]
        public void CartServiceShouldCallCartRepository()
        {
            // Arrange
            var givenCartId = Guid.NewGuid();
            var givenItemId = new Random().Next(1, int.MaxValue);
            var givenItemName = "Name_" + Guid.NewGuid().ToString();
            var givenItem = new CartItem() { Id = givenItemId, Name = givenItemName, Price = 123.00m, Quantity = 1 };

            // Act
            _cartService.AddItem(givenCartId, givenItem);

            // Assert
            var expectedCart = _cartRepository.GetCartById(givenCartId);
            Assert.Equal(expectedCart.Id, givenCartId);
            Assert.Equal(expectedCart.Items[0].Id, givenItemId);
            Assert.Equal(expectedCart.Items[0].Name, givenItemName);
        }
    }
}
