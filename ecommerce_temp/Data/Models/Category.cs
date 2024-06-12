namespace ecommerce_temp.Data.Models
{
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
