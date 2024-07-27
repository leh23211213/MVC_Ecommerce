using System.ComponentModel.DataAnnotations;

namespace ecommerce_temp.Areas.Account.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}