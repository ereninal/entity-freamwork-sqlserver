using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace entity_freamwork_sqlserver_example
{

    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlServer(@"Data Source = .\SQLEXPRESS;Initial Catalog=ShopDb;Integrated Security=SSPI; ")
            .UseLoggerFactory(MyLoggerFactory);
            
        }
    }
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public decimal? Price { get; set; }


    }
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }

    }
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
