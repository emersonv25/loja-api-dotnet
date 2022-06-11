using api_loja.Models.Object;
using api_loja.Services;
using api_loja.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ProductImageController(IImageService image)
        {
            _imageService = image;
        }
        [HttpGet("{productId:int}")]
        public ActionResult<ICollection<ViewImage>> GetByProductId(int productId)
        {
            try
            {
                ICollection<ViewImage> viewImage = _imageService.GetUrlByProductId(productId);
                if (viewImage == null)
                {
                    return NotFound("Nenhum resultado encontrado");
                }
                return Ok(viewImage);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }

        }
        [HttpPost("{productId:int}")]
        public async Task<ActionResult> Post(int productId, IFormFileCollection files)
        {
            try
            {
                if (files.Count == 0 && Request.Form.Files.Count > 0)
                    files = Request.Form.Files;
                else if (Request.Form.Files.Count == 0)
                    return BadRequest("É necessário enviar um arquivo de imagem.");

                if (!files.Any(f => f.ContentType.Contains("image")))
                {
                    return BadRequest("Formato não suportado, insira um arquivo de imagem");
                }
                await _imageService.Post(productId, files); // Salva os paths no banco de dados

                return Ok("Upload realizado com sucesso");
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possível realizar o upload da imagem: " + ex.Message);
            }

        }
        [HttpDelete("{imageId:int}")]
        public async Task<ActionResult> Delete(int imageId)
        {
            try
            {
                await _imageService.Delete(imageId);

                return Ok("Deletado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível deletar a imagem: " + ex.Message);
            }

        }
    }
}
