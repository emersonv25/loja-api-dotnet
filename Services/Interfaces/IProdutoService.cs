using System.Collections.Generic;
using System.Threading.Tasks;
using api_loja.Models;
using Microsoft.AspNetCore.Mvc;
using api_loja.Models.Object;


namespace api_loja.Services.Interfaces
{
    public interface IProdutoService
    {
        ICollection<ViewProduto> GetAll();
        ViewProduto GetById(int id);
        ICollection<ViewProduto> GetByName(string nome);
        Task<bool> Post(ParamProduto param);
        Task<bool> Put(int id, ParamEditarProduto param);
        Task<bool> Delete(int id);
    }
}