using Microsoft.AspNetCore.Identity;

namespace ecommerce_temp.Models
{
    public class User : IdentityUser
    {
        // public string? FirstName { get; set; }
        // public string? LastName { get; set; }
        // public string? ImageUrl { get; set; }
        // public bool? IsActive { get; set; }
        // public decimal? Balance { get; set; }
        // public string? AddressLine1 { get; set; }
        // public string? AddressLine2 { get; set; }
        // public string? City { get; set; }
        // public string? Country { get; set; }
        // public IEnumerable<Order>? Orders { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
