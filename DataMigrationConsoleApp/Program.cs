// See https://aka.ms/new-console-template for more information
using DataLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class Program
{
    public static void Main(string[] args)
    {
        /**
         * Workaround for EF Core issue in Maui
         * Data Migration not possible in Maui app
         * DbContext is moved to class library and console app is set as startup project
         * https://github.com/dotnet/efcore/issues/25938
         */
    }
    public class ContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();

            using (var tempContext = new DataContext(optionsBuilder.Options))
            {
                optionsBuilder.UseSqlite($"Data Source={tempContext.DbPath}",
                    options => options.MigrationsAssembly("DataMigrationConsoleApp"));
            }

            return new DataContext(optionsBuilder.Options);
        }
    }
}

