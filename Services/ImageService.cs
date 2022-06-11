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
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _db;

        public ImageService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _db = context;
        }
        public ICollection<string> GetUrlByProductId(int productId)
        {
            ICollection<ProductImage> images = _db.ProductImage.Where(i => i.ProductId == productId).ToList();
            List<string> urls = new List<string>();
            foreach(ProductImage i in images)
            {
                urls.Add(Utils.GetFileUrl(i.Path, _configuration["Directories:BaseUrl"], _configuration["Directories:ImagesPath"]));
            }
            return urls;
        }
        public async Task<bool> Post(int productId, IFormFileCollection files)
        {
            Product product = _db.Product.Find(productId);
            if(product == null)
            {
                throw new Exception("Produto não encontrado");
            }
            List<string> paths = Utils.SaveFiles(files, _configuration["Directories:ImagesPath"]); // Salva as fotos e obtem o path
            foreach (string path in paths)
            {
                ProductImage image = new ProductImage { ProductId = product.ProductId, Path = path };
                _db.ProductImage.Add(image);
            }
            await _db.SaveChangesAsync();

            return true;
        }
        public async Task<bool> Delete(int imageId)
        {
            ProductImage productImage = await _db.ProductImage.FindAsync(imageId);
            if (productImage == null)
            {
                throw new Exception("Imagem não encontrado");
            }
            Utils.DeleteFile(Path.Combine(_configuration["Directories:ImagesPath"], productImage.Path));
            _db.ProductImage.Remove(productImage);
            await _db.SaveChangesAsync();

            return true;
        }

    }
}
