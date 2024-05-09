namespace ecommerce_temp.Models
{
    public class ProductDetail
    {
        public string ProductDetailId { get; set; }
        // Khóa ngoại
        public string ProductId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
        public virtual Color Color { get; set; }
        public virtual Category Category { get; set; }
    }
}