namespace ecommerce_temp.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

    public enum OrderStatus
    {
        Processing,
        Completed,
        Canceled
    }
}