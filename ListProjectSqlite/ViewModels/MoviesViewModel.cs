using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using DataLibrary.Models;
using ListProjectSqlite.Services;

namespace ListProjectSqlite.ViewModels
{
	public class MoviesViewModel : BindableObject
    {
        private readonly IMovieService _movieService;
        public ObservableCollection<Movie> Movies { get; set; } = new ObservableCollection<Movie>();

        private Movie _selectedMovie;
        private string _newMovieName;
        private string _newMovieGenre;

        public ICommand LoadMoviesCommand { get; }
        public ICommand AddMovieCommand { get; }
        public ICommand UpdateMovieCommand { get; }
        public ICommand DeleteMovieCommand { get; }

        public Movie SelectedMovie
        {
            get => _selectedMovie;
            set
            {
                _selectedMovie = value;
                OnPropertyChanged();
            }
        }

        public string NewMovieName
        {
            get => _newMovieName;
            set
            {
                _newMovieName = value;
                OnPropertyChanged();
            }
        }

        public string NewMovieGenre
        {
            get => _newMovieGenre;
            set
            {
                _newMovieGenre = value;
                OnPropertyChanged();
            }
        }

        public MoviesViewModel(IMovieService movieService)
        {
            _movieService = movieService;

            LoadMoviesCommand = new Command(async () => await LoadMovies());
            AddMovieCommand = new Command<Movie>(async (movie) => await AddMovie());
            UpdateMovieCommand = new Command<Movie>(async (movie) => await UpdateMovie());
            DeleteMovieCommand = new Command<int>(async (id) => await DeleteMovie(id));

            Task.Run(async () => await LoadMovies());
        }

        private async Task LoadMovies()
        {
            var movies = await _movieService.GetAllMovies();

            Application.Current.Dispatcher.Dispatch(() =>
            {
                Movies.Clear();
                foreach (var movie in movies)
                {
                    Movies.Add(movie);
                }
            });
        }

        private async Task AddMovie()
        {
            if (string.IsNullOrWhiteSpace(NewMovieName) || string.IsNullOrWhiteSpace(NewMovieGenre))
            {
                return;
            }

            var newMovie = new Movie
            {
                Name = NewMovieName,
                Genre = NewMovieGenre
            };

            var result = await _movieService.AddMovie(newMovie);

            if (result)
            {
                await LoadMovies();
                NewMovieName = string.Empty;
                NewMovieGenre = string.Empty;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to add movie.", "OK");
            }

        }

        private async Task UpdateMovie()
        {

            if (SelectedMovie != null)
            {
                SelectedMovie.Name = NewMovieName;
                SelectedMovie.Genre = NewMovieGenre;
               var result =  await _movieService.UpdateMovie(SelectedMovie);
                if(result)
                await LoadMovies();
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to update movie.", "OK");
                }
            }
        }

        private async Task DeleteMovie(int id)
        {
           var result =  await _movieService.DeleteMovie(id);
            if(result)
            await LoadMovies();
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to delte movie.", "OK");
            }
        }
    }
}

