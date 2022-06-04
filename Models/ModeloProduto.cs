using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_loja.Models
{
    public class ModeloProduto
    {
        [Key]
        public int IdModeloProduto{ get; set; }
        public string NomeModelo { get; set; }
        public bool? FlAtivoModelo { get; set; }
        public int Estoque { get; set; }
        public int IdProduto { get; set; }
        [ForeignKey("IdProduto")]
        [JsonIgnore]
        public  Produto Produto { get; set; }

    }
}
