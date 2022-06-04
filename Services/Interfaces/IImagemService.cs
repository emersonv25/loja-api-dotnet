using System.Collections.Generic;
using System.Threading.Tasks;
using api_loja.Models;
using Microsoft.AspNetCore.Http;

namespace api_loja.Services.Interfaces
{
    public interface IImagemService
    {
       Task<List<string>> SaveFiles(IFormFileCollection files);
        Task<bool> Post(int idProduto, List<string> paths);
        ICollection<string> GetUrlByProdutoId(int idProduto);
    }
}