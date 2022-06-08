using System.Collections.Generic;
using System.Threading.Tasks;
using api_loja.Models;
using Microsoft.AspNetCore.Mvc;
using api_loja.Models.Object;


namespace api_loja.Services.Interfaces
{
    public interface IAuthService
    {
        User Login(string username, string password); 
        Task<User> Register(ParamRegister usuario); 
        User GetUser(string username); 
        User GetUserByEmail(string email); 
        Task<User> GetUserById(int id); 
        Task<User> PutUser(int id, User usuarioEditado);
        Task <bool> DeleteUser(int id);
        Task<User> PutUserAdm(int id, User usuarioEditado);
    }
}