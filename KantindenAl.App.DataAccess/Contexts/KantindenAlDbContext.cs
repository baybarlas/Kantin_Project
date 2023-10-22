using KantindenAl.App.DataAccess.Identity;
using KantindenAl.App.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KantindenAl.App.DataAccess.Contexts
{
    public class KantindenAlDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public KantindenAlDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleDetail> SaleDetails { get; set; }
        public DbSet<WalletActivity> WalletActivities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {



            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Yiyecek", Description = "Yiyecek çeşitleri" },
                new Category { Id = 2, Name = "İçecek", Description = "İçecek çeşitleri" },
                new Category { Id = 3, Name = "Şekerleme", Description = "Şekerleme çeşitleri" }
                );

            builder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Albeni", UnitPrice = 5.5M, Stock = 25, CategoryId = 3, SchoolId = 1, Description = "Albeni çikolata", IsPopuler = true, ImageUrl = "/images/Albeni.jpg" },

                new Product { Id = 2, Name = "Pepsi Kola", UnitPrice = 15M, Stock = 25, CategoryId = 2, SchoolId = 1, Description = "Pepsi kola", IsPopuler = false, ImageUrl = "/images/PepsiKola.jpg" },

                new Product { Id = 3, Name = "Tabldot Menü", UnitPrice = 15.53M, Stock = 25, CategoryId = 1, SchoolId = 1, Description = "Mercimek Çorbası, Patlıcan Kebabı, Pilav", IsPopuler = true, ImageUrl = "/images/Mercimek.jpg" },

                new Product { Id = 4, Name = "Meyve Suyu", UnitPrice = 10m, Stock = 25, CategoryId = 3,  SchoolId = 2, Description = "Meyve suyu", IsPopuler = true, ImageUrl = "/images/MeyveSuyu.jpg" },

                new Product { Id = 5, Name = "Su", UnitPrice = 5m, Stock = 25, CategoryId = 1 , SchoolId = 2, Description = "Meyve suyu", IsPopuler = true, ImageUrl = "/images/MeyveSuyu.jpg" },

                new Product { Id = 6, Name = "Fanta", UnitPrice = 10m, Stock = 25, CategoryId = 2, SchoolId = 2, Description = "Meyve suyu", IsPopuler = true, ImageUrl = "/images/MeyveSuyu.jpg" },

                new Product { Id = 7, Name = "Coca Cola", UnitPrice = 10m, Stock = 25, CategoryId = 3, SchoolId = 2, Description = "Meyve suyu", IsPopuler = true, ImageUrl = "/images/MeyveSuyu.jpg" },

                new Product { Id = 8, Name = "Meyve Suyu", UnitPrice = 10m, Stock = 25, CategoryId = 2, SchoolId = 2, Description = "Meyve suyu", IsPopuler = true, ImageUrl = "/images/MeyveSuyu.jpg" }
                );

            builder.Entity<School>().HasData
                (
                    new School { Id = 1, Name = "Deneme İlkokulu", Address = "Beşiktaş/Istanbul", Phone = "0212 256 2565" },
                    new School { Id = 2, Name = "Çekmeköy İlkokulu", Address = "Çekmeköy/Istanbul", Phone = "0216 256 2565" },
                    new School { Id = 3, Name = "Beykoz Ortaokulu", Address = "Beykoz/Istanbul", Phone = "0216 325 2155" },
                    new School { Id = 4, Name = "Bakırköy İlkokulu", Address = "Bakırköy/Istanbul", Phone = "0212 256 6592" }
                );

            builder.Entity<Product>().
                HasOne(s => s.School).WithMany(s => s.Products).HasForeignKey(p => p.SchoolId);


            base.OnModelCreating(builder);
        }
    }
}
