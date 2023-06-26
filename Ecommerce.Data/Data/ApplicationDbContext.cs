using Ecommerce.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Data.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //relation  (pending)
            modelBuilder.Entity<Product>().HasOne<Category>(u => u.Category).WithMany(u => u.Products).HasForeignKey(c => c.CategoryId);
            //Seed product data
            modelBuilder.Entity<Product>().HasData(
                new Product {Id=1,Description="Test Description",Name="Test Name" ,CreatedDate=DateTime.Now,CategoryId=1},
                new Product { Id = 2, Description = "Dummy Description", Name = "Dummy Name", CreatedDate = DateTime.Now, CategoryId = 2 },
                new Product { Id = 3, Description = "Dummy1 Description", Name = "Dummy1 Name", CreatedDate = DateTime.Now, CategoryId = 2 },
                new Product { Id = 4, Description = "Dummy4 Description", Name = "Dummy4 Name", CreatedDate = DateTime.Now, CategoryId = 2 },
                new Product { Id = 5, Description = "Dummy5 Description", Name = "Dummy5 Name", CreatedDate = DateTime.Now, CategoryId = 2 },
                new Product { Id = 6, Description = "Dummy6 Description", Name = "Dummy6 Name", CreatedDate = DateTime.Now, CategoryId = 2 }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId=1,Name="Test Category",Description="Test Category Description",CreatedDate=DateTime.Now},
                new Category { CategoryId = 2, Name = "Dummy Category", Description = "Dummy Category Description", CreatedDate = DateTime.Now }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.
        }
    }
}
