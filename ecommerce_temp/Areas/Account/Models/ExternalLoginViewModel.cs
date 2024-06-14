using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;

namespace ecommerce_temp.Areas.Account.Models
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
