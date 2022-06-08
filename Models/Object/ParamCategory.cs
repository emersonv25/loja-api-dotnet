using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Models.Object
{
    public class ParamCategory
    {
        [Required(ErrorMessage = "O nome da category é obrigatório", AllowEmptyStrings = false)]
        public string Title { get; set; }
        public int? CategoryParentId { get; set; }
    }
}
