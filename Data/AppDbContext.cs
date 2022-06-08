using api_loja.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_loja.Services;

namespace api_loja.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PRODUTO
            modelBuilder.Entity<Product>().HasKey(p => p.ProductId);
            modelBuilder.Entity<Product>().Property(p => p.Title).IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Value).HasColumnType("decimal(18,2)").IsRequired();
            modelBuilder.Entity<Product>().Property(p => p.Discount).HasColumnType("decimal(18,2)").IsRequired().HasDefaultValue(0.00m);
            modelBuilder.Entity<Product>().Property(p => p.Enabled).HasDefaultValueSql("1").IsRequired();

            modelBuilder.Entity<Product>().HasData(new Product{ProductId = 1, Title = "NVIDIA Geforce RTX", Description = "Uma placa de video", Value = 4999.99m, Discount = 400.00m, Enabled = true, CategoryId = 3});
            modelBuilder.Entity<ProductType>().HasData(new ProductType { ProductTypeId = 1, Title = "3080 12GB", Inventory = 10, ProductId = 1 });
            modelBuilder.Entity<ProductType>().HasData(new ProductType { ProductTypeId = 2, Title = "3070 8GB", Inventory = 10, ProductId = 1 });

            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 2, Title = "AMD Radeon RX", Description = "Uma placa de video", Value = 4999.99m, Discount = 400.00m, Enabled = true, CategoryId = 4 });


            // MODELOPRODUTO
            modelBuilder.Entity<ProductType>().Property(m => m.Enabled).HasDefaultValueSql("1").IsRequired();
            modelBuilder.Entity<ProductType>().Property(m => m.ProductId).IsRequired();

            modelBuilder.Entity<ProductType>().HasData(new ProductType { ProductTypeId = 3, Title = "6800 12GB", Inventory = 10, ProductId = 2 });
            modelBuilder.Entity<ProductType>().HasData(new ProductType { ProductTypeId = 4, Title = "6700 8GB", Inventory = 10, ProductId = 2 });

            // IMAGEM PRODUTO
            modelBuilder.Entity<ProductImage>().Property(i => i.ProductId).IsRequired();
            modelBuilder.Entity<ProductImage>().HasData(new ProductImage { ProductImageId = 1,Path =  "default.png", ProductId = 1 });
            modelBuilder.Entity<ProductImage>().HasData(new ProductImage { ProductImageId = 4, Path = "default.png", ProductId = 2 });

            // CATEGORIA
            modelBuilder.Entity<Category>().HasKey(p => p.CategoryId);
            modelBuilder.Entity<Category>().Property(p => p.Title).IsRequired();
            modelBuilder.Entity<Category>().Property(p => p.Enabled).HasDefaultValueSql("1").IsRequired();
            modelBuilder.Entity<Category>().HasMany(p => p.SubCategories).WithOne().HasForeignKey(t => t.CategoryParentId).HasPrincipalKey(t => t.CategoryId);

            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 1, Title = "Hardware"});
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 2, Title = "GPU", CategoryParentId = 1 });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 3, Title = "NVIDIA", CategoryParentId = 2 });
            modelBuilder.Entity<Category>().HasData(new Category { CategoryId = 4, Title = "AMD", CategoryParentId = 2 });

            // USUARIO
            modelBuilder.Entity<User>().HasKey(u => u.IdUser);
            modelBuilder.Entity<User>().Property(u => u.Username).HasMaxLength(64).IsRequired();
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique(true);
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique(true);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Enabled).HasDefaultValueSql("1").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Admin).HasDefaultValueSql("0").IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Email).HasMaxLength(256).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.CreatedAt).IsRequired();
            modelBuilder.Entity<User>()
                .HasData(
                    new User { IdUser = 1, Username = "admin", FullName = "Administrador", Password = Utils.sha256_hash("admin"), Enabled = true, Admin = true, Email = "admin@admin.com"}
                );
        }

    }
}