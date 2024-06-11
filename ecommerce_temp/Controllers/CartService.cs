using ecommerce_temp.Data;
using ecommerce_temp.Models;
using Microsoft.EntityFrameworkCore;

public class CartService
{
    private readonly ecommerce_tempContext _context;
    private readonly HttpContext HttpContext;

    public CartService(ecommerce_tempContext context)
    {
        _context = context;
    }

    private Cart GetCartId(string userId)
    {
        var cart = _context.Carts
           .Include(c => c.CartItems)
           .ThenInclude(ci => ci.Product)
           .FirstOrDefault(c => c.UserId == userId);

        return cart == null ? null : cart;
    }

    public CartViewModel GetView(string userId)
    {
        var cart = GetCartId(userId);

        var cartViewModel = new CartViewModel
        {
            UserId = cart.UserId,
            CartItems = cart.CartItems.Select(ci => new CartItemViewModel
            {
                CartItemId = ci.CartItemId,
                ProductName = ci.Product.ProductName,
                ImageUrl = ci.Product.ImageUrl,
                Price = ci.Product.Price,
                Quantity = ci.Quantity
            }).ToList()
        };
        return cartViewModel;
    }

    public async Task Add(string userId, string productId, int quantity = 1)
    {
        var cart = GetCartId(userId);

        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartId == cart.CartId && ci.ProductId == productId);
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
            cartItem.Quantity += 1;
            _context.CartItems.Update(cartItem);
        }
        _context.SaveChanges();
    }

    public void Delete(string userId, string productId)
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
    // public CartViewModel IncreaseCartItemQuantity(string userId, string cartItemId)
    // {
    //     var cart = _context.Carts
    //         .Include(c => c.CartItems)
    //         .FirstOrDefault(c => c.UserId == userId);

    //     if (cart != null)
    //     {
    //         var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
    //         if (cartItem != null)
    //         {
    //             cartItem.Quantity++;
    //             _context.SaveChanges();
    //         }
    //     }

    //     return GetCart(userId);
    // }

    // public CartViewModel DecreaseCartItemQuantity(string userId, string cartItemId)
    // {
    //     var cart = _context.Carts
    //         .Include(c => c.CartItems)
    //         .FirstOrDefault(c => c.UserId == userId);

    //     if (cart != null)
    //     {
    //         var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
    //         if (cartItem != null && cartItem.Quantity > 1)
    //         {
    //             cartItem.Quantity--;
    //             _context.SaveChanges();
    //         }
    //     }

    //     return GetCart(userId);
    // }
}