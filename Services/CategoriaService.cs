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
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _db;
        public CategoriaService(AppDbContext context)
        {
            _db = context;

        }
        public ICollection<Categoria> GetAll()
        {
            ICollection<Categoria> categorias = _db.Categoria.Where(x => x.IdCategoriaPai == null).ToList();
            _db.Categoria.Where(x => x.IdCategoriaPai != null).Load(); // Carrega as subcategorias
            return categorias;
        }
        public Categoria GetById(int id)
        {
            Categoria categoria = _db.Categoria.Find(id);
            _db.Categoria.Where(x => x.IdCategoriaPai != null).Load(); // Carrega as subcategorias

            return categoria;
        }

        public async Task<Categoria> Post(ParamCategoria param)
        {

            Categoria categoria = new Categoria { NomeCategoria = param.NomeCategoria, IdCategoriaPai = param.IdCategoriaPai != 0 ? param.IdCategoriaPai : null };
            _db.Categoria.Add(categoria);
            await _db.SaveChangesAsync();
            return categoria;

        }
        public async Task<bool> Put(int id, ParamCategoria param)
        {

            Categoria categoria = await _db.Categoria.FindAsync(id);
            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada");
            }
            categoria.NomeCategoria = param.NomeCategoria;
            if (param.IdCategoriaPai != 0)
                categoria.IdCategoriaPai = param.IdCategoriaPai;
            await _db.SaveChangesAsync();
            return true;

        }
        public async Task<bool> Delete(int id)
        {

            Categoria categoria = await _db.Categoria.FindAsync(id);
            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada");
            }
            if(categoria.SubCategorias.Count != 0)
            {
                throw new Exception("A categoria contém subcategorias");
            }
            _db.Categoria.Remove(categoria);
            await _db.SaveChangesAsync();
            return true;

        }
    }
}
