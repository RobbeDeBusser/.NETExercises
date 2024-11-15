using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext _context;

        public StoreController(StoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .FirstOrDefaultAsync(m => m.AlbumID == id);
            if (album == null)
            {
                return NotFound();
            }

            return View(album);
        }

        public async Task<IActionResult> ListGenres()
        {
            var genres = _context.Genres.OrderBy(g => g.Name);
            return View(await genres.ToListAsync());
        }

        public async Task<IActionResult> ListAlbums(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }
            var album = await _context.Genres.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            ViewData["Genre"] = album.Name;
            var albums = _context.Albums.Where(a => a.GenreID == id);
            return View(albums);
        }
    }
}
