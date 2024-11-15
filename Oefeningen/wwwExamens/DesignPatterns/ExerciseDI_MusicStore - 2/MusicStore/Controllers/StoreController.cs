using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using Microsoft.EntityFrameworkCore;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext _context;

        public StoreController(StoreContext context)
        {
            _context = context;
        }

        // GET: Genres
        public async Task<IActionResult> ListGenres()
        {
            return View(await _context.Genres.OrderBy(g => g.Name).ToListAsync());
        }

        // GET: Albums
        public async Task<IActionResult> ListAlbums(int id)
        {
            var albums = _context.Albums.Where(a => a.GenreID == id).Include(a => a.Genre).OrderBy(a => a.Title);

            ViewData["Genre"] = _context.Genres.Where(g => g.GenreID == id).SingleOrDefault();

            return View(await albums.ToListAsync());
        }

        // GET: Albums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var album = await _context.Albums.Include(a => a.Genre).Include(a => a.Artist).SingleOrDefaultAsync(m => m.AlbumID == id);
            
            return View(album);
        }

    }
}