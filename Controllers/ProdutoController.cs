using api_produtos.Data;
using api_produtos.Models;
using api_produtos.Models.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_produtos.Controllers
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
            return _db.Produto.Include(x => x.Categoria).ToList();
        }

        // GET api/<ProdutoController>/5
        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<Produto>> GetById(int id)
        {
            return _db.Produto.Include(x => x.Categoria).Where(i => i.idProduto == id).ToList();
        }
        // GET api/<CategoriaController>/Placa
        [HttpGet("{nome}")]
        public ActionResult<IEnumerable<Produto>> GetByName(string nome)
        {
            return _db.Produto.Include(i => i.Categoria).Where(i => i.nmProduto.Contains(nome) || i.Categoria.nmCategoria.Contains(nome) || i.dsProduto.Contains(nome)).ToList();
        }

        // POST api/<ProdutoController>
        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] ParamProduto param)
        {
            try
            {
                Produto produto = new Produto { nmProduto = param.nmProduto, vlProduto = param.vlProduto, dsProduto = param.dsProduto, 
                    qtdEstoque = param.qtdEstoque, idCategoria = param.idCategoria, flAtivo = param.flAtivo };

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

                if (!String.IsNullOrEmpty(param.nmProduto))
                    produto.nmProduto = param.nmProduto;
                if (!String.IsNullOrEmpty(param.dsProduto))
                    produto.dsProduto = param.dsProduto;
                if (param.vlProduto != null && param.vlProduto > 0)
                    produto.vlProduto = param.vlProduto.Value;
                if (param.vlPromocional != null && param.vlPromocional > 0)
                    produto.vlPromocional = param.vlPromocional.Value;
                if (param.idCategoria != null && param.idCategoria > 0)
                {
                    if (_db.Categoria.FirstOrDefault(c => c.idCategoria == param.idCategoria) == null)
                    {
                        return NotFound(new { error = "Categoria não encontrada" });
                    }
                    produto.idCategoria = param.idCategoria.Value;
                }
                if (param.flAtivo != null && param.flAtivo != produto.flAtivo)
                    produto.flAtivo = param.flAtivo.Value;
                if (param.qtdEstoque != null && param.qtdEstoque.Value > 0)
                    produto.qtdEstoque = param.qtdEstoque.Value;

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
