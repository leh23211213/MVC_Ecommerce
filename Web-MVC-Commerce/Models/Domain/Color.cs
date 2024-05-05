namespace MVC_Ecommerce.Models
{
    public class Color
    {
        public Color()
        {
            this.ProductDetails = new HashSet<ProductDetail>();
        }

        public string ColorId { get; set; }
        public string ColorName { get; set; }

        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}