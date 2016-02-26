using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PedidosWeb.Models.Admin
{
    public class Produto
    {
        public Produto()
        {
            Ativo = true;
        }

        [DisplayFormat(DataFormatString="{0:000000}", ApplyFormatInEditMode=true)]
        public int ID { get; set; }

        [Display(Name="Código Interno")]
        [Required]
        public string CodigoInterno { get; set; }

        [Required(ErrorMessage="O campo descrição é obrigatório")]
        [Display(Name="Descrição")]
        public string Descricao { get; set; }
                
        [Display(Name="Preço Unitário")]        
        public decimal? PrecoUnitario { get; set; }
                
        [Display(Name = "Preço Quantidade")]        
        public decimal? PrecoQuantidade { get; set; }

        [DisplayFormat(DataFormatString = "{0:n3}", ApplyFormatInEditMode = true)]
        [Display(Name = "Quantidade preço")]        
        public decimal? QuantidadePreco { get; set; }

        public bool Ativo { get; set; }
    }
}