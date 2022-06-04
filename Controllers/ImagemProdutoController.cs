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
    public class ImagemProdutoController : ControllerBase
    {
        private readonly IImagemService _imagemService;

        public ImagemProdutoController(IImagemService imagem)
        {
            _imagemService = imagem;
        }
        [HttpGet("{idProduto:int}")]
        public ActionResult<ICollection<string>> GetByProdutoId(int idProduto)
        {
            try
            {
                ICollection<string> paths = _imagemService.GetUrlByProdutoId(idProduto);
                if (paths == null)
                {
                    return NotFound("Nenhum resultado encontrado");
                }
                return Ok(paths);
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar a consulta: " + ex.Message);
            }

        }
        [HttpPost("{idProduto:int}")]
        public async Task<ActionResult> Post(int idProduto, IFormFileCollection files)
        {
            try
            {
                if (files.Count == 0 && Request.Form.Files.Count > 0)
                    files = Request.Form.Files;
                else if (Request.Form.Files.Count == 0)
                    BadRequest("É necessário selecionar um arquivo de imagem.");

                List<string> paths = await _imagemService.SaveFiles(files); // Salva as fotos e obtem o path
                await _imagemService.Post(idProduto, paths); // Salva os paths no banco de dados

                return Ok(paths);
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possível realizar o upload da imagem: " + ex.Message);
            }

        }
    }
}
