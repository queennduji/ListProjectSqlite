using System.Reflection;
using DataLibrary.Data;
using ListProjectSqlite.Helpers;
using ListProjectSqlite.Services;
using ListProjectSqlite.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ListProjectSqlite;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        var tempOptions = new DbContextOptionsBuilder<DataContext>().Options;
        using var tempContext = new DataContext(tempOptions);
        var dbPath = tempContext.DbPath;

        var dbDirectory = Path.GetDirectoryName(dbPath);

        if (!Directory.Exists(dbDirectory))
        {
            Directory.CreateDirectory(dbDirectory);
        }

        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite($"Data Source={dbPath}");
        });

        builder.Services.AddScoped<IMovieService, SqlLiteMovieService>();

        /*Uncomment and comment the dependency injection for 
         * SqlLiteMovieService if you want to use web api instead 
         */
        // builder.Services.AddHttpClient<IMovieService, WebApiService>(client =>
        // {
        //     client.BaseAddress = new Uri("https://localhost:7291/");
        // })
        //.ConfigurePrimaryHttpMessageHandler(() => new CustomHttpClientHandler());

        builder.Services.AddTransient<MoviesViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddTransient<MoviesViewModel>();

        var mauiApp = builder.Build();

        App.Services = mauiApp.Services;


        ApplyMigrations(mauiApp);

        return mauiApp;
    }

    private static void ApplyMigrations(MauiApp mauiApp)
    {
        using (var scope = mauiApp.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DataContext>();
            db.Database.Migrate();
        }
    }

}

