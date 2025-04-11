using CartServiceConsoleApp.DAL.Repositories;
using CartServiceConsoleApp.Entities;

namespace CartServiceConsoleAppTests.UnitTests
{
    public class CartRepositoryTests : IDisposable
    {
        private readonly CartRepository _sut;

        private readonly List<string> _cartIdsToDispose;

        public CartRepositoryTests()
        {
            _sut = new CartRepository();
            _cartIdsToDispose = new List<string>();
        }

        [Fact]
        public void CartRepositoryShouldSaveCart()
        {
            // Arrange
            var givenId = Guid.NewGuid().ToString();
            _cartIdsToDispose.Add(givenId);
            var cart = new Cart(givenId);

            // Act
            _sut.SaveCart(cart);

            // Assert
            var expectedCart = _sut.GetCartById(givenId);
            Assert.Equal(expectedCart.Id, cart.Id);
        }

        void IDisposable.Dispose()
        {
            // Cleanup
            foreach (var id in _cartIdsToDispose)
            {
                _sut.DeleteCart(id);
            }

            _sut.Dispose();
        }
    }
}
