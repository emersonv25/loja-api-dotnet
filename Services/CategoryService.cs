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

namespace api_loja.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;
        public CategoryService(AppDbContext context)
        {
            _db = context;

        }
        public ICollection<Category> GetAll()
        {
            ICollection<Category> categories = _db.Category.Where(x => x.CategoryParentId == null).ToList();
            _db.Category.Where(x => x.CategoryParentId != null).Load(); // Carrega as subcategories
            return categories;
        }
        public Category GetById(int id)
        {
            Category category = _db.Category.Find(id);
            _db.Category.Where(x => x.CategoryParentId != null).Load(); // Carrega as subcategories

            return category;
        }

        public async Task<Category> Post(ParamCategory param)
        {

            Category category = new Category { Title = param.Title, CategoryParentId = param.CategoryParentId != 0 ? param.CategoryParentId : null };
            _db.Category.Add(category);
            await _db.SaveChangesAsync();
            return category;

        }
        public async Task<bool> Put(int id, ParamCategory param)
        {

            Category category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Categoria não encontrada");
            }
            category.Title = param.Title;
            if (param.CategoryParentId != 0)
                category.CategoryParentId = param.CategoryParentId;
            await _db.SaveChangesAsync();
            return true;

        }
        public async Task<bool> Delete(int id)
        {

            Category category = await _db.Category.FindAsync(id);
            if (category == null)
            {
                throw new Exception("Categoria não encontrada");
            }
            if(category.SubCategories.Count != 0)
            {
                throw new Exception("A Categoria contém subcategories");
            }
            _db.Category.Remove(category);
            await _db.SaveChangesAsync();
            return true;

        }
    }
}
