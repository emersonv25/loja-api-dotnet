using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_loja.Models.Object
{
    public class ViewProduto
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string DescricaoProduto { get; set; }
        public decimal ValorProduto { get; set; }
        public decimal DescontoProduto { get; set; }
        public bool? FlAtivoProduto { get; set; }
        public ICollection<string> Imagens { get; set; }
        public  ICollection<ModeloProduto> ModeloProduto { get; set; }
        public int IdCategoria { get; set; }
        public  Categoria Categoria { get; set; }
    }
}
