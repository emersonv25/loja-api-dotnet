using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_produtos.Models.Object
{
    public class ParamProduto
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório", AllowEmptyStrings = false)]
        public string nmProduto { get; set; }

        public string dsProduto { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório", AllowEmptyStrings = false)]
        public decimal vlProduto { get; set; }
        public decimal vlPromocional { get; set; }

        [Required(ErrorMessage = "A quantidade disponível é obrigatório", AllowEmptyStrings = false)]
        public int qtdEstoque { get; set; }

        public bool flAtivo { get; set; }

        public int idCategoria { get; set; }
    }
    public class ParamEditarProduto
    {
        public string nmProduto { get; set; }

        public string dsProduto { get; set; }

        public decimal? vlProduto { get; set; }
        public decimal? vlPromocional { get; set; }

        public int? qtdEstoque { get; set; }

        public bool? flAtivo { get; set; }

        public int? idCategoria { get; set; }
    }
}
