using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;
using ListProjectSqlite.ViewModels;

namespace ListProjectSqlite.Views;

public partial class MoviesPage : ContentPage
{
	public MoviesPage()
	{
		InitializeComponent();
		BindingContext = App.Services.GetService<MoviesViewModel>();
    }
}
