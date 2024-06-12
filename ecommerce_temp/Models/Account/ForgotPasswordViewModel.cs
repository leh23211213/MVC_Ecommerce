using System.ComponentModel.DataAnnotations;

namespace ecommerce_temp.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
