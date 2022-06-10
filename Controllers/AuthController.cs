using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_loja.Data;
using api_loja.Models;
using Microsoft.AspNetCore.Authorization;
using api_loja.Services;
using api_loja.Services.Interfaces;
using api_loja.Models.Object;

namespace api_loja.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Login([FromBody]ParamLogin login)
        {
            try
            {
                User user = _authService.Login(login.Username, login.Password);
                if (user == null)
                {
                    return BadRequest("Usuário ou senha inválidos !");
                }

                if (user.Enabled == false)
                {
                    return BadRequest("Usuário Inativo !");
                }

                var token = TokenService.GenerateToken(user);

                return new { UserName = user.Username, FullName = user.FullName, Email = user.Email, Token = token };
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possível realizar o login: " + ex.Message);
            }

        }
        
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Post([FromBody]ParamRegister user)
        {
            try
            {
                if (user.Username != null && user.Password != null && user.FullName != null)
                {
                    if (user.Password.Length < 4)
                    {
                        return BadRequest("A Senha precisa conter mais de 4 caracteres");
                    }
                    if (user.Username.Length < 4)
                    {
                        return BadRequest("O nome de usuário precisa conter mais de 4 caracteres");
                    }
                    if (user.FullName == "")
                    {
                        return BadRequest("O Nome não pode ser nulo");
                    }
                    if (_authService.GetUser(user.Username) != null)
                    {
                        return BadRequest("Nome de usuário já cadastrado");
                    }
                    if (_authService.GetUserByEmail(user.Email) != null)
                    {
                        return BadRequest("E-mail já cadastrado");
                    }
                }
                else
                {
                    return BadRequest("Dados para o cadastro inválidos !");
                }

                User newUser = await _authService.Register(user);
                if (newUser == null)
                {
                    return BadRequest(new { error = "Não foi possivel cadastrar o usuário" });
                }

                return ("Usuário cadastrado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar o cadastro: " + ex.Message);
            }

        }
        [HttpPut]
        [Route("admin/update")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<dynamic>> UpdateAdmin(int id, User usuarioEditado)
        {
            try
            {
                User usuario = await _authService.PutUserAdm(id, usuarioEditado);
                if (usuario == null)
                {
                    return BadRequest("Falha ao editar usuário");
                }

                return ("Usuário editado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possivel realizar a atualização: " + ex.Message);
            }

        }
        [HttpPut]
        [Route("update")]
        [Authorize]
        public async Task<ActionResult<dynamic>> Update(int id, User userEdited)
        {
            try
            {
                User user = await _authService.PutUser(id, userEdited);
                if (user == null)
                {
                    return BadRequest("Falha ao editar usuário");
                }

                return ("Usuário editado com sucesso !");

            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possivel realizar a atualização: " + ex.Message);
            }

        }
        [HttpDelete]
        [Route("admin/delete")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<dynamic>> DeleteUserAdm(int id)
        {

            try
            {
                bool user = await _authService.DeleteUser(id);
                if (user == false)
                {
                    return BadRequest("Falha ao deletar usuário");
                }

                return ("Usuário deletado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível excluir o usuário: " + ex.Message);
            }

        }


        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Autenticado() {
            return String.Format("Autenticado: {0}", User.Identity.Name);
         }

        [HttpGet]
        [Route("anonymous")]
        public string Anonimo() => "Anônimo";

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public string Admin() => "Administrador";

    }


}
