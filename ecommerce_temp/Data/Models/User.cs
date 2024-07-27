using Microsoft.AspNetCore.Identity;

namespace ecommerce_temp.Data.Models;

public class User : IdentityUser
{
    public DateTime? BirthDate { get; set; }
    public virtual Cart? Cart { get; set; } = null!;
}
