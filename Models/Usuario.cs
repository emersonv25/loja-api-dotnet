using System.ComponentModel.DataAnnotations;

namespace api_produtos.Models
{
    public class Usuario
    {
        public Usuario(){
            
        }
        public Usuario(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }
        public Usuario(string Username, string Password, string Nome, string Email, string Cargo = "usuario", bool flAtivo = true)
        {
            this.Username = Username;
            this.Password = Password;
            this.Nome = Nome;
            this.Email = Email;
            this.Cargo = Cargo;
            this.flAtivo = flAtivo;
        }

        [Key]
        public int idUsuario { get; set; }

        public string Username { get; set; }

        public string Nome { get; set; }

        public string Password { get; set; }

        public bool? flAtivo { get; set; }

        public string Cargo { get; set; }

        public string Email { get; set; }


    }

}