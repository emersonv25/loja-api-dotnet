using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api_produtos.Models
{

    public class Categoria
    {
        [Key]
        public int idCategoria { get; set; }
        public string Nome { get; set; }
        [JsonIgnore]
        public virtual ICollection<Produto> Produtos { get; set; }
        public int? idPai { get; set; }
        public virtual ICollection<Categoria> SubCategorias { get; set; }


    }
}
