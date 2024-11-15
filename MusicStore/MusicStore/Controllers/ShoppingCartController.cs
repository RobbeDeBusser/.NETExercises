using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly StoreContext _context;
        string _cartKey { get; set; }


        public ShoppingCartController(IHttpContextAccessor httpContextAccessor, StoreContext context)
        {
            _cartKey = GetCartKey(httpContextAccessor.HttpContext);
            _context = context;
        }
        private string GetCartKey(HttpContext httpContext)
        {
            if (httpContext.Session.GetString("CartKey") == null)
            {
                Guid tempCartKey = Guid.NewGuid();
                httpContext.Session.SetString("CartKey", tempCartKey.ToString());
            }
            return httpContext.Session.GetString("CartKey");
        }
        public async Task<IActionResult> Index()
        {
            var CartItems = await _context.CartItems.Include(c=>c.Album).ToListAsync();
            return View(CartItems);
        }
        public IActionResult AddToCart(int id)
        {
            // Get the matching cart and album instances
            var cartItem = _context.CartItems.SingleOrDefault(
                c => c.CartKey == _cartKey
                && c.AlbumID == id);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new CartItem
                {
                    AlbumID = id,
                    CartKey = _cartKey,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(m => m.CartItemID == id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);

            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Add(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (id != cartItem.CartItemID)
            {
                return NotFound();
            }
            cartItem.Count = cartItem.Count + 1;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.CartItemID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Subtract(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (id != cartItem.CartItemID)
            {
                return NotFound();
            }
            cartItem.Count = cartItem.Count - 1;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartItemExists(cartItem.CartItemID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        private bool CartItemExists(int id)
        {
            return _context.CartItems.Any(e => e.CartItemID == id);
        }

        public decimal GetTotalPrice()
        {
            var total = (from cartItem in _context.CartItems
                         where cartItem.CartKey == _cartKey
                         select (int?)cartItem.Count * cartItem.Album.Price).Sum();

            return total ?? 0;
        }
    }
}
