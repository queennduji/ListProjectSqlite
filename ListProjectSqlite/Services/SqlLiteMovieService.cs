using DataLibrary.Data;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace ListProjectSqlite.Services
{
    public class SqlLiteMovieService : IMovieService
    {
        private readonly DataContext _context;

        public SqlLiteMovieService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovie(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<bool> AddMovie(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> UpdateMovie(Movie updatedMovie)
        {
            _context.Movies.Update(updatedMovie);
           var result =  await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteMovie(int id)
        {
            int result = 0;
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                result = await _context.SaveChangesAsync();
            }
            return result > 0;
        }
       
    }
}

