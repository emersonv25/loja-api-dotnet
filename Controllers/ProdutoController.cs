using api_loja.Data;
using api_loja.Models;
using api_loja.Models.Object;
using api_loja.Services.Interfaces;
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
        private readonly IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        // GET: api/<ProdutoController>
        [HttpGet]
        public ActionResult<Retorno> GetAll()
        {
            try
            {
                Retorno retorno = _produtoService.GetAll();

                return Ok(retorno);
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }
        }

        // GET api/<ProdutoController>/5
        [HttpGet("{id:int}")]
        public ActionResult<ViewProduto> GetById(int id)
        {
            try
            {
                ViewProduto produto = _produtoService.GetById(id);
                if (produto == null)
                    return NotFound("Produto não encontrado");
                return Ok(produto);
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }
        }
        // GET api/<CategoriaController>/Placa
        [HttpGet("{nome}")]
        public ActionResult<Retorno> GetByName(string nome)
        {
            try
            {
                return Ok(_produtoService.GetByName(nome));
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }
        }

        // POST api/<ProdutoController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ParamProduto param)
        {
            try
            {
                await _produtoService.Post(param);
                return Ok("Produto cadastrado com sucesso !");
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possivel cadastrar o produto: " + ex.Message);
            }

        }

        // PUT api/<ProdutoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ParamEditarProduto param)
        {
            try
            {
                await _produtoService.Put(id, param);
                return Ok("Produto editado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest( "Não foi possivel cadastrar o produto: " + ex.Message);
            }
        }

        // DELETE api/<ProdutoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _produtoService.Delete(id);
                return Ok("Deletado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possivel deletar o produto: " + ex.Message);
            }

        }
    }
}
