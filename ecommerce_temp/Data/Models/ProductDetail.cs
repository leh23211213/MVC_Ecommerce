namespace ecommerce_temp.Data.Models
{
    public class ProductDetail
    {
        public string ProductDetailId { get; set; }
        public string ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Color Color { get; set; }
        public virtual Size Size { get; set; }
    }
}