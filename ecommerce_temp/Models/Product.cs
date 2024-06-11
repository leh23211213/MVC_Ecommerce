namespace ecommerce_temp.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }
        public int? Vote { get; set; }
        public int? CategoryId { get; set; }
        public int BrandId { get; set; }
        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}