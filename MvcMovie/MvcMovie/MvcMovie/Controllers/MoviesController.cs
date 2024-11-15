using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
using MvcMovie.DAL;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Models.ViewModels;

namespace MvcMovie.Controllers
{
	public class MoviesController : Controller
	{
		private readonly MovieContext _context;


        private IRepository<Movie> movieRepository;
        private IRepository<Rating> ratingRepository; 
		public MoviesController(MovieContext context)
        {
            movieRepository = new MovieRepository(context);
			ratingRepository = new RatingRepository(context);
        }

        public IActionResult List(int ratingID = 0)
		{
			var listMoviesVM = new ListMoviesViewModel();

			if (ratingID != 0)
			{
				listMoviesVM.Movies = movieRepository.GetAll().Where(m => m.RatingID == ratingID).OrderBy(m => m.Title).ToList();
			}
			else
			{
				listMoviesVM.Movies = movieRepository.GetAll().OrderBy(m => m.Title).ToList();
			}

			listMoviesVM.Ratings =
				new SelectList(ratingRepository.GetAll()
				.OrderBy(r => r.Name), "RatingID", "Name");
			listMoviesVM.ratingID = ratingID;

			return View(listMoviesVM);
		}

		public IActionResult Details(int id)
		{
			var movie = movieRepository
							.GetByID(id);

			return View(movie);
		}

		// GET: Movies/Create
		public IActionResult Create()
		{
			ViewData["Ratings"] =
				new SelectList(ratingRepository.GetAll().OrderBy(r => r.Name),
							   "RatingID",
							   "Name");

			return View();
		}
	}
}