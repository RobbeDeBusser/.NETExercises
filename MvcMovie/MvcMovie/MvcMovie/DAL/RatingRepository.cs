using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.DAL
{
    public class RatingRepository : IRepository<Rating>
    {
        private MovieContext _context;
        public RatingRepository(MovieContext context)
        {
            _context = context;
        }
        public IEnumerable<Rating> GetAll()
        {
            return _context.Ratings.ToList();
        }
        public Rating GetByID(int id)
        {
            return _context.Ratings.Find(id);
        }
        public void Insert(Rating obj)
        {
            _context.Ratings.Add(obj);
        }
        public void Delete(int id)
        {
            var rating = GetByID(id);
            _context.Ratings.Remove(rating);
        }
        public void Update(Rating obj)
        {
            _context.Update(obj);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
