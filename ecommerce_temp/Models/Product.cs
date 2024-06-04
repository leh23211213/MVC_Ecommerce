namespace ecommerce_temp.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public string? CategoryId { get; set; }
        public int? Vote { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<ProductDetail> Details { get; set; }
        // public virtual Brand Brand { get; set; }
        //public virtual Category Category { get; set; }
    }
}