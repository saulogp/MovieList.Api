using Microsoft.EntityFrameworkCore;
using MovieList.Models;

namespace MovieList.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseSqlite(connectionString:"DataSource=app.db;Cache=Shared");
    }
}