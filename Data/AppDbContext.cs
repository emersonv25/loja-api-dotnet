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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>().HasKey(p => p.Id);
            modelBuilder.Entity<Produto>().Property(p => p.Nome).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Valor).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Categoria>().HasKey(p => p.Id);
            modelBuilder.Entity<Categoria>().Property(p => p.Nome).IsRequired();


            modelBuilder.Entity<Categoria>()
            .HasData(
                new Categoria {Id = 1, Nome = "Hardware"}
            );
            modelBuilder.Entity<Produto>()
            .HasData(
                new Produto {Id = 1, Nome = "NVIDIA Geforce RTX", Descricao = "Uma placa de video", 
                    Valor = 4999.99m, Quantidade = 10, Ativo = true, CategoriaId  = 1}
            );
        }

    }
}