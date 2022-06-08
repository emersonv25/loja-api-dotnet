using System.Collections.Generic;
using System.Threading.Tasks;
using api_loja.Models;
using Microsoft.AspNetCore.Mvc;
using api_loja.Models.Object;


namespace api_loja.Services.Interfaces
{
    public interface ICategoryService
    {
        ICollection<Category> GetAll();
        Category GetById(int id);
        Task<Category> Post(ParamCategory param);
        Task<bool> Put(int id, ParamCategory param);
        Task<bool> Delete(int id);
    }
}