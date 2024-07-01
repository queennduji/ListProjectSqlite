using ListProjectSqlite.Views;
using Microsoft.EntityFrameworkCore;

namespace ListProjectSqlite;

public partial class App : Application
{
    public static IServiceProvider Services { get; set; }
    public App(IServiceProvider serviceProvider)
	{

		InitializeComponent();

        //MainPage = new AppShell();

        MainPage = new NavigationPage(new MoviesPage());

        Services = serviceProvider;
	}
}

