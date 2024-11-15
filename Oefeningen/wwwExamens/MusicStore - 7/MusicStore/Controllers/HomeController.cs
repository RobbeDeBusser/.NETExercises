using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;
using System.Diagnostics;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly StoreContext _context;
        public HomeController(StoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var albums = _context.Albums.OrderBy(albums=>Guid.NewGuid()).Take(6);
            return View(await albums.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
