using System.Collections.Generic;
using System.Threading.Tasks;
using api_loja.Models;
using Microsoft.AspNetCore.Mvc;
using api_loja.Models.Object;
using Microsoft.AspNetCore.Http;

namespace api_loja.Services.Interfaces
{
    public interface IProductService
    {
        Result GetAll();
        ViewProduct GetById(int id);
        Result GetByName(string name);
        Task<bool> Post(ObjectProduct param, IFormFileCollection images);
        Task<bool> Put(int id, ParamProductEdit param);
        Task<bool> Delete(int id);
    }
}