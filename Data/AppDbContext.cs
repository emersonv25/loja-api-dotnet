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
            modelBuilder.Entity<Produto>().Property(p => p.Nome).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Valor).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Produto>().HasData(new Produto{idProduto = 1, Nome = "NVIDIA Geforce RTX", Descricao = "Uma placa de video", Valor = 4999.99m, Quantidade = 10, Ativo = true, idCategoria = 3});
            modelBuilder.Entity<Produto>().HasData(new Produto { idProduto = 2, Nome = "AMD Radeon RX", Descricao = "Uma placa de video", Valor = 4999.99m, Quantidade = 10, Ativo = true, idCategoria = 4 });
            // CATEGORIA
            modelBuilder.Entity<Categoria>().HasKey(p => p.idCategoria);
            modelBuilder.Entity<Categoria>().Property(p => p.Nome).IsRequired();
            modelBuilder.Entity<Categoria>().HasMany(p => p.SubCategorias).WithOne().HasForeignKey(t => t.idPai).HasPrincipalKey(t => t.idCategoria);

            modelBuilder.Entity<Categoria>().HasData(new Categoria { idCategoria = 1, Nome = "Hardware"});
            modelBuilder.Entity<Categoria>().HasData(new Categoria { idCategoria = 2, Nome = "GPU", idPai = 1 });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { idCategoria = 3, Nome = "NVIDIA", idPai = 2 });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { idCategoria = 4, Nome = "AMD", idPai = 2 });

            // USUARIO
            modelBuilder.Entity<Usuario>().HasKey(u => u.idUsuario);
            modelBuilder.Entity<Usuario>().Property(u => u.Username).HasMaxLength(64).IsRequired();
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Username).IsUnique(true);
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique(true);
            modelBuilder.Entity<Usuario>().Property(u => u.Password).HasMaxLength(128).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Ativo).HasMaxLength(2).HasDefaultValue(1).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Cargo).HasDefaultValue("usuario").IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Email).HasMaxLength(256).IsRequired();
            modelBuilder.Entity<Usuario>()
                .HasData(
                    new Usuario { idUsuario = 1, Username = "admin", Nome = "Administrador", Password = "admin", Ativo = 1, Cargo = "admin", Email = "admin@admin.com" }
                );
        }

    }
}