namespace ecommerce_temp.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public string Status { get; set; }


        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        // public virtual Customer Customers { get; set; }
    }
}