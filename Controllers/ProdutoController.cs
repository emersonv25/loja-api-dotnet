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
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetById(int id)
        {
            return await _db.Produto.Include(i => i.Categoria).FirstOrDefaultAsync(p => p.Id == id);
        }
        [HttpGet("Nome/{nomeCategoria}")]
        public ActionResult<IEnumerable<Produto>> GetByName(string nome)
        {
            return _db.Produto.Include(i => i.Categoria).Where(i => i.Nome.Contains(nome)).ToList();
        }

        // POST api/<ProdutoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProdutoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProdutoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
