namespace ecommerce_temp.Models
{
    public class CartItemViewModel
    {
        public string CartItemId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
