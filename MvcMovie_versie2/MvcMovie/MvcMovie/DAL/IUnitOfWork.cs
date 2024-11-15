using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcMovie.Models;

namespace MvcMovie.DAL
{
    public interface IUnitOfWork
    {
        IRepository<Movie> MovieRepository { get; }
        IRepository<Rating> RatingRepository { get; }

        void Save();
    }
}
