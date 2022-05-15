using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_produtos.Models.Object
{
    public class ParamCategoria
    {
        [Required(ErrorMessage = "O nome da categoria é obrigatório", AllowEmptyStrings = false)]
        public string Nome { get; set; }
        public int? idPai { get; set; }
    }
}
