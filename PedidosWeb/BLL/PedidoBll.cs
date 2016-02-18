using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PedidosWeb.DAL;
using PedidosWeb.Models;

namespace PedidosWeb.BLL
{
    public class PedidoBll
    {
        Contexto db;

        public PedidoBll()
        {
            db = new Contexto();
        }

        public static Pedido RetornarPedido(int? id)
        {
            Contexto db = new Contexto();

            Pedido pedido = (from p in db.Pedidos
                             where p.ID == id
                             select p).FirstOrDefault();

            return pedido;
        }
    }
}