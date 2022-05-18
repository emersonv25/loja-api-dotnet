using System;
using System.Linq;
using System.Threading.Tasks;
using api_produtos.Data;
using api_produtos.Models;
using api_produtos.Services.Interfaces;
using api_produtos.Models.Object;

namespace api_produtos.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        public AuthService(AppDbContext context)
        {
            _context = context;

        }
        public async Task<Usuario> Login(string username, string password)
        {
            Usuario usuario = new Usuario();
            var senha = Utils.sha256_hash(password);
            try
            {
                usuario = _context.Usuarios.FirstOrDefault(u => u.Username == username && u.Password == senha);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                usuario = new Usuario();
            }
            return usuario;
        }
        public async Task<Usuario> Cadastrar(ParamCadastro usuario)
        {
            Usuario user = new Usuario();
            try
            {
                var password = Utils.sha256_hash(usuario.Password);
                user = new Usuario(usuario.Username, password, usuario.Nome, usuario.Email);
                _context.Usuarios.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                user = new Usuario();
            }
            return user;
        }
        public async Task<Usuario> GetUsuario(string username)
        {
            Usuario usuario = new Usuario();
            usuario = _context.Usuarios.FirstOrDefault(u => u.Username == username);

            return usuario;
        }
        public async Task<Usuario> GetUsuarioByEmail(string email)
        {
            Usuario usuario = new Usuario();
            usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email);

            return usuario;
        }
        public async Task<Usuario> GetUsuarioById(int id)
        {
            Usuario usuario = new Usuario();
            usuario = await _context.Usuarios.FindAsync(id);

            return usuario;
        }

        public async Task<Usuario> PutUsuarioAdm(int id, Usuario usuarioEditado)
        {
            Usuario usuario = new Usuario();

            try
            {
                usuario = await _context.Usuarios.FindAsync(id);
                if (usuarioEditado.Password != "" && usuarioEditado.Password != null)
                {
                    var password = Utils.sha256_hash(usuarioEditado.Password);
                    usuario.Password = password;
                }
                usuario.NomeCompleto = usuarioEditado.NomeCompleto;
                usuario.Username = usuarioEditado.Username;
                usuario.FlAtivoUsuario = usuarioEditado.FlAtivoUsuario;
                usuario.Cargo = usuarioEditado.Cargo;
                usuario.Email = usuarioEditado.Email;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                usuario = new Usuario();
            }

            return usuario;
        }
        public async Task<Usuario> PutUsuario(int id, Usuario usuarioEditado)
        {
            Usuario usuario = new Usuario();

            try
            {
                usuario = await _context.Usuarios.FindAsync(id);
                if (usuarioEditado.Password != "" && usuarioEditado.Password != null)
                {
                    var password = Utils.sha256_hash(usuarioEditado.Password);
                    usuario.Password = password;
                }
                usuario.NomeCompleto = usuarioEditado.NomeCompleto;
                usuario.Username = usuarioEditado.Username;
                usuario.Email = usuarioEditado.Email;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                usuario = new Usuario();
            }

            return usuario;
        }
        public async Task<bool> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return false;
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}