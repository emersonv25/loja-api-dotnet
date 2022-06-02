using api_loja.Data;
using api_loja.Models;
using api_loja.Models.Object;
using api_loja.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _db;
        public ProdutoService(AppDbContext context)
        {
            _db = context;

        }
        public ICollection<ViewProduto> GetAll()
        {
            ICollection<Produto> produtos = _db.Produto.Include(c => c.Categoria).Include(m => m.ModeloProduto).ToList();
            List<ViewProduto> viewProduto = new List<ViewProduto>();
            foreach (Produto p in produtos)
            {
                ViewProduto v = new ViewProduto
                {
                    IdProduto = p.IdProduto,
                    NomeProduto = p.NomeProduto,
                    DescricaoProduto = p.DescricaoProduto,
                    ValorProduto = p.ValorProduto,
                    DescontoProduto = p.DescontoProduto,
                    FlAtivoProduto = p.FlAtivoProduto,
                    ModeloProduto = p.ModeloProduto,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria,
                    Imagens = new List<string> { "url", "url" }
                };
                viewProduto.Add(v);
            }
            return viewProduto;
        }
        public ViewProduto GetById(int id)
        {
            Produto p = _db.Produto.Include(c => c.Categoria).SingleOrDefault(i => i.IdProduto == id);

            ViewProduto viewProduto = new ViewProduto
            {
                IdProduto = p.IdProduto,
                NomeProduto = p.NomeProduto,
                DescricaoProduto = p.DescricaoProduto,
                ValorProduto = p.ValorProduto,
                DescontoProduto = p.DescontoProduto,
                FlAtivoProduto = p.FlAtivoProduto,
                ModeloProduto = p.ModeloProduto,
                IdCategoria = p.IdCategoria,
                Categoria = p.Categoria,
                Imagens = new List<string> { "url", "url" }
            };

            return viewProduto;
        }
        public ICollection<ViewProduto> GetByName(string nome)
        {
            ICollection<Produto> produtos = _db.Produto.Include(c => c.Categoria).Include(m => m.ModeloProduto).Where(i => i.NomeProduto.Contains(nome) || i.Categoria.NomeCategoria.Contains(nome) || i.DescricaoProduto.Contains(nome)).ToList();
            List<ViewProduto> viewProduto = new List<ViewProduto>();
            foreach (Produto p in produtos)
            {
                ViewProduto v = new ViewProduto
                {
                    IdProduto = p.IdProduto,
                    NomeProduto = p.NomeProduto,
                    DescricaoProduto = p.DescricaoProduto,
                    ValorProduto = p.ValorProduto,
                    DescontoProduto = p.DescontoProduto,
                    FlAtivoProduto = p.FlAtivoProduto,
                    ModeloProduto = p.ModeloProduto,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria,
                    Imagens = new List<string> { "url", "url" }
                };
                viewProduto.Add(v);
            }
            return viewProduto;
        }
        public async Task<bool> Post(ParamProduto param)
        {

            Produto produto = new Produto
            {
                NomeProduto = param.NomeProduto,
                ValorProduto = param.ValorProduto,
                DescricaoProduto = param.DescricaoProduto,
                IdCategoria = param.IdCategoria,
                FlAtivoProduto = param.FlAtivoProduto,
                ModeloProduto = param.ModeloProduto
            };

            _db.Produto.Add(produto);
            await _db.SaveChangesAsync();
            return true;

        }
        public async Task<bool> Put(int id, ParamEditarProduto param)
        {

            Produto produto = await _db.Produto.FindAsync(id);
            if (produto == null)
            {
                throw new Exception("Produto não encontrado");
            }

            if (!string.IsNullOrEmpty(param.NomeProduto))
                produto.NomeProduto = param.NomeProduto;
            if (!string.IsNullOrEmpty(param.DescricaoProduto))
                produto.DescricaoProduto = param.DescricaoProduto;
            if (param.ValorProduto != null && param.ValorProduto > 0)
                produto.ValorProduto = param.ValorProduto.Value;
            if (param.DescontoProduto != null && param.DescontoProduto > 0)
                produto.DescontoProduto = param.DescontoProduto.Value;
            if (param.IdCategoria != null && param.IdCategoria > 0)
            {
                if (_db.Categoria.Find(param.IdCategoria) == null)
                {
                    throw new Exception("Categoria não encontrada");
                }
                produto.IdCategoria = param.IdCategoria.Value;
            }
            if (param.FlAtivoProduto != null && param.FlAtivoProduto != produto.FlAtivoProduto)
                produto.FlAtivoProduto = param.FlAtivoProduto.Value;

            await _db.SaveChangesAsync();
            return true;

        }
        public async Task<bool> Delete(int id)
        {

            Produto produto = await _db.Produto.FindAsync(id);
            if (produto == null)
            {
                throw new Exception("Produto não encontrado");
            }
            _db.Produto.Remove(produto);
            await _db.SaveChangesAsync();
            return true;

        }
    }
}
