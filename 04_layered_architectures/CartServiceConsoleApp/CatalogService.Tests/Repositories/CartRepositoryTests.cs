using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using CatalogService.Domain.Repositories;
using Moq;

namespace CatalogService.Tests.Repositories
{
    public class CartRepositoryTests
    {
        private readonly Mock<ICartDatabase<Cart>> _mockDatabase;
        private readonly CartRepository _cartRepository;

        public CartRepositoryTests()
        {
            _mockDatabase = new Mock<ICartDatabase<Cart>>();
            _cartRepository = new CartRepository(_mockDatabase.Object);
        }

        [Fact]
        public void CartRepositoryShouldSaveCart()
        {
            // Arrange
            var givenId = Guid.NewGuid();
            var cart = new Cart(givenId);

            // Act
            _cartRepository.SaveCart(cart);

            // Assert
            _mockDatabase.Verify(db => db.Upsert(It.Is<Cart>(c => c.Id == givenId)), Times.Once);
        }

        [Fact]
        public void CartRepositoryShouldRetriveCart()
        {
            // Arrange
            var givenId = Guid.NewGuid();
            var expectedCart = new Cart(givenId);

            _mockDatabase.Setup(x => x.FindById(It.IsAny<Guid>()))
                .Returns(expectedCart);

            // Act
            var cart = _cartRepository.GetCartById(givenId);

            // Assert
            _mockDatabase.Verify(x => x.FindById(givenId), Times.Once);
        }

        [Fact]
        public void CartRepositoryShouldDeleteCart()
        {
            // Arrange
            var givenId = Guid.NewGuid();

            // Act
            _cartRepository.DeleteCart(givenId);

            // Assert
            _mockDatabase.Verify(x => x.Delete(It.Is<Guid>(guid => guid == givenId)));
        }
    }
}
