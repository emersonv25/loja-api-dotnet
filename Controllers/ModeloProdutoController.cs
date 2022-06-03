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
    public class ModeloProdutoController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ModeloProdutoController(AppDbContext context)
        {
            _db = context;
        }

        // GET api/<ModeloProdutoController>/5
        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<ModeloProduto>> GetById(int id)
        {
            return _db.ModeloProduto.Where(i => i.IdModeloProduto == id).ToList();
        }

        // POST api/<ModeloProdutoController>
        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] ParamModelo param)
        {
            try
            {
                ModeloProduto modelo = new ModeloProduto { NomeModelo = param.NomeModelo, Estoque = param.Estoque, IdProduto = param.IdProduto };

                _db.ModeloProduto.Add(modelo);
                await _db.SaveChangesAsync();
                return Ok(modelo);
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possivel cadastrar o Modelo: " + ex.Message);
            }

        }

        // PUT api/<ModeloProdutoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<dynamic>> Put(int id, [FromBody] ParamEditarModeloProduto param)
        {
            try
            {
                ModeloProduto modelo = await _db.ModeloProduto.FindAsync(id);
                if (modelo == null)
                {
                    return NotFound(new { error = "Modelo não encontrado" });
                }

                if (!string.IsNullOrEmpty(param.NomeModelo))
                    modelo.NomeModelo = param.NomeModelo;
                if(param.Estoque != null)
                {
                    modelo.Estoque = param.Estoque.Value;
                }
                if(param.FlAtivoModelo.HasValue && param.FlAtivoModelo.Value != modelo.FlAtivoModelo)
                {
                    modelo.FlAtivoModelo = param.FlAtivoModelo.Value;
                }

                await _db.SaveChangesAsync();
                return Ok(modelo);
            }
            catch (Exception ex)
            {
                return BadRequest( "Não foi possivel cadastrar o produto: " + ex.Message);
            }
        }

        // DELETE api/<ModeloProdutoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<dynamic>> Delete(int id)
        {
            try
            {
                ModeloProduto modelo = await _db.ModeloProduto.FindAsync(id);
                if (modelo == null)
                {
                    return NotFound(new { error = "Modelo não encontrado" });
                }
                _db.ModeloProduto.Remove(modelo);
                await _db.SaveChangesAsync();
                return Ok("Deletado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possivel deletar o modelo: " + ex.Message);
            }

        }
    }
}
