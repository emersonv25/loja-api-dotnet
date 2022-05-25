using api_produtos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Proxies;
using api_produtos.Services;

namespace api_produtos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<ModeloProduto> ModeloProduto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PRODUTO
            modelBuilder.Entity<Produto>().HasKey(p => p.IdProduto);
            modelBuilder.Entity<Produto>().Property(p => p.NomeProduto).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.ValorProduto).HasColumnType("decimal(18,2)").IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.DescontoProduto).HasColumnType("decimal(18,2)").IsRequired().HasDefaultValue(0.00m);
            modelBuilder.Entity<Produto>().Property(p => p.FlAtivoProduto).HasDefaultValueSql("1").IsRequired();

            modelBuilder.Entity<Produto>().HasData(new Produto{IdProduto = 1, NomeProduto = "NVIDIA Geforce RTX", DescricaoProduto = "Uma placa de video", ValorProduto = 4999.99m, DescontoProduto = 4000.00m, EstoqueProduto = 10, FlAtivoProduto = true, IdCategoria = 3});
            modelBuilder.Entity<ModeloProduto>().HasData(new ModeloProduto { IdModeloProduto = 1, NomeModelo = "3080 12GB", IdProduto = 1 });
            modelBuilder.Entity<ModeloProduto>().HasData(new ModeloProduto { IdModeloProduto = 2, NomeModelo = "3070 8GB", IdProduto = 1 });

            modelBuilder.Entity<Produto>().HasData(new Produto { IdProduto = 2, NomeProduto = "AMD Radeon RX", DescricaoProduto = "Uma placa de video", ValorProduto = 4999.99m, DescontoProduto = 4000.00m, EstoqueProduto = 10, FlAtivoProduto = true, IdCategoria = 4 });


            // MODELOPRODUTO
            modelBuilder.Entity<ModeloProduto>().Property(p => p.FlAtivoModelo).HasDefaultValueSql("1").IsRequired();

            modelBuilder.Entity<ModeloProduto>().HasData(new ModeloProduto { IdModeloProduto = 3, NomeModelo = "6800 12GB", IdProduto = 2 });
            modelBuilder.Entity<ModeloProduto>().HasData(new ModeloProduto { IdModeloProduto = 4, NomeModelo = "6700 8GB", IdProduto = 2 });

            // CATEGORIA
            modelBuilder.Entity<Categoria>().HasKey(p => p.IdCategoria);
            modelBuilder.Entity<Categoria>().Property(p => p.NomeCategoria).IsRequired();
            modelBuilder.Entity<Categoria>().Property(p => p.FlAtivoCategoria).HasDefaultValueSql("1").IsRequired();
            modelBuilder.Entity<Categoria>().HasMany(p => p.SubCategorias).WithOne().HasForeignKey(t => t.IdCategoriaPai).HasPrincipalKey(t => t.IdCategoria);

            modelBuilder.Entity<Categoria>().HasData(new Categoria { IdCategoria = 1, NomeCategoria = "Hardware"});
            modelBuilder.Entity<Categoria>().HasData(new Categoria { IdCategoria = 2, NomeCategoria = "GPU", IdCategoriaPai = 1 });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { IdCategoria = 3, NomeCategoria = "NVIDIA", IdCategoriaPai = 2 });
            modelBuilder.Entity<Categoria>().HasData(new Categoria { IdCategoria = 4, NomeCategoria = "AMD", IdCategoriaPai = 2 });

            // USUARIO
            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Usuario>().Property(u => u.Username).HasMaxLength(64).IsRequired();
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Username).IsUnique(true);
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Email).IsUnique(true);
            modelBuilder.Entity<Usuario>().Property(u => u.Password).HasMaxLength(128).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.FlAtivoUsuario).HasDefaultValueSql("1").IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Admin).HasDefaultValueSql("0").IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.Email).HasMaxLength(256).IsRequired();
            modelBuilder.Entity<Usuario>().Property(u => u.DataCadastro).HasDefaultValueSql("getDate()").IsRequired();
            modelBuilder.Entity<Usuario>()
                .HasData(
                    new Usuario { IdUsuario = 1, Username = "admin", NomeCompleto = "Administrador", Password = Utils.sha256_hash("admin"), FlAtivoUsuario = true, Admin = true, Email = "admin@admin.com" }
                );
        }

    }
}