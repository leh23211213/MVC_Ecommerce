namespace MVC_Ecommerce.Models
{
    public class Size{
        public Size(){
            this.ProductDetails = new HashSet<ProductDetailt>();
        }

        public string SizeId {get;set;}
        public string SizeName {get;set;}

        public virtual ICollection<ProductDetail> ProductDetails {get;set;}
    }
}