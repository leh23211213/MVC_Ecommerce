namespace ecommerce_temp.Data.Models
{
    public class Size
    {
        public Size()
        {
            this.ProductDetails = new HashSet<ProductDetail>();
        }
        public int SizeId { get; set; }
        public string SizeName { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
