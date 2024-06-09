namespace ecommerce_temp.Models
{
    public class ProductDetail
    {
        public string ProductDetailId { get; set; }
        public string ProductId { get; set; }
        public string ColorId { get; set; }
        public string SizeId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Color Color { get; set; }
        public virtual Size Size { get; set; }
    }
}