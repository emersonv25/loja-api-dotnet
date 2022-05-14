using System.Collections.Generic;
using System.Threading.Tasks;
using api_produtos.Models;
using Microsoft.AspNetCore.Mvc;
using api_produtos.Models.Object;


namespace api_produtos.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Usuario> Login(string username, string password); 
        Task<Usuario> Cadastrar(ParamCadastro usuario); 
        Task<Usuario> GetUsuario(string username); 
        Task<Usuario> GetUsuarioByEmail(string email); 
        Task<Usuario> GetUsuarioById(int id); 
        Task<Usuario> PutUsuario(int id, Usuario usuarioEditado);
        Task <bool> DeleteUsuario(int id);
        Task<Usuario> PutUsuarioAdm(int id, Usuario usuarioEditado);
    }
}