using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace ecommerce_temp.Models.Account
{
    public class ExternalLoginViewModel
    {
        public string ProviderDisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
