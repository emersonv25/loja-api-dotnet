using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api_produtos.Models
{

    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [JsonIgnore]
        public List<Produto> Produtos { get; set; }


    }
}
