using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Models.Object
{
    public class ObjectProduct
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public decimal Discount { get; set; }

        public bool Enabled { get; set; }

        public int CategoryId { get; set; }
        public List<ParamProductType> ProductType { get; set; }
    }
    public class FormProduct
    {
        public string Json { get; set; }
        public IFormFileCollection Files { get; set; }
    }
    public class ParamProductEdit
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Value { get; set; }
        public decimal? Discount { get; set; }

        public bool? Enabled { get; set; }

        public int? CategoryId { get; set; }

    }

}
