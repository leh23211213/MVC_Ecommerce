using System.ComponentModel.DataAnnotations;
using ecommerce_temp.Enums;
using ecommerce_temp.ViewModels;

namespace ecommerce_temp.Models.Order
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string CustomerName { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
    }
}