using System;
using DataLibrary.Models;

namespace ListProjectSqlite.Services
{
	public interface IMovieService
	{
        Task<IEnumerable<Movie>> GetAllMovies();
        Task<Movie> GetMovie(int id);
        Task<bool> AddMovie(Movie movie);
        Task<bool> UpdateMovie(Movie movie);
        Task<bool> DeleteMovie(int id);
    }
}

