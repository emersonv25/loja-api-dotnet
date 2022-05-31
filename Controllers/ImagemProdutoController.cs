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
        public static IWebHostEnvironment _environment;
        public ImagemProdutoController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImagem(List<IFormFile> file)
        {
            var formData = Request.Form.Files;

            if (formData.Count == 0)
                return BadRequest("Arquivos vazio");
            string directoryPath = Path.Combine(_environment.ContentRootPath, "public");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            foreach (var formFile in formData)
            {

                string filePath = Path.Combine(directoryPath, formFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(stream);

                }
            }

            // Process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok("Upload sucesso");
        }
    }
}
