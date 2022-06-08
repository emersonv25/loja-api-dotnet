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
    public class ProductTypeController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductTypeController(AppDbContext context)
        {
            _db = context;
        }

        // GET api/<ProductTypeController>/5
        [HttpGet("{id:int}")]
        public ActionResult<ProductType> GetById(int id)
        {
            ProductType type = _db.ProductType.Find(id);
            if (type == null)
                return NotFound("Modelo não encontrado");
            return Ok(new { type });
        }

        // POST api/<ProductTypeController>
        [HttpPost]
        public async Task<ActionResult<ProductType>> Post([FromBody] ParamType param)
        {
            try
            {
                ProductType type = new ProductType { Title = param.Title, Inventory = param.Inventory, ProductId = param.ProductId };

                _db.ProductType.Add(type);
                await _db.SaveChangesAsync();
                return Ok(type);
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possivel cadastrar o Modelo: " + ex.Message);
            }

        }

        // PUT api/<ProductTypeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ParamEditProductType param)
        {
            try
            {
                ProductType type = await _db.ProductType.FindAsync(id);
                if (type == null)
                {
                    return NotFound(new { error = "Modelo não encontrado" });
                }

                if (!string.IsNullOrEmpty(param.Title))
                    type.Title = param.Title;
                if(param.Inventory != null)
                {
                    type.Inventory = param.Inventory.Value;
                }
                if(param.Enabled.HasValue && param.Enabled.Value != type.Enabled)
                {
                    type.Enabled = param.Enabled.Value;
                }

                await _db.SaveChangesAsync();
                return Ok(type);
            }
            catch (Exception ex)
            {
                return BadRequest( "Não foi possivel cadastrar o produto: " + ex.Message);
            }
        }

        // DELETE api/<ProductTypeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                ProductType type = await _db.ProductType.FindAsync(id);
                if (type == null)
                {
                    return NotFound(new { error = "Modelo não encontrado" });
                }
                _db.ProductType.Remove(type);
                await _db.SaveChangesAsync();
                return Ok("Deletado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possivel deletar o tipo: " + ex.Message);
            }

        }
    }
}
