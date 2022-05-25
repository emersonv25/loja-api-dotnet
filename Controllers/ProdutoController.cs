using api_loja.Data;
using api_loja.Models;
using api_loja.Models.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProdutoController(AppDbContext context)
        {
            _db = context;
        }

        // GET: api/<ProdutoController>
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetAll()
        {
            return _db.Produto.Include(c => c.Categoria).Include(m => m.ModeloProduto).ToList();
        }

        // GET api/<ProdutoController>/5
        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<Produto>> GetById(int id)
        {
            return _db.Produto.Include(c => c.Categoria).Where(i => i.IdProduto == id).ToList();
        }
        // GET api/<CategoriaController>/Placa
        [HttpGet("{nome}")]
        public ActionResult<IEnumerable<Produto>> GetByName(string nome)
        {
            return _db.Produto.Include(c => c.Categoria).Include(m => m.ModeloProduto).Where(i => i.NomeProduto.Contains(nome) || i.Categoria.NomeCategoria.Contains(nome) || i.DescricaoProduto.Contains(nome)).ToList();
        }

        // POST api/<ProdutoController>
        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] ParamProduto param)
        {
            try
            {
                Produto produto = new Produto { NomeProduto = param.NomeProduto, ValorProduto = param.ValorProduto, DescricaoProduto = param.DescricaoProduto, 
                   IdCategoria = param.IdCategoria, FlAtivoProduto = param.FlAtivoProduto, ModeloProduto = param.ModeloProduto };

                _db.Produto.Add(produto);
                await _db.SaveChangesAsync();
                return Ok(produto);
            }
            catch(Exception ex)
            {
                return BadRequest(new { error = "Não foi possivel cadastrar o produto: " + ex.Message });
            }

        }

        // PUT api/<ProdutoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<dynamic>> Put(int id, [FromBody] ParamEditarProduto param)
        {
            try
            {
                Produto produto = await _db.Produto.FindAsync(id);
                if (produto == null)
                {
                    return NotFound(new { error = "Produto não encontrado" });
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
                    if (_db.Categoria.FirstOrDefault(c => c.IdCategoria == param.IdCategoria) == null)
                    {
                        return NotFound(new { error = "Categoria não encontrada" });
                    }
                    produto.IdCategoria = param.IdCategoria.Value;
                }
                if (param.FlAtivoProduto != null && param.FlAtivoProduto != produto.FlAtivoProduto)
                    produto.FlAtivoProduto = param.FlAtivoProduto.Value;

                await _db.SaveChangesAsync();
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Não foi possivel cadastrar o produto: " + ex.Message });
            }
        }

        // DELETE api/<ProdutoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<dynamic>> Delete(int id)
        {
            try
            {
                Produto produto = await _db.Produto.FindAsync(id);
                if (produto == null)
                {
                    return NotFound(new { error = "Produto não encontrado" });
                }
                _db.Produto.Remove(produto);
                await _db.SaveChangesAsync();
                return Ok(new { message = "Deletado com sucesso !" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Não foi possivel deletar o produto: " + ex.Message });
            }

        }
    }
}
