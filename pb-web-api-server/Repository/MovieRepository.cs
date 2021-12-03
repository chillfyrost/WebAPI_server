using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext repositoryContext)
           : base(repositoryContext)
        {
        }
        public void CreateMovie(Movie movie)
        {
            Create(movie);
        }

        public void DeleteMovie(Movie movie)
        {
            Delete(movie);
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            return await FindAll()
                .OrderBy(mv => mv.Title)
                .ToListAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(Guid movieId)
        {
            return await FindByCondition(movie => movie.Id.Equals(movieId))
                .FirstOrDefaultAsync();
        }

        public async Task<Movie> GetMovieWithDetailsAsync(Guid movieId)
        {
            return await FindByCondition(movie => movie.Id.Equals(movieId))
                .FirstOrDefaultAsync();
        }

        public void UpdateMovie(Movie movie)
        {
            Update(movie);
        }
    }
}
