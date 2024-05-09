namespace ecommerce_temp.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public virtual Order Oder { get; set; }
        public virtual Product Product { get; set; }
    }
}