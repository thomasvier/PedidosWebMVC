using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PedidosWeb.Models.Admin;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedidosWeb.Models
{
    public class Pedido
    {
        public Pedido()
        {
            DataPedido = DateTime.Now;
            DataEntrega = DateTime.Now.AddDays(1);
        }

        public int ID { get; set; }
                         
        [ForeignKey("Cliente")]
        public int ClienteID { get; set; }

        [Display(Name="Código interno")]
        public string CodigoInterno { get; set; }

        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:dd/MM/yyyy}")]
        [Display(Name="Data do pedido")]
        [Required]
        public DateTime DataPedido { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de entrega")]
        public DateTime DataEntrega { get; set; }

        [Display(Name="Valor total")]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString="{0:N2}")]
        public decimal? ValorTotal { get; set; }

        [Display(Name="Situação do pedido")]
        public SituacaoPedido SituacaoPedido { get; set; }

        public virtual Cliente Cliente { get; set; }
    }
}