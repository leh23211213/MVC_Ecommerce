namespace MVC_Ecommerce.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Orders = new HashSet<Order>();
        }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual Account Account { get; set; }
    }
}