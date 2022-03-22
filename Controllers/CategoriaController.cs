using api_produtos.Data;
using api_produtos.Models;
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


        // GET: api/<CategoriaController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
        {
            return await _db.Categoria.ToListAsync();
        }

        // GET api/<CategoriaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetById(int id)
        {
            return await _db.Categoria.FindAsync(id);
        }
        [HttpGet("Nome/{nomeCategoria}")]
        public ActionResult<IEnumerable<Categoria>> GetByName(string nomeCategoria)
        {
            return _db.Categoria.Where(i => i.Nome.Contains(nomeCategoria)).ToList();
        }
        // POST api/<CategoriaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoriaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoriaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
