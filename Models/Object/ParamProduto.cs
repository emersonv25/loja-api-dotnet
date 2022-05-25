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
        public string NomeProduto { get; set; }

        public string DescricaoProduto { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório", AllowEmptyStrings = false)]
        public decimal ValorProduto { get; set; }
        public decimal DescontoProduto { get; set; }

        public bool FlAtivoProduto { get; set; }

        public int IdCategoria { get; set; }
        public List<ModeloProduto> ModeloProduto { get; set; }
    }
    public class ParamEditarProduto
    {
        public string NomeProduto { get; set; }

        public string DescricaoProduto { get; set; }

        public decimal? ValorProduto { get; set; }
        public decimal? DescontoProduto { get; set; }

        public bool? FlAtivoProduto { get; set; }

        public int? IdCategoria { get; set; }
        public List<ModeloProduto> ModeloProduto { get; set; }

    }

}
