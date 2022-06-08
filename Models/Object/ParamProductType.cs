using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Models.Object
{
    public class ParamType
    {
        [Required(ErrorMessage = "O nome do type é obrigatório", AllowEmptyStrings = false)]
        public string Title { get; set; }
        public bool? Enabled { get; set; }
        [Required(ErrorMessage = "A quantidade em estoque é obrigatorio", AllowEmptyStrings = false)]
        public int Inventory { get; set; }
        [Required(ErrorMessage = "O ID do product em que o type faz parte é obrigatorio", AllowEmptyStrings = false)]
        public int ProductId { get; set; }

    }
    public class ParamEditProductType
    {
        public string Title { get; set; }
        public bool? Enabled { get; set; }
        public int? Inventory { get; set; }

    }
    public class ParamProductType
    {
        [Required(ErrorMessage = "O nome do type é obrigatório", AllowEmptyStrings = false)]
        public string Title { get; set; }
        [Required(ErrorMessage = "A quantidade em estoque é obrigatorio", AllowEmptyStrings = false)]
        public int Inventory { get; set; }
    }
}
