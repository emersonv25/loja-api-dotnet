using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Models.Object
{
    public class ViewProduct
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }
        public decimal ValueWithDiscount { get; set; }
        public bool? Enabled { get; set; }
        public List<string> Images { get; set; }
        public  ICollection<ProductType> ProductType { get; set; }
        public int CategoryId { get; set; }
        public  Category Category { get; set; }
    }
}
