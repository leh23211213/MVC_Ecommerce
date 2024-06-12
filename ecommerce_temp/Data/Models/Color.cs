namespace ecommerce_temp.Data.Models
{
    public class Color
    {
        public Color()
        {
            this.ProductDetails = new HashSet<ProductDetail>();
        }

        public int ColorId { get; set; }
        public string ColorName { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
