namespace CatalogService.Domain.Entities
{
    public class Cart
    {
        public Cart(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
