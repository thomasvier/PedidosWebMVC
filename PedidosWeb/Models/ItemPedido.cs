using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PedidosWeb.Models.Admin;

namespace PedidosWeb.Models
{
    public class ItemPedido
    {
        public int ID { get; set; }

        public int IDProduto { get; set; }

        public decimal ValorItem { get; set; }
    }
}