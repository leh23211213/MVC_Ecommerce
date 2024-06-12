namespace ecommerce_temp.Models.Cart
{
    public class CartViewModel
    {
        public string UserId { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
    }
}
