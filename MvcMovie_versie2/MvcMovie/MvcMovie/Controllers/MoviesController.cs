using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
using MvcMovie.DAL;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Models.ViewModels;
using System.Data;

namespace MvcMovie.Controllers
{
	public class MoviesController : Controller
	{
		private readonly MovieContext _context;


		private IUnitOfWork _uow;
		public MoviesController(IUnitOfWork uow)
		{
			_uow = uow;
		}

		public IActionResult List(int ratingID = 0)
		{
			var listMoviesVM = new ListMoviesViewModel();

			if (ratingID != 0)
			{
				listMoviesVM.Movies = _uow.MovieRepository.Get(
					filter: m => m.RatingID == ratingID,
					orderBy: m => m.OrderBy(x => x.Title)).ToList();
			}
			else
			{
				listMoviesVM.Movies = _uow.MovieRepository.Get(
					orderBy: m => m.OrderBy(x => x.Title)).ToList();
			}

			listMoviesVM.Ratings =
				new SelectList(_uow.RatingRepository.Get(orderBy: m => m.OrderBy(x => x.Name)).ToList(), "RatingID", "Name");

			return View(listMoviesVM);
		}

        public IActionResult Details(int id)
        {
            var movie = _uow.MovieRepository.Get(
            filter: x => x.MovieID == id,
            includes: x => x.Rating).FirstOrDefault();
            return View(movie);
        }


        // GET: Movies/Create
        public IActionResult Create()
		{
			ViewData["Ratings"] =
				new SelectList(_uow.RatingRepository.GetAll().OrderBy(r => r.Name),
							   "RatingID",
							   "Name");

			return View();
		}
        // GET: Movies/Add
        public IActionResult Add()
        {
			var addMovieVM = new AddMovieViewModel();
			addMovieVM.Movie = new Movie();
            return View(addMovieVM);
        }

        //POST: Movies/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind("Movie,Code, Name")] AddMovieViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var rating = _uow.RatingRepository.Get
                        (filter: r => r.Code == model.Code && r.Name == model.Name).FirstOrDefault();

                    if (rating == null)
                    {
                        rating = new Rating { Code = model.Code, Name = model.Name };
                        _uow.RatingRepository.Insert(rating);
                    }

                    model.Movie.Rating = rating;

                    _uow.MovieRepository.Insert(model.Movie);
                    _uow.Save();

                    return RedirectToAction("List");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes.");
            }
            return RedirectToAction("Add");
        }
    }
}