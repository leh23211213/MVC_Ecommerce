namespace ecommerce_temp.Models
{
    public class Cart
    {
        public string UserId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}