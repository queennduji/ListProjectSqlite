using System;
using System.Net.Http.Json;
using DataLibrary.Models;

namespace ListProjectSqlite.Services
{
	public class WebApiService : IMovieService
    {
        private readonly HttpClient _httpClient;

        public WebApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            return await _httpClient.GetFromJsonAsync<List<Movie>>("api/movie");

        }

        public async Task<Movie> GetMovie(int id)
        {
            return await _httpClient.GetFromJsonAsync<Movie>($"api/movie/{id}");
        }

        public async Task<bool> AddMovie(Movie movie)
        {
            var response = await _httpClient.PostAsJsonAsync("api/movie", movie);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateMovie(Movie movie)
        {
            var response = await _httpClient.PutAsJsonAsync("api/movie", movie);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteMovie(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/movie?id={id}");
            return response.IsSuccessStatusCode;
        }
    }
}

