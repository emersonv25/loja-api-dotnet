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

        // GET api/<AuthController>/login 
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<ViewUserLogin> Login([FromBody]ParamLogin login)
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

                return new ViewUserLogin { Username = user.Username, FullName = user.FullName, Email = user.Email, Token = token };
            }
            catch(Exception ex)
            {
                return BadRequest("Não foi possível realizar o login: " + ex.Message);
            }

        }
        // POST api/<AuthController>/register 
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody]ParamRegister user)
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
                    return BadRequest("Não foi possivel cadastrar o usuário");
                }

                return Ok("Usuário cadastrado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível realizar o cadastro: " + ex.Message);
            }

        }
        // PUT api/<AuthController>/admin/{id} 
        [HttpPut]
        [Route("admin/update/{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> UpdateAdmin(int id, User usuarioEditado)
        {
            try
            {
                User usuario = await _authService.PutUserAdm(id, usuarioEditado);
                if (usuario == null)
                {
                    return BadRequest("Falha ao editar usuário");
                }

                return Ok("Usuário editado com sucesso !");
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possivel realizar a atualização: " + ex.Message);
            }

        }
        // PUT api/<AuthController>/update/{id}
        [HttpPut]
        [Route("update/{id:int}")]
        [Authorize]
        public async Task<ActionResult> Update(int id, User userEdited)
        {
            try
            {
                User user = await _authService.PutUser(id, userEdited);
                if (user == null)
                {
                    return BadRequest("Falha ao editar usuário");
                }

                return Ok("Usuário editado com sucesso !");

            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possivel realizar a atualização: " + ex.Message);
            }

        }
        // DELETE api/<AuthController>/admin/delete/{id} 
        [HttpDelete]
        [Route("admin/delete/{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> DeleteUserAdm(int id)
        {

            try
            {
                bool user = await _authService.DeleteUser(id);
                if (user == false)
                {
                    return BadRequest("Falha ao deletar usuário");
                }

                return Ok("Usuário deletado com sucesso !");
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
