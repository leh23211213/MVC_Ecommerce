using System.ComponentModel.DataAnnotations;
using ecommerce_temp.Enums;

namespace ecommerce_temp.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderDetailId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public int Quantity { get; set; }

        public decimal Price { get; set; }
        public OrderDetailStatus Status { get; set; }
        public string ProductName { get; set; }
    }
}
