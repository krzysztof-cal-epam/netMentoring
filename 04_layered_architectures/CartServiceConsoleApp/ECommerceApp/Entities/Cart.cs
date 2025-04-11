namespace CartServiceConsoleApp.Entities
{
    public class Cart
    {
        public Cart(string id)
        {
            Id = id;
        }

        //todo change to Guid type
        public string Id { get; private set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
