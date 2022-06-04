using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_loja.Models
{
    public class ImagemProduto
    {
        [Key]
        public int IdImagemProduto { get; set; }
        public string Path { get; set; }

        public int IdProduto { get; set; }
        [ForeignKey("IdProduto")]
        [JsonIgnore]
        public  Produto Produto { get; set; }

    }
}
