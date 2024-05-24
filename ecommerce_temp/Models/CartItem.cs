using ecommerce_temp.Models;

public class CartItem
{
    public string CartItemId { get; set; }
    public string CartId { get; set; }
    public string ProductId { get; set; }
    public int Quantity { get; set; }

    // Navigation property for Cart
    public Cart Cart { get; set; }
    // Navigation property for Product
    public Product Product { get; set; }
}
