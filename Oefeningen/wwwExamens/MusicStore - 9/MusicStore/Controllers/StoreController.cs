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
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListGenres()
        {
            var genres = _context.Genres.OrderBy(g=>g.Name).ToList();
            return View(genres);
        }
        public IActionResult ListAlbums(int id)
        {
            var albums = _context.Albums.Where(a=>a.GenreID == id).ToList();
            ViewData["Genre"] = _context.Genres.FirstOrDefault(g=>g.GenreID == id);
            return View(albums);
        }
        public IActionResult Details(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album =  _context.Albums.Include(a=>a.Artist).Include(a=>a.Genre).FirstOrDefault(a=>a.AlbumID == id);
            return View(album);
        }
    }
}
