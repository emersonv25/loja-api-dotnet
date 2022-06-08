using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace api_loja.Models
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId{ get; set; }
        public string Title { get; set; }
        public bool? Enabled { get; set; }
        public int Inventory { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [JsonIgnore]
        public  Product Product { get; set; }

    }
}
