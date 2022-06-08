using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api_loja.Models
{

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public bool? Enabled { get; set; }
        [JsonIgnore]
        public  ICollection<Product> Product { get; set; }

        public int? CategoryParentId { get; set; }
        public  ICollection<Category> SubCategories { get; set; }


    }
}
