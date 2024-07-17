namespace ecommerce_temp.Data.Models
{
    public class Cart
    {
        public string CartId { get; set; }
        public string UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartId = Guid.NewGuid().ToString(); // Tạo GUID mới cho CartId khi khởi tạo Cart
        }
    }

}