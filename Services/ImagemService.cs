using api_loja.Data;
using api_loja.Models;
using api_loja.Models.Object;
using api_loja.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Services
{
    public class ImagemService : IImagemService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _db;

        public ImagemService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _db = context;
        }
        public ICollection<string> GetUrlByProdutoId(int idProduto)
        {
            ICollection<ImagemProduto> imagens = _db.ImagemProduto.Where(i => i.IdProduto == idProduto).ToList();
            List<string> urls = new List<string>();
            foreach(ImagemProduto i in imagens)
            {
                urls.Add(Utils.GetFileUrl(i.Path, _configuration["Directories:BaseUrl"], _configuration["Directories:ImagesPath"]));
            }
            return urls;
        }
        public async Task<bool> Post(int idProduto, List<string> paths)
        {
            foreach(string path in paths)
            {
                ImagemProduto imagem = new ImagemProduto { IdProduto = idProduto, Path = path };
                _db.ImagemProduto.Add(imagem);
            }
            await _db.SaveChangesAsync();

            return true;
        }
        public async Task<List<string>> SaveFiles(IFormFileCollection files)
        {
            List<string> path = new List<string>();
            foreach (var file in files)
            {
                string fileName = Utils.GenerateNewFileName(file.FileName);
                string directory = Utils.CreateFilePath(fileName, _configuration["Directories:ImagesPath"]);
                #region Salva o arquivo em disco
                byte[] bytesFile = Utils.ConvertFileInByteArray(file);
                await System.IO.File.WriteAllBytesAsync(directory, bytesFile);

                /*
                using (var stream = new FileStream(directory, FileMode.Create))
                {
                    file.CopyTo(stream);

                }
                */
                #endregion

                path.Add(fileName);
            }

            return path;

        }

    }
}
