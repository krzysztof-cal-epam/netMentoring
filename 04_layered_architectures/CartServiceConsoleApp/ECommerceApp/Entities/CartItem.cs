namespace CartServiceConsoleApp.Entities
{
    public class CartItem
    {
        // change to int -> check task 2
        public string Id { get; set; }
        public string Name { get; set; }
        //todo add image property, make it optional by nullable reference
        public string ImageUrl { get; set; } //todo this should be a part of inner object of Image 
        public string AltText { get; set; } //todo this should be a part of inner object of Image and optional
        public decimal Price { get; set; }
        public int Quantity { get; set; } //todo quantity should be > 0 -> req, can be uint
    }
}
