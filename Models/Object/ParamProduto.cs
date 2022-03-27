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
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório", AllowEmptyStrings = false)]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "A quantidade disponível é obrigatório", AllowEmptyStrings = false)]
        public int Quantidade { get; set; }

        public bool Ativo { get; set; }

        public int CategoriaId { get; set; }
    }
    public class ParamEditarProduto
    {
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public decimal? Valor { get; set; }

        public int? Quantidade { get; set; }

        public bool? Ativo { get; set; }

        public int? CategoriaId { get; set; }
    }
}
