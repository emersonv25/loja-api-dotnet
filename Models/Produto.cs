using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_produtos.Models
{
    public class Produto
    {
        [Key]
        public int idProduto { get; set; }
        public string nmProduto { get; set; }
        public string dsProduto { get; set; }
        public decimal vlProduto { get; set; }
        public decimal vlPromocional { get; set; }
        public int qtdEstoque { get; set; }
        public bool? flAtivo { get; set; }
        public int idCategoria { get; set; }
        [ForeignKey("idCategoria")]
        public virtual Categoria Categoria { get; set; }

    }
}
