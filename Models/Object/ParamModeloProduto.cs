using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_produtos.Models.Object
{
    public class ParamModeloProduto
    {
        [Required(ErrorMessage = "O nome do modelo é obrigatório", AllowEmptyStrings = false)]
        public string NomeModelo { get; set; }
        public bool? FlAtivoModelo { get; set; }
        [Required(ErrorMessage = "A quantidade em estoque é obrigatorio", AllowEmptyStrings = false)]
        public int Estoque { get; set; }
        [Required(ErrorMessage = "O ID do produto em que o modelo faz parte é obrigatorio", AllowEmptyStrings = false)]
        public int IdProduto { get; set; }

    }
    public class ParamEditarModeloProduto
    {
        public string NomeModelo { get; set; }
        public bool? FlAtivoModelo { get; set; }
        public int? Estoque { get; set; }

    }
}
