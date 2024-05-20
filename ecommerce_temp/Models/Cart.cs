using ecommerce_temp.Models;

public class Cart
{
    public string CartId { get; set; }
    public string UserId { get; set; }
    public DateTime DateCreated { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; }

    public Cart()
    {
        CartItems = new List<CartItem>();
    }
}
