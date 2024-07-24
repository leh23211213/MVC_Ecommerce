namespace ecommerce_temp.Models.Cart
{
    public class CartViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
    }
}
