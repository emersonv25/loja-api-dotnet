using System.Collections.Generic;
using System.Threading.Tasks;
using api_loja.Models;
using api_loja.Models.Object;
using Microsoft.AspNetCore.Http;

namespace api_loja.Services.Interfaces
{
    public interface IImageService
    {
        Task<bool> Post(int productId, IFormFileCollection files);
        ICollection<ViewImage> GetUrlByProductId(int productId);
        Task<bool> Delete(int imageId);
    }
}