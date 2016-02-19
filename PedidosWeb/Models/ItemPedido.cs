using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PedidosWeb.Models.Admin;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PedidosWeb.Models.Admin;

namespace PedidosWeb.Models
{
    public class ItemPedido
    {
        public int ID { get; set; }

        [ForeignKey("Produto")]
        public virtual int IDProduto { get; set; }

        [ForeignKey("Cliente")]
        public virtual int IDPedido { get; set; }
                
        public decimal Quantidade { get; set; }

        [Display(Name="Preço")]
        public decimal PrecoCompra { get; set; }

        [Display(Name = "Valor total")]
        public decimal ValorTotal { get; set; }

        public virtual Produto Produto { get; set; }

        public virtual Pedido Pedido { get; set; }
    }
}