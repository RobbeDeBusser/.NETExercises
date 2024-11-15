using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.DAL
{
    public class MovieRepository : IRepository<Movie>
    {
        private MovieContext _context;

        public MovieRepository(MovieContext context)
        {
            _context = context;
        }
        public IEnumerable<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }
        public Movie GetByID(int id)
        {
            return _context.Movies
                .Include(m => m.Rating)
                .SingleOrDefault(m => m.MovieID == id);
        }
        public void Insert(Movie obj)
        {
            _context.Movies.Add(obj);
        }
        public void Delete(int id)
        {
            var movie = _context.Movies.Find(id);
            _context.Movies.Remove(movie);
        }
        public void Update(Movie obj)
        {
            _context.Update(obj);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
