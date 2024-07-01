using System.Data;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DataLibrary.Data
{
    public class DataContext : DbContext
    {
        public virtual DbSet<Movie> Movies { get; set; }

        public string DbPath { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
           : base(options)
        {
            EnsureTablesCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                DbPath = System.IO.Path.Join(path, "movies.db");
                options.UseSqlite($"Data Source={DbPath}");
            }
        }

        public void EnsureTablesCreated()
        {
            using var connection = Database.GetDbConnection();
            connection.Open();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Movies (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Genre TEXT NOT NULL
                );";
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}

