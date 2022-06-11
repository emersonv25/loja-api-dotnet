using api_loja.Controllers;
using api_loja.Data;
using api_loja.Models;
using api_loja.Models.Object;
using api_loja.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _db = context;

        }
        public Result GetAll()
        {
            ICollection<Product> products = _db.Product
                .Include(c => c.Category)
                .Include(m => m.ProductType)
                .Include(i => i.ProductImages).ToList();

            #region ViewProduct
            List<ViewProduct> returnProduct = new List<ViewProduct>();
            foreach (Product p in products)
            {
                ViewProduct v = new ViewProduct
                {
                    ProductId = p.ProductId,
                    Title = p.Title,
                    Description = p.Description,
                    Value = p.Value,
                    Discount = p.Discount,
                    ValueWithDiscount = p.Value - p.Discount,
                    Enabled = p.Enabled,
                    ProductType = p.ProductType,
                    CategoryId = p.CategoryId,
                    Category = p.Category,
                    Images = GetUrlProductImage(p.ProductImages)
                };
                returnProduct.Add(v);
            }
            #endregion

            #region Retorno
            Result result = new Result();
            result.TotalResults = returnProduct.Count();
            result.CurrentPage = 1;
            result.ItemsPerPage = returnProduct.Count();
            result.Results = returnProduct.ToList<dynamic>();
            #endregion

            return result;
        }
        public ViewProduct GetById(int id)
        {
            Product p = _db.Product
                .Include(c => c.Category)
                .Include(m => m.ProductType)
                .Include(i => i.ProductImages)
                .SingleOrDefault(i => i.ProductId == id);

            if(p == null)
            {
                return null;
            }

            #region ViewProduct
            ViewProduct viewProduct = new ViewProduct
            {
                ProductId = p.ProductId,
                Title = p.Title,
                Description = p.Description,
                Value = p.Value,
                Discount = p.Discount,
                ValueWithDiscount = p.Value - p.Discount,
                Enabled = p.Enabled,
                ProductType = p.ProductType,
                CategoryId = p.CategoryId,
                Category = p.Category,
                Images = GetUrlProductImage(p.ProductImages)
            };
            #endregion

            return viewProduct;
        }
        public Result GetByName(string name)
        {
            ICollection<Product> products = _db.Product
                .Include(c => c.Category)
                .Include(m => m.ProductType)
                .Include(i => i.ProductImages)
                .Where(i => i.Title.Contains(name) || i.Category.Title.Contains(name) || i.Description.Contains(name)).ToList();

            #region ViewProduct
            List<ViewProduct> returnProduct = new List<ViewProduct>();
            foreach (Product p in products)
            {
                ViewProduct v = new ViewProduct
                {
                    ProductId = p.ProductId,
                    Title = p.Title,
                    Description = p.Description,
                    Value = p.Value,
                    Discount = p.Discount,
                    ValueWithDiscount = p.Value - p.Discount,
                    Enabled = p.Enabled,
                    ProductType = p.ProductType,
                    CategoryId = p.CategoryId,
                    Category = p.Category,
                    Images = GetUrlProductImage(p.ProductImages)
                };
                returnProduct.Add(v);
            }
            #endregion

            #region Retorno
            Result result = new Result();
            result.TotalResults = returnProduct.Count();
            result.CurrentPage = 1;
            result.ItemsPerPage = returnProduct.Count();
            result.Results = returnProduct.ToList<dynamic>();
            #endregion

            return result;
        }

        public ICollection<ViewProduct> GetByListId(int[] ids)
        {
            ICollection<Product> products = _db.Product.Include(c => c.Category)
                .Include(m => m.ProductType)
                .Include(i => i.ProductImages)
                .Where(i => ids.Contains(i.ProductId)).ToList();

            #region ViewProduct
            List<ViewProduct> retornoProduct = new List<ViewProduct>();
            foreach (Product p in products)
            {
                ViewProduct v = new ViewProduct
                {
                    ProductId = p.ProductId,
                    Title = p.Title,
                    Description = p.Description,
                    Value = p.Value,
                    Discount = p.Discount,
                    ValueWithDiscount = p.Value - p.Discount,
                    Enabled = p.Enabled,
                    ProductType = p.ProductType,
                    CategoryId = p.CategoryId,
                    Category = p.Category,
                    Images = GetUrlProductImage(p.ProductImages)
                };
                retornoProduct.Add(v);
            }
            #endregion

            return retornoProduct;

        }

        public async Task<bool> Post(ObjectProduct param, IFormFileCollection files)
        {

            List<ProductType> types = new List<ProductType>();
            foreach (ParamProductType m in param.ProductType)
            {
                ProductType type = new ProductType
                {
                    Title = m.Title,
                    Inventory = m.Inventory
                };
                types.Add(type);
            }
            List<string> paths = Utils.SaveFiles(files, _configuration["Directories:ImagesPath"]); // Salva as fotos e obtem o path
            List<ProductImage> images = new List<ProductImage>();
            foreach(string path in paths)
            {
                ProductImage image = new ProductImage
                {
                    Path = path
                };
                images.Add(image);
            }

            Product product = new Product
            {
                Title = param.Title,
                Value = param.Value,
                Discount = param.Discount,
                Description = param.Description,
                CategoryId = param.CategoryId,
                Enabled = param.Enabled,
                ProductType = types,
                ProductImages = images
            };


            _db.Product.Add(product);
            await _db.SaveChangesAsync();
            return true;

        }
        public async Task<bool> Put(int id, ParamProductEdit param)
        {

            Product product = await _db.Product.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Produto não encontrado");
            }

            if (!string.IsNullOrEmpty(param.Title))
                product.Title = param.Title;
            if (!string.IsNullOrEmpty(param.Description))
                product.Description = param.Description;
            if (param.Value != null && param.Value > 0)
                product.Value = param.Value.Value;
            if (param.Discount != null && param.Discount > 0)
                product.Discount = param.Discount.Value;
            if (param.CategoryId != null && param.CategoryId > 0)
            {
                if (_db.Category.Find(param.CategoryId) == null)
                {
                    throw new Exception("Categoria não encontrada");
                }
                product.CategoryId = param.CategoryId.Value;
            }
            if (param.Enabled != null && param.Enabled != product.Enabled)
                product.Enabled = param.Enabled.Value;

            await _db.SaveChangesAsync();
            return true;

        }
        public async Task<bool> Delete(int id)
        {

            Product product = await _db.Product.FindAsync(id);
            if (product == null)
            {
                throw new Exception("Produto não encontrado");
            }
            _db.Product.Remove(product);
            await _db.SaveChangesAsync();
            return true;

        }
        #region Image
        private List<string> GetUrlProductImage(ICollection<ProductImage> productImage)
        {
            List<string> urls = new List<string>();
            foreach (ProductImage i in productImage)
            {
                urls.Add(Utils.GetFileUrl(i.Path, _configuration["Directories:BaseUrl"], _configuration["Directories:ImagesPath"]));
            }
            return urls;
        }
        #endregion
    }
}
