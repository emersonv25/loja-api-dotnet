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
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }


        [HttpGet]
        public ActionResult<ICollection<Categoria>> GetAll()
        {

            try
            {
                ICollection<Categoria> categorias = _categoriaService.GetAll();
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }

        }


        [HttpGet("{id:int}")]
        public ActionResult<Categoria> GetById(int id)
        {
            try
            {
                Categoria categoria = _categoriaService.GetById(id);
                if (categoria == null)
                {
                    return NotFound("Nenhum resultado encontrado");
                }
                return Ok( new { categoria });
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }

        }

        [HttpGet("{nome}")]
        public ActionResult<ICollection<Categoria>> GetByName(string nome)
        {
            try
            {
                ICollection<Categoria> categoria = _categoriaService.GetByName(nome);
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ParamCategoria param)
        {
            try
            {
                Categoria categoria = await _categoriaService.Post(param);
                return Ok(new { message = "Categoria cadastrado com sucesso !" });
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possivel cadastrar a categoria: " + ex.Message );
            }

        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ParamCategoria param)
        {
            try
            {
                await _categoriaService.Put(id, param);
                return Ok("Categoria editada com sucesso !");
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possivel editar a categoria: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _categoriaService.Delete(id);
                return Ok("Categoria deletada com sucesso");
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possivel deletar a categoria: " + ex.Message);
            }

        }
    }
}
