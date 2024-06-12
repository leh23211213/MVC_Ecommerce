namespace ecommerce_temp.Models.Cart
{
    public class CartItemViewModel
    {
        public int CartItemId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
