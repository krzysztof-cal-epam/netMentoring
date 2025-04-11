namespace CartServiceConsoleApp.Entities
{
    public class Cart
    {
        public Cart(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
