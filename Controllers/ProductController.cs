using api_loja.Data;
using api_loja.Models;
using api_loja.Models.Object;
using api_loja.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<ICollection<Product>> GetAll()
        {
            try
            {
                Result result = _productService.GetAll();

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id:int}")]
        public ActionResult<Product> GetById(int id)
        {
            try
            {
                ViewProduct product = _productService.GetById(id);
                if (product == null)
                    return NotFound("Produto não encontrado");
                return Ok(product);
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }
        }
        // GET api/<CategoryController>/Placa
        [HttpGet("{name}")]
        public ActionResult<ICollection<Product>> GetByName(string name)
        {
            try
            {
                return Ok(_productService.GetByName(name));
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] FormProduct param)
        {
            try
            {
                if (param.Files.Count == 0 && Request.Form.Files.Count > 0)
                    param.Files = Request.Form.Files;
                else if (Request.Form.Files.Count == 0)
                    BadRequest("É necessário enviar ao menos 1 image para o product.");

                ObjectProduct product = JsonConvert.DeserializeObject<ObjectProduct>(param.Json.ToString());

                // Validação conteudo do json
                CheckParamObjectProduct(product);

                await _productService.Post(product, param.Files);
                return Ok("Produto cadastrado com sucesso !");
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possivel cadastrar o produto: " + ex.Message);
            }

        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ParamProductEdit param)
        {
            try
            {
                await _productService.Put(id, param);
                return Ok("Produto editado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest( "Não foi possivel cadastrar o product: " + ex.Message);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _productService.Delete(id);
                return Ok("Deletado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possivel deletar o product: " + ex.Message);
            }

        }
        private bool CheckParamObjectProduct(ObjectProduct product)
        {
            if (string.IsNullOrEmpty(product.Title))
                throw new Exception("O título do produto é obrigatório");
            if(product.Value == 0.0m)
                throw new Exception("O Valor do produto é obrigatório");
            if (product.CategoryId == 0)
                throw new Exception("O Id da categoria é obrigatório");
            if (product.ProductType == null)
                throw new Exception("É obrigatório ao menos 1 tipo de produto");

            foreach(ParamProductType t in product.ProductType)
            {
                if(string.IsNullOrEmpty(t.Title))
                    throw new Exception("O título do tipo do produto é obrigatório");
                if(t.Inventory == 0)
                    throw new Exception("A quantidade em estoque é obrigatorio");
            }

            return true;
        }
    }
}
