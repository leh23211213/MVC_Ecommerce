using System.ComponentModel.DataAnnotations;
namespace Web_MVC_Commerce.Models.Account
{
    public class AccountRegisterModel
    {
        public string Email
        {
            get;
            set;
        }
        public string Password
        {
            get;
            set;
        }
    }
}
