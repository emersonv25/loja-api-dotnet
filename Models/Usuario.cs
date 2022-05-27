using System;
using System.ComponentModel.DataAnnotations;

namespace api_loja.Models
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
        public Usuario(string Username, string Password, string NomeCompleto, string Email)
        {
            this.Username = Username;
            this.Password = Password;
            this.NomeCompleto = NomeCompleto;
            this.Email = Email;
            Admin = false;
            FlAtivoUsuario = true;
        }

        [Key]
        public int IdUsuario { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string NomeCompleto { get; set; }
        public bool? FlAtivoUsuario { get; set; }
        public bool? Admin { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;


    }

}