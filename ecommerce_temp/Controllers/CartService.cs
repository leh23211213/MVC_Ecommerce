using ecommerce_temp.Data;
using ecommerce_temp.Models;
using Microsoft.EntityFrameworkCore;

public class CartService
{
    private readonly ecommerce_tempContext _context;

    public CartService(ecommerce_tempContext context)
    {
        _context = context;
    }

    public CartViewModel GetCartByUserId(string userId)
    {
        var cart = _context.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefault(c => c.UserId == userId);

        if (cart == null)
        {
            return null;
        }

        var cartViewModel = new CartViewModel
        {
            UserId = cart.UserId,
            CartItems = cart.CartItems.Select(ci => new CartItemViewModel
            {
                CartItemId = ci.CartItemId,
                ProductName = ci.Product.Name,
                Price = ci.Product.Price,
                Quantity = ci.Quantity
            }).ToList()
        };
        return cartViewModel;
    }

    public void AddToCart(string userId, string productId, int quantity)
    {
        var cart = _context.Carts
          .Include(c => c.CartItems)
          .FirstOrDefault(c => c.UserId == userId);
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
        var cart = _context.Carts
          .Include(c => c.CartItems)
          .FirstOrDefault(c => c.UserId == userId);

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
        var cart = _context.Carts
          .Include(c => c.CartItems)
          .FirstOrDefault(c => c.UserId == userId);

        if (cart != null)
        {
            _context.CartItems.RemoveRange(cart.CartItems);
            _context.SaveChanges();
        }
    }
    public CartViewModel IncreaseCartItemQuantity(string userId, string cartItemId)
    {
        var cart = _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefault(c => c.UserId == userId);

        if (cart != null)
        {
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity++;
                _context.SaveChanges();
            }
        }

        return GetCartByUserId(userId);
    }

    public CartViewModel DecreaseCartItemQuantity(string userId, string cartItemId)
    {
        var cart = _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefault(c => c.UserId == userId);

        if (cart != null)
        {
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
            if (cartItem != null && cartItem.Quantity > 1)
            {
                cartItem.Quantity--;
                _context.SaveChanges();
            }
        }

        return GetCartByUserId(userId);
    }
}