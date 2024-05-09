using Microsoft.AspNetCore.Identity;

namespace ecommerce_temp.Models
{
    public class User : IdentityUser
    {
        public string? HomeAdress { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
