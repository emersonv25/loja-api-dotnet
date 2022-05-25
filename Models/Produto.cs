﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_loja.Models
{
    public class Produto
    {
        [Key]
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public decimal DescontoProduto { get; set; }
        public int EstoqueProduto { get; set; }
        public bool? FlAtivoProduto { get; set; }
        public virtual ICollection<ModeloProduto> ModeloProduto { get; set; }
        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public virtual Categoria Categoria { get; set; }

    }
}
