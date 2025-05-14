namespace Prog7311_poe.Models
{

    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

   
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<Farmer> Farmers { get; set; }
            public DbSet<Product> Products { get; set; }
            public DbSet<User> Users { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Seed initial data
                modelBuilder.Entity<User>().HasData(
                    new User { UserId = 1, Username = "employee1", Password = "password123", Role = "Employee" },
                    new User { UserId = 2, Username = "farmer1", Password = "password123", Role = "Farmer", FarmerId = 1 }
                );

                modelBuilder.Entity<Farmer>().HasData(
     new Farmer { FarmerId = 1, Name = "Green Valley Farm", Location = "Western Cape", ContactNumber = "0721234567" },
     new Farmer { FarmerId = 2, Name = "Sunshine Organics", Location = "Eastern Cape", ContactNumber = "0829876543" }
 );

                modelBuilder.Entity<Product>().HasData(
                    new Product { ProductId = 1, Name = "Organic Tomatoes", Category = "Vegetables", ProductionDate = new DateTime(2024, 10, 5), FarmerId = 1 },
                    new Product { ProductId = 2, Name = "Free-range Eggs", Category = "Poultry Products", ProductionDate = new DateTime(2024, 10, 8), FarmerId = 1 },
                    new Product { ProductId = 3, Name = "Solar Panels", Category = "Energy Equipment", ProductionDate = new DateTime(2024, 9, 30), FarmerId = 2 }
                );

            }
        }
    }

    

