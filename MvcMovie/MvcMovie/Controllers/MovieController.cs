using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models.ViewModels;
using System.Linq;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult List(int ratingID = 0, string zoekstring = "")
        {
            var listMoviesVM = new ListMoviesViewModel();

            if (ratingID != 0)
            {
                listMoviesVM.Movies = _context.Movies.Where(m => m.Title.Contains(zoekstring)).OrderBy(m =>
                m.Title).ToList();
            }
            else
            {
                listMoviesVM.Movies = _context.Movies
                    .Where(m => m.Title.Contains(zoekstring))
                    .OrderBy(m => m.Title).ToList();
            }
            listMoviesVM.Ratings =
            new SelectList(_context.Ratings.OrderBy(r => r.Name),
            "RatingID", "Name");
            listMoviesVM.ratingID = ratingID;
            return View(listMoviesVM);
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                            .Include(m => m.Rating)
                            .SingleOrDefault(m => m.MovieID == id);

            return View(movie);
        }

        public IActionResult ShowAll()
        {
            var ratings = _context.Ratings.Include(r => r.Movies).OrderBy(m => m.Name);
     
            return View(ratings.ToList());
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["Ratings"] =
                new SelectList(_context.Ratings.OrderBy(r => r.Name),
                               "RatingID",
                               "Name");

            return View();
        }



    }
}
