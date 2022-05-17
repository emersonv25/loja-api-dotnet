using api_produtos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Proxies;


namespace api_produtos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PRODUTO
            modelBuilder.Entity<Produto>().HasKey(p => p.idProduto);
            modelBuilder.Entity<Produto>().Property(p => p.nmProduto).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.vlProduto).HasColumnType("decimal(18,2)").IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.vlPromocional).HasColumnType("decimal(18,2)").IsRequired().HasDefaultValue(0.00m);
            modelBuilder.Entity<Produto>().Property(p => p.flAtivo).HasDefaultValueSql("1").IsRequired();
            modelBuilder.Entity<Produto>().HasData(new Produto{idProduto = 1, nmProduto = "NVIDIA Geforce RTX", dsProduto = "Uma placa de video", vlProduto = 4999.99m, vlPromocional = 4000.00m, qtdEstoque = 10, flAtivo = true, idCategoria = 3});
            modelBuilder.Entity<Produto>().HasData(new Produto { idProduto = 2, nmProduto = "AMD Radeon RX", dsProduto = "Uma placa de video", vlProduto = 4999.99m, vlPromocional = 4000.00m, qtdEstoque = 10, flAtivo = true, idCategoria = 4 });
            
            // CATEGORIA
            modelBuilder.Entity<Categoria>().HasKey(p => p.idCategoria);
            modelBuilder.Entity<Categoria>().Property(p => p.nmCategoria).IsRequired();
            modelBuilder.Entity<Categoria>().HasMany(p => p.SubCategorias).WithOne().HasForeignKey(t => t.idPai).HasPrincipalKey(t => t.idCategoria);

            modelBuilder.Entity<Categoria>().HasData(new Categoria { idCategoria = 1, nmCategoria = "Hardware"});
            modelBuilder.Entity<Categoria>().HasData(new Categoria { idCategoria = 2, nmCategoria = "GPU", idPai = 1 });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { idCategoria = 3, nmCategoria = "NVIDIA", idPai = 2 });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { idCategoria = 4, nmCategoria = "AMD", idPai = 2 });

            // USUARIO
            modelBuilder.Entity<Usuario>().HasKey(u => u.idUsuario);
            modelBuilder.Entity<Usuario>().Property(u => u.Username).HasMaxLength(64).IsRequired();
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Username).IsUnique(true);
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique(true);
            modelBuilder.Entity<Usuario>().Property(u => u.Password).HasMaxLength(128).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.flAtivo).HasDefaultValueSql("1").IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Cargo).HasDefaultValue("usuario").IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Email).HasMaxLength(256).IsRequired();
            modelBuilder.Entity<Usuario>()
                .HasData(
                    new Usuario { idUsuario = 1, Username = "admin", Nome = "Administrador", Password = "admin", flAtivo = true, Cargo = "admin", Email = "admin@admin.com" }
                );
        }

    }
}