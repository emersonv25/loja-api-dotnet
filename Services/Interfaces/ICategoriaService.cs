using System.Collections.Generic;
using System.Threading.Tasks;
using api_loja.Models;
using Microsoft.AspNetCore.Mvc;
using api_loja.Models.Object;


namespace api_loja.Services.Interfaces
{
    public interface ICategoriaService
    {
        ICollection<Categoria> GetAll();
        Categoria GetById(int id);
        ICollection<Categoria> GetByName(string nome);
        Task<Categoria> Post(ParamCategoria param);
        Task<bool> Put(int id, ParamCategoria param);
        Task<bool> Delete(int id);
    }
}