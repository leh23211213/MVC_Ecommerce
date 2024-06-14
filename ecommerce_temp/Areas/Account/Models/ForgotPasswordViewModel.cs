using System.ComponentModel.DataAnnotations;

namespace ecommerce_temp.Areas.Account.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
