using ecommerce_temp.Enums;

namespace ecommerce_temp.Data.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderDetailStatus Status { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}