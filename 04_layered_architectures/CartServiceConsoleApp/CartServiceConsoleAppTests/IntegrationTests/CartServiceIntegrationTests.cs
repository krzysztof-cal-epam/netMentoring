using CartServiceConsoleApp.BLL.Services;
using CartServiceConsoleApp.DAL.Repositories;
using CartServiceConsoleApp.Entities;

namespace CartServiceConsoleAppTests.IntegrationTests
{
    public class CartServiceIntegrationTests
    {
        private readonly CartService _sut;
        private readonly CartRepository _cartRepository;
        private readonly string _testDbName;

        public CartServiceIntegrationTests()
        {
            _testDbName = "TestECommerce.db";
            _cartRepository = new CartRepository();
            _sut = new CartService(_cartRepository);
        }

        [Fact]
        public void CartServiceShouldCallCartRepository()
        {
            // Arrange
            var givenCartId = Guid.NewGuid().ToString();
            var givenItemId = Guid.NewGuid().ToString();
            var givenItemName = "Name_" + Guid.NewGuid().ToString();
            var givenItem = new CartItem() { Id = givenItemId, Name = givenItemName, Price = 123.00m, Quantity = 1 };

            // Act
            _sut.AddItem(givenCartId, givenItem);

            // Assert
            var expectedCart = _cartRepository.GetCartById(givenCartId);
            Assert.Equal(expectedCart.Id, givenCartId);
            Assert.Equal(expectedCart.Items[0].Id, givenItemId);
            Assert.Equal(expectedCart.Items[0].Name, givenItemName);
        }
    }
}
