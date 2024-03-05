namespace MVC_Ecommerce.Models
{
    using System.ComponentModel.DataAnnotations;
    public class User
    {
        public int id { get; set; }

        public string name { get; set; }

        public string password { get; set; }
        public double amount { get; set; }
    }
}

