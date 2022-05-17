using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_produtos.Data;
using api_produtos.Models;
using Microsoft.AspNetCore.Authorization;
using api_produtos.Services;
using api_produtos.Services.Interfaces;
using api_produtos.Models.Object;

namespace api_produtos.Controllers
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
        public async Task<ActionResult<dynamic>> Autenticar([FromBody]ParamLogin user)
        {
            Usuario usuario = await _authService.Login(user.Username, user.Password);
            if (usuario == null){
                return BadRequest(new {error = "Usuário ou senha inválidos"});
            }

            if(usuario.flAtivo == true){
                return BadRequest(new {error = "Usuário Inativo !"});
            }

            var token = TokenService.GenerateToken(usuario);


            usuario.Password = "";
            return new { usuario = usuario, token = token};

        }
        
        [HttpPost]
        [Route("cadastrar")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Cadastrar([FromBody]ParamCadastro user)
        {
            if(user.Username != null && user.Password != null && user.Nome != null){
                if(user.Password.Length < 4){
                    return BadRequest(new {error = "A Senha precisa conter mais de 4 caracteres"});
                }
                if(user.Username.Length < 4){
                    return BadRequest(new {error = "O nome de usuário precisa conter mais de 4 caracteres"});
                }
                if(user.Nome == ""){
                    return BadRequest(new {error = "O Nome não pode ser nulo"});
                }
                if(await _authService.GetUsuario(user.Username) != null){
                    return BadRequest(new {error = "Nome de usuário já cadastrado"});
                }
                if(await _authService.GetUsuarioByEmail(user.Email) != null){
                    return BadRequest(new {error = "E-mail já cadastrado"});
                }
            }
            else{
                return BadRequest(new {error = "Dados para o cadastro inválidos !"});
            }

            Usuario usuario = await _authService.Cadastrar(user);
            if (usuario == null){
                return BadRequest(new {error = "Não foi possivel cadastrar o usuário"});
            }

            return (new {message = "Usuário cadastrado com sucesso !"});

        }
        [HttpPut]
        [Route("admin/editar")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<dynamic>> EditarUsuarioAdm(int id, Usuario usuarioEditado)
        {

            Usuario usuario = new Usuario();
            usuario = await _authService.PutUsuarioAdm(id, usuarioEditado);
            if(usuario == null){
                return BadRequest(new {error = "Falha ao editar usuário"});
            }

            return (new {message = "Usuário editado com sucesso !"});
        }
        [HttpPut]
        [Route("editar")]
        [Authorize]
        public async Task<ActionResult<dynamic>> EditarUsuario(int id, Usuario usuarioEditado)
        {

            Usuario usuario = new Usuario();
            usuario = await _authService.PutUsuario(id, usuarioEditado);
            if(usuario == null){
                return BadRequest(new {error = "Falha ao editar usuário"});
            }

            return (new {message = "Usuário editado com sucesso !"});
        }
        [HttpDelete]
        [Route("admin/deletar")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<dynamic>> DeleteUsuarioAdm(int id)
        {

            bool usuario = await _authService.DeleteUsuario(id);
            if(usuario == false){
                return BadRequest(new {error = "Falha ao deletar usuário"});
            }

            return (new {message = "Usuário deletado com sucesso !"});
        }


        [HttpGet]
        [Route("autenticado")]
        [Authorize]
        public string Autenticado() => String.Format("Autenticado: {0}", User.Identity.Name);

        [HttpGet]
        [Route("anonimo")]
        [AllowAnonymous]
        public string Anonimo() => "Anônimo";

        [HttpGet]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        public string Admin() => "Administrador";

    }


}
