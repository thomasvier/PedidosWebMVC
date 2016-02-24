using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PedidosWeb.DAL;
using PedidosWeb.Models;
using System.Data.Entity;

namespace PedidosWeb.BLL
{
    public class PedidoBll
    {
        Contexto db;

        public PedidoBll()
        {
            db = new Contexto();
        }

        public Pedido Criar(Pedido pedido)
        {
            db.Pedidos.Add(pedido);
            db.SaveChanges();

            return pedido;
        }

        public Pedido Atualizar(Pedido pedido)
        {
            db.Entry(pedido).State = EntityState.Modified;
            db.SaveChanges();

            return pedido;
        }

        public Pedido Salvar(Pedido pedido, TipoOperacao tipoOperacao)
        {
            if(tipoOperacao.Equals(TipoOperacao.Create))
            {
                return this.Criar(pedido);
            }
            else if(tipoOperacao.Equals(TipoOperacao.Update))
            {
                return this.Atualizar(pedido);
            }

            return pedido;
        }

        public static Pedido RetornarPedido(int? id)
        {
            Contexto db = new Contexto();

            Pedido pedido = db.Pedidos.Find(id);

            return pedido;
        }

        public static List<ItemPedido> RetornarItens(int? idPedido)
        {
            Contexto db = new Contexto();

            List<ItemPedido> itens = db.ItemPedidos.Where(x => x.IDPedido == idPedido).OrderBy(x => x.Produto.Descricao).ToList();

            return itens;
        }

        public ItemPedido Criar(ItemPedido itemPedido)
        {
            db.ItemPedidos.Add(itemPedido);
            db.SaveChanges();

            return itemPedido;
        }

        public ItemPedido Atualizar(ItemPedido itemPedido)
        {
            db.Entry(itemPedido).State = EntityState.Modified;
            db.SaveChanges();

            return itemPedido;
        }

        public static ItemPedido RetornaItemPedido(int? ID)
        {
            Contexto db = new Contexto();

            ItemPedido itemPedido = db.ItemPedidos.Find(ID);

            return itemPedido;
        }
    }
}