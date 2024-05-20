namespace ecommerce_temp.Models
{
    public class CartViewModel
    {
        public string UserId { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
    }
}
