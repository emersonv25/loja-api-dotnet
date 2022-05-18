using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_produtos.Models
{
    public class ModeloProduto
    {
        [Key]
        public int IdModeloProduto{ get; set; }
        public string NomeModelo { get; set; }
        public bool? FlAtivoModelo { get; set; }
        public int IdProduto { get; set; }
        [ForeignKey("IdProduto")]
        [JsonIgnore]
        public virtual Produto Produto { get; set; }

    }
}
