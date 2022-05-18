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
        public Usuario(string Username, string Password, string NomeCompleto, string Email, string Cargo = "usuario", bool FlAtivoUsuario = true)
        {
            this.Username = Username;
            this.Password = Password;
            this.NomeCompleto = NomeCompleto;
            this.Email = Email;
            this.Cargo = Cargo;
            this.FlAtivoUsuario = FlAtivoUsuario;
        }

        [Key]
        public int IdUsuario { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }

        public string NomeCompleto { get; set; }


        public bool? FlAtivoUsuario { get; set; }

        public string Cargo { get; set; }

        public string Email { get; set; }


    }

}