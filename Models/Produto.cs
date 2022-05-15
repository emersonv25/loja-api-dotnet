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
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }
        public bool Ativo { get; set; }
        public int idCategoria { get; set; }
        [ForeignKey("idCategoria")]
        public virtual Categoria Categoria { get; set; }

    }
}
