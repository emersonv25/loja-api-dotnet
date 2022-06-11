using api_loja.Models;
using api_loja.Models.Object;
using api_loja.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_loja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET api/<CategoryController>
        [HttpGet]
        public ActionResult<ICollection<Category>> GetAll()
        {

            try
            {
                ICollection<Category> categories = _categoryService.GetAll();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }

        }

        // GET api/<CategoryController>/{id}
        [HttpGet("{id:int}")]
        public ActionResult<Category> GetById(int id)
        {
            try
            {
                Category category = _categoryService.GetById(id);
                if (category == null)
                {
                    return NotFound("Nenhum resultado encontrado");
                }
                return Ok( new { category });
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }

        }

        // POST api/<CategoryController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ParamCategory param)
        {
            try
            {
                Category category = await _categoryService.Post(param);
                return Ok("Categoria cadastrado com sucesso !");
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possivel cadastrar a categoria: " + ex.Message );
            }

        }

        // PUT api/<CategoryController>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ParamCategory param)
        {
            try
            {
                await _categoryService.Put(id, param);
                return Ok("Categoria editada com sucesso !");
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possivel editar a categoria: " + ex.Message);
            }
        }

        // DELETE api/<CategoryController>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.Delete(id);
                return Ok("Categoria deletada com sucesso");
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possivel deletar a categoria: " + ex.Message);
            }

        }
    }
}
