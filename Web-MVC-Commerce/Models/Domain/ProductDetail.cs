namespace MVC_Ecommerce.Models
{
    public class ProductDetail
    {
        public int ProductDetailId { get; set; }
        public int ProductId { get; set; }
        public Nullable<int> Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Size Size { get; set; }
        public virtual Color Color { get; set; }
        public virtual Category Category { get; set; }

    }
}