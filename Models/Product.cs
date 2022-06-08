using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_loja.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public bool? Enabled { get; set; }
        public  ICollection<ProductImage> ProductImages { get; set; }

        public  ICollection<ProductType> ProductType { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public  Category Category { get; set; }

    }
}
