using DataFetchingAndSqLiteManupilation.Models;
using Microsoft.EntityFrameworkCore;

namespace DataFetchingAndSqLiteManupilation.Context
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=D:\\CustomeFoldersEyup\\DataFetchingAndSqLiteManupilation\\DataFetchingAndSqLiteManupilation\\Db\\ProductsDb");
        }

        public DbSet<Product> Products { get; set; }
    }
}
