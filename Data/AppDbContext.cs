using api_produtos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            modelBuilder.Entity<Produto>().HasKey(p => p.Id);
            modelBuilder.Entity<Produto>().Property(p => p.Nome).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Valor).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Produto>()
            .HasData(
                new Produto
                {
                    Id = 1,
                    Nome = "NVIDIA Geforce RTX",
                    Descricao = "Uma placa de video",
                    Valor = 4999.99m,
                    Quantidade = 10,
                    Ativo = true,
                    CategoriaId = 1
                }
            );
            // CATEGORIA
            modelBuilder.Entity<Categoria>().HasKey(p => p.Id);
            modelBuilder.Entity<Categoria>().Property(p => p.Nome).IsRequired();
            modelBuilder.Entity<Categoria>().HasData(new Categoria {Id = 1, Nome = "Hardware"}
);


            // USUARIO
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            modelBuilder.Entity<Usuario>().Property(u => u.Username).HasMaxLength(64).IsRequired();
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Username).IsUnique(true);
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique(true);
            modelBuilder.Entity<Usuario>().Property(u => u.Password).HasMaxLength(128).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Ativo).HasMaxLength(2).HasDefaultValue(1).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Cargo).HasDefaultValue("usuario").IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Email).HasMaxLength(256).IsRequired();
            modelBuilder.Entity<Usuario>()
                .HasData(
                    new Usuario { Id = 1, Username = "admin", Nome = "Administrador", Password = "admin", Ativo = 1, Cargo = "admin", Email = "admin@admin.com" }
                );
        }

    }
}