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
                urls.Add(GetFileUrl(i.Path));
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
                string fileName = GenerateNewFileName(file.FileName);
                string fileFormat = GetFileFormat(fileName);

                byte[] bytesFile = ConvertFileInByteArray(file);


                string directory = CreateFilePath(fileName);

                await System.IO.File.WriteAllBytesAsync(directory, bytesFile);

                string url = GetFileUrl(fileName);
                path.Add(fileName);
            }

            return path;

        }

        #region Utils
        private string GetFileFormat(string fullFileName)
        {
            var format = fullFileName.Split(".").Last();
            return "." + format;
        }

        private string GenerateNewFileName(string fileName)
        {
            var newFileName = (Guid.NewGuid().ToString() + "_" + fileName).ToLower();
            newFileName = newFileName.Replace("-", "");

            return newFileName;
        }

        private string CreateFilePath(string fileName)
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["Directories:Images"]);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, fileName);

            return filePath;
        }

        private string GetFileUrl(string filePath)
        {
            var baseUrl = _configuration["Directories:BaseUrl"];

            var fileUrl = _configuration["Directories:Images"]
                .Replace("wwwroot", "")
                .Replace("\\", "");

            return (baseUrl + "/" + fileUrl + "/" + filePath);
        }

        private byte[] ConvertFileInByteArray(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
        #endregion
    }
}
