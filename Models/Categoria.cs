using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api_loja.Models
{

    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string NomeCategoria { get; set; }
        public bool? FlAtivoCategoria { get; set; }
        [JsonIgnore]
        public  ICollection<Produto> Produto { get; set; }

        public int? IdCategoriaPai { get; set; }
        public  ICollection<Categoria> SubCategorias { get; set; }


    }
}
