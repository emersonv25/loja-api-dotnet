using api_produtos.Data;
using api_produtos.Models;
using api_produtos.Models.Object;
using Microsoft.AspNetCore.Http;
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
    public class CategoriaController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CategoriaController(AppDbContext context)
        {
            _db = context;
        }


        /// <summary>
        /// Lista todos as categorias registrada.
        /// </summary>
        /// <returns>Uma lista com as categorias</returns>
        /// <response code="200">Retorna os itens da categoria cadastrados</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
        {
            return await _db.Categoria.ToListAsync();
        }

        /// <summary>
        /// Retorna uma categoria a partir de seu Id
        /// </summary>
        /// <param name="id">Id da categoria</param>
        /// <remarks> 
        /// Exemplo de requisição
        /// GET /api/Categoria/1
        /// {
        ///     "id": 1,
        ///     "nome": "Hardware"
        /// }
        /// </remarks>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<dynamic>> GetById(int id)
        {
            var categoria = _db.Categoria.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                return NotFound(new { errors = "Categoria não encontrada" });
            }
            return categoria;
        }
        [HttpGet("{nome}")]
        public ActionResult<IEnumerable<Categoria>> GetByName(string nome)
        {
            return _db.Categoria.Where(i => i.Nome.Contains(nome)).ToList();
        }
        // POST api/<CategoriaController>
        [HttpPost]
        public async Task<ActionResult<dynamic>> Post([FromBody] ParamCategoria param)
        {
            try
            {
                Categoria categoria = new Categoria { Nome = param.Nome };
                _db.Categoria.Add(categoria);
                await _db.SaveChangesAsync();
                return Ok(categoria);
            }
            catch(Exception ex)
            {
                return BadRequest(new { errors = "Não foi possivel cadastrar a categoria: " + ex.Message });
            }

        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<dynamic>> Put(int id, [FromBody] ParamCategoria param)
        {
            try
            {
                Categoria categoria = await _db.Categoria.FindAsync(id);
                if (categoria == null)
                {
                    return BadRequest(new { errors = "Categoria não encontrada" });
                }
                categoria.Nome = param.Nome;
                await _db.SaveChangesAsync();
                return Ok(categoria);
            }
            catch(Exception ex)
            {
                return BadRequest(new { errors = "Não foi possivel editar a categoria: " + ex.Message });
            }
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<dynamic>> Delete(int id)
        {
            try
            {
                Categoria categoria = await _db.Categoria.FindAsync(id);
                if(categoria == null)
                {
                    return NotFound(new { errors = "Categoria não encontrada" });
                }
                _db.Categoria.Remove(categoria);
                await _db.SaveChangesAsync();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new { errors = "Não foi possivel deletar a categoria: " + ex.Message });
            }

        }
    }
}
