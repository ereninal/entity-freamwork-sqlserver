﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace entity_freamwork_sqlserver_example
{

    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlServer(@"Data Source = EREN-LAPTOP\SQLEXPRESS;Initial Catalog=ShopDb;Integrated Security=SSPI; ")
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
        public int CategoryId { get; set; }


    }
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string name { get; set; }

    }
    public class Order
    {
        public int Id { get; set; }
        public int Productıd { get; set; }
        public DateTime DateAdded { get; set; }

    } 

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Customer Customer { get; set; }
        public List<Adress> Adresses {get; set;}

    }
    public class Customer
    {
        public int Id { get; set; }
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

    }
    public class Supplier
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string TaxNumber { get; set; }
    }
    public class Adress
    {
        public int Id { get; set; }
        public string Fullname  { get; set; }
        public string Title { get;set ;}
        public User User {get; set;}
        public int? UserId { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //AddProducts();
            //GetProductById(3);
            //GetProducts();
            //UpdateProduct(2,"Xaiomi Note",2000);
            //InsertUser("Erdem AL","erdemal@gmail.com");
            //UpdateUser(4,"Onur Gençoğlu","onurgencoglu@gmail.com");
            InsertAdres("ereninal1@gmail.com","Eren","İş - 2");

        }
        static void AddProducts()
        {
            using(var db = new ShopContext())
            {
                var products = new List<Product>()
                {
                    new Product{Name = "Apple 5", Price =5000},
                    new Product{Name = "Apple 5S", Price =6000},
                    new Product{Name = "Apple 6", Price =7000},
                    new Product{Name = "Apple 6S", Price =8000},
                    new Product{Name = "Apple 6 PLUS", Price =9000},
                    new Product{Name = "Apple 7", Price =10000},
                    new Product{Name = "Apple 8", Price =11000}
                };
                db.Products.AddRange(products);
                db.SaveChanges();
                Console.WriteLine("Datas added");
            }
        }
        static void AddProduct(string name,decimal price)
        {
            using(var db = new ShopContext())
            {
                var product = new Product(){Name =name,Price=price};
                db.Products.AddRange(product);
                db.SaveChanges();
                Console.WriteLine("Data added");
            }
        }
        static void GetProductById(int id)
        {
            using(var db = new ShopContext())
            {
                var product = db.Products.Where(p => p.Id ==id).FirstOrDefault();
                Console.WriteLine($"name :{product.Name}");
            }
        }
        static void GetProducts()
        {
            using(var db = new ShopContext())
            {
                var product = db.Products.Select(p => new {
                    p.Name,
                    p.Price
                }).ToList();
                foreach (var item in product)
                {
                    Console.WriteLine($"Name : {item.Name}");
                }
            }
        }
        static void UpdateProduct(int id,string name,decimal price)
        {
            using(var db = new ShopContext())
            {
                var query = new Product(){Id = id};
                db.Products.Attach(query);
                query.Name = name;
                query.Price =price;
                db.SaveChanges();
                Console.WriteLine("Entry updated");
            }
        }
        static void InsertUser(string name,string email)
        {
            using(var db = new ShopContext())
            {
                var user = new User()
                {
                    Name = name,
                    Email = email
                };
                db.Users.AddRange(user);
                db.SaveChanges();
                Console.WriteLine("User Added");
            }
        }
        static void UpdateUser(int id,string name,string email)
        {
            using(var db = new ShopContext())
            {
                var query = new User(){Id=id};
                db.Users.Attach(query);
                query.Name =name;
                query.Email = email;
                db.SaveChanges();
                Console.Write("Entry updated");
            }
            
        }
        static void InsertAdres(string username,string fullname,string title)
        {
            using (var db = new ShopContext())
            {
                var user = db.Users.FirstOrDefault(u=>u.Email==username);
                if(user != null)
                {
                    user.Adresses = new List<Adress>();
                    user.Adresses.AddRange(new List<Adress>()
                    {
                        new Adress(){Fullname = fullname,Title=title}
                    });
                    db.SaveChanges();
                    Console.WriteLine("Adres data added");
                }
                else
                    Console.WriteLine("Adres");
                /*var adres = new Adress()
                {
                    Fullname = fullname,
                    Title = title,
                    UserId = userid
                };
                db.Adresses.AddRange(adres);
                db.SaveChanges();*/
                
            }
        }
    }

}
