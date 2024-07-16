using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace ecommerce_temp.Areas.Account.Models
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}