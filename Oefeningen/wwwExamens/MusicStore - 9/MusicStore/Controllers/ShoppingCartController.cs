using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        string _cartKey { get; set; }
        private readonly StoreContext _context;

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
        public IActionResult Index()
        {
            var cartItems = _context.CartItems.Include(c=>c.Album).ToList();
            return View(cartItems);
        }
        public IActionResult AddToCart(int id)
        {
            var cartItem = _context.CartItems.SingleOrDefault(
                c=>c.CartKey == _cartKey && c.AlbumID == id);
            if (cartItem == null)
            {
                cartItem = new CartItem()
                {
                    AlbumID = id,
                    CartKey = _cartKey,
                    Count = 1,
                    DateCreated = DateTime.Now,
                };
                _context.CartItems.Add(cartItem);
            } else
            {
                cartItem.Count++;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int id)
        {
            var cartItem = _context.CartItems.SingleOrDefault(c=>c.CartItemID == id);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Add(int id)
        {
            var cartItem = _context.CartItems.Find(id);
            if(id != cartItem.CartItemID)
            {
                return NotFound();
            }
            cartItem.Count++;
            if(ModelState.IsValid)
            {
                _context.Update(cartItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Subtract(int id)
        {
            var cartItem = _context.CartItems.Find(id);
            if (id != cartItem.CartItemID)
            {
                return NotFound();
            }
            cartItem.Count = cartItem.Count-1;
            if (ModelState.IsValid)
            {
                _context.Update(cartItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
