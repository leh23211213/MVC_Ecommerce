using Microsoft.AspNetCore.Identity;

namespace ecommerce_temp.Models
{
    public class UserViewModel : IdentityUser
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
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}
