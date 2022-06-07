using api_loja.Controllers;
using api_loja.Data;
using api_loja.Models;
using api_loja.Models.Object;
using api_loja.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;

        public ProdutoService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _db = context;

        }
        public Retorno GetAll()
        {
            ICollection<Produto> produtos = _db.Produto.Include(c => c.Categoria).Include(m => m.ModeloProduto).Include(i => i.ImagemProduto).ToList();
            List<ViewProduto> retornoProduto = new List<ViewProduto>();
            foreach (Produto p in produtos)
            {
                ViewProduto v = new ViewProduto
                {
                    IdProduto = p.IdProduto,
                    NomeProduto = p.NomeProduto,
                    DescricaoProduto = p.DescricaoProduto,
                    ValorProduto = p.ValorProduto,
                    DescontoProduto = p.DescontoProduto,
                    ValorComDesconto = p.ValorProduto - p.DescontoProduto,
                    FlAtivoProduto = p.FlAtivoProduto,
                    ModeloProduto = p.ModeloProduto,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria,
                    Imagens = GetUrlImagemProduto(p.ImagemProduto)
                };
                retornoProduto.Add(v);
            }

            #region Retorno
            Retorno retorno = new Retorno();
            retorno.TotalDeRegistros = retornoProduto.Count();
            retorno.PaginaAtual = 1;
            retorno.ItensPorPagina = 0;
            retorno.Registros = retornoProduto.ToList<dynamic>();
            #endregion

            return retorno;
        }
        public ViewProduto GetById(int id)
        {
            Produto p = _db.Produto.Include(c => c.Categoria).Include(m => m.ModeloProduto).Include(i => i.ImagemProduto).SingleOrDefault(i => i.IdProduto == id);

            if(p == null)
            {
                return null;
            }

            ViewProduto viewProduto = new ViewProduto
            {
                IdProduto = p.IdProduto,
                NomeProduto = p.NomeProduto,
                DescricaoProduto = p.DescricaoProduto,
                ValorProduto = p.ValorProduto,
                DescontoProduto = p.DescontoProduto,
                ValorComDesconto = p.ValorProduto - p.DescontoProduto,
                FlAtivoProduto = p.FlAtivoProduto,
                ModeloProduto = p.ModeloProduto,
                IdCategoria = p.IdCategoria,
                Categoria = p.Categoria,
                Imagens = GetUrlImagemProduto(p.ImagemProduto)
            };

            return viewProduto;
        }
        public Retorno GetByName(string nome)
        {
            ICollection<Produto> produtos = _db.Produto.Include(c => c.Categoria).Include(m => m.ModeloProduto).Include(i => i.ImagemProduto).Where(i => i.NomeProduto.Contains(nome) || i.Categoria.NomeCategoria.Contains(nome) || i.DescricaoProduto.Contains(nome)).ToList();
            List<ViewProduto> retornoProduto = new List<ViewProduto>();
            foreach (Produto p in produtos)
            {
                ViewProduto v = new ViewProduto
                {
                    IdProduto = p.IdProduto,
                    NomeProduto = p.NomeProduto,
                    DescricaoProduto = p.DescricaoProduto,
                    ValorProduto = p.ValorProduto,
                    DescontoProduto = p.DescontoProduto,
                    ValorComDesconto = p.ValorProduto - p.DescontoProduto,
                    FlAtivoProduto = p.FlAtivoProduto,
                    ModeloProduto = p.ModeloProduto,
                    IdCategoria = p.IdCategoria,
                    Categoria = p.Categoria,
                    Imagens = GetUrlImagemProduto(p.ImagemProduto)
                };
                retornoProduto.Add(v);
            }
            #region Retorno
            Retorno retorno = new Retorno();
            retorno.TotalDeRegistros = retornoProduto.Count();
            retorno.PaginaAtual = 1;
            retorno.ItensPorPagina = 0;
            retorno.Registros = retornoProduto.ToList<dynamic>();
            #endregion

            return retorno;
        }
        public async Task<bool> Post(ObjectProduto param, IFormFileCollection files)
        {

            List<ModeloProduto> modelos = new List<ModeloProduto>();
            foreach (ParamModeloProduto m in param.ModeloProduto)
            {
                ModeloProduto modelo = new ModeloProduto
                {
                    NomeModelo = m.NomeModelo,
                    Estoque = m.Estoque
                };
                modelos.Add(modelo);
            }
            List<string> paths = Utils.SaveFiles(files, _configuration["Directories:ImagesPath"]); // Salva as fotos e obtem o path
            List<ImagemProduto> imagens = new List<ImagemProduto>();
            foreach(string path in paths)
            {
                ImagemProduto image = new ImagemProduto
                {
                    Path = path
                };
                imagens.Add(image);
            }

            Produto produto = new Produto
            {
                NomeProduto = param.NomeProduto,
                ValorProduto = param.ValorProduto,
                DescontoProduto = param.DescontoProduto,
                DescricaoProduto = param.DescricaoProduto,
                IdCategoria = param.IdCategoria,
                FlAtivoProduto = param.FlAtivoProduto,
                ModeloProduto = modelos,
                ImagemProduto = imagens
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
        #region Files
        private List<string> GetUrlImagemProduto(ICollection<ImagemProduto> imagemProduto)
        {
            List<string> urls = new List<string>();
            foreach (ImagemProduto i in imagemProduto)
            {
                urls.Add(Utils.GetFileUrl(i.Path, _configuration["Directories:BaseUrl"], _configuration["Directories:ImagesPath"]));
            }
            return urls;
        }
        #endregion
    }
}
