using api_produtos.Data;
using api_produtos.Models;
using api_produtos.Models.Object;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

        // GET: api/<CategoriaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetAll()
        {
            return await _db.Produto.Include(i => i.Categoria).ToListAsync();
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<dynamic>> GetById(int id)
        {
            var produto = _db.Produto.Include(i => i.Categoria).FirstOrDefault(p => p.Id == id);
            if(produto == null)
            {
                return NotFound(new { error = "Produto não encontrado" });
            }
            return produto;
        }
        // GET api/<CategoriaController>/Placa
        [HttpGet("{nome}")]
        public ActionResult<IEnumerable<Produto>> GetByName(string nome)
        {
            return _db.Produto.Include(i => i.Categoria).Where(i => i.Nome.Contains(nome)).ToList();
        }

        // POST api/<ProdutoController>
        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] ParamProduto param)
        {
            try
            {
                Produto produto = new Produto { Nome = param.Nome, Valor = param.Valor, Descricao = param.Descricao, 
                    Quantidade = param.Quantidade, CategoriaId = param.CategoriaId, Ativo = param.Ativo };

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

                if (!String.IsNullOrEmpty(param.Nome))
                    produto.Nome = param.Nome;
                if (!String.IsNullOrEmpty(param.Descricao))
                    produto.Descricao = param.Descricao;
                if (param.Valor != null && param.Valor > 0)
                    produto.Valor = param.Valor.Value;
                if (param.CategoriaId != null && param.CategoriaId > 0)
                {
                    if (_db.Categoria.FirstOrDefault(c => c.Id == param.CategoriaId) == null)
                    {
                        return NotFound(new { error = "Categória não encontrado" });
                    }
                    produto.CategoriaId = param.CategoriaId.Value;
                }
                if (param.Ativo != null && param.Ativo != produto.Ativo)
                    produto.Ativo = param.Ativo.Value;
                if (param.Quantidade != null && param.Quantidade.Value > 0)
                    produto.Quantidade = param.Quantidade.Value;

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
                    return NotFound(new { error = "Produto não encontrada" });
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
