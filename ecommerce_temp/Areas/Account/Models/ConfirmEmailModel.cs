using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce_temp.Areas.Account.Models
{
    public class ConfirmEmailModel
    {
        [TempData]
        public string StatusMessage { get; set; }
    }
}