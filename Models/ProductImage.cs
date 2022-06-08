using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_loja.Models
{
    public class ProductImage
    {
        [Key]
        public int ProductImageId { get; set; }
        public string Path { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [JsonIgnore]
        public  Product Product { get; set; }

    }
}
