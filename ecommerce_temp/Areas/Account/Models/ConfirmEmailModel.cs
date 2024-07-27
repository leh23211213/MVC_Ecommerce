using Microsoft.AspNetCore.Mvc;

namespace ecommerce_temp.Areas.Account.Models
{
    public class ConfirmEmailModel
    {
        [TempData]
        public string StatusMessage { get; set; }
    }
}