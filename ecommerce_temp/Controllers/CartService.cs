using ecommerce_temp.Data;
using Microsoft.EntityFrameworkCore;

public class CartService
{
    private readonly ecommerce_tempContext _context;

    public CartService(ecommerce_tempContext context)
    {
        _context = context;
    }

    public Cart GetCartByUserId(string userId)
    {
        return _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefault(c => c.UserId == userId);
    }

    public void AddToCart(string userId, string productId, int quantity)
    {
        var cart = GetCartByUserId(userId);
        if (cart == null)
        {
            cart = new Cart
            {
                UserId = userId,
                DateCreated = DateTime.Now
            };
            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                ProductId = productId,
                CartId = cart.CartId,
                Quantity = quantity
            };
            _context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity += quantity;
        }
        _context.SaveChanges();
    }

    public void RemoveFromCart(string userId, string productId)
    {
        var cart = GetCartByUserId(userId);
        if (cart != null)
        {
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }
    }

    public void ClearCart(string userId)
    {
        var cart = GetCartByUserId(userId);
        if (cart != null)
        {
            _context.CartItems.RemoveRange(cart.CartItems);
            _context.SaveChanges();
        }
    }
}