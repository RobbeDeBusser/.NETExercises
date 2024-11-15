using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using MusicStore.Models;
using MusicStore.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly StoreContext _context;
        private readonly IDiscountService _medium;

        public ShoppingCartController(StoreContext context, IDiscountService medium)
        {
            _context = context;
            _medium = medium;
        }

        // GET: /ShoppingCart/
        public ActionResult Index()
        {
            var cart = new ShoppingCart(HttpContext, _context);

            var cartitems = cart.GetCartItems();

            var discount = _medium.GetDiscount(cartitems);
            ViewData["Discount"] = discount;

            // Return the view
            return View(cartitems);
        }

        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            var newAlbum = _context.Albums
                .Single(a => a.AlbumID == id);

            // Add it to the shopping cart
            var cart = new ShoppingCart(HttpContext, _context);

            cart.AddToCart(newAlbum);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        // GET: /Store/AddToCart/5
        public ActionResult RemoveFromCart(int id)
        {
            var cart = new ShoppingCart(HttpContext, _context);

            cart.RemoveFromCart(id);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

    }
}
