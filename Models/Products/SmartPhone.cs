using System.Data.SqlTypes;

namespace MVC_Ecommerce.Models.Products
{
    public class SmartPhone
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public SqlMoney Price { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
    }
}