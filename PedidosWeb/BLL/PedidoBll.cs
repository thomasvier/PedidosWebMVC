using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PedidosWeb.DAL;
using PedidosWeb.Models;
using System.Data.Entity;
using PagedList;

namespace PedidosWeb.BLL
{
    public class PedidoBll
    {
        Contexto db;

        public IPagedList<Pedido> ListaPedidosPaginacao(int? page, string clienteFiltro,
                                                            string situacaoPedidoFiltro, 
                                                            string sortOrder, 
                                                            string codigoInternoFiltro)
        {
            int situacao = int.TryParse(situacaoPedidoFiltro, out situacao) ? situacao : 99;
            
            var pedidos = from p in db.Pedidos
                           select p;

            if (!string.IsNullOrEmpty(clienteFiltro))
            {
                pedidos = pedidos.Where(p => p.Cliente.RazaoSocial.Contains(clienteFiltro) || p.Cliente.NomeFantasia.Contains(clienteFiltro) || p.Cliente.CPFCNPJ == clienteFiltro);
            }

            if (situacao < 99)
            {
                SituacaoPedido situacaoPedido = (SituacaoPedido)situacao;
                pedidos = pedidos.Where(x => x.SituacaoPedido == situacaoPedido);
            }
            
            if (!string.IsNullOrEmpty(codigoInternoFiltro))
            {
                pedidos = pedidos.Where(x => x.CodigoInterno.Equals(codigoInternoFiltro));
            }

            switch (sortOrder)
            {
                case "razaosocial_desc":
                    pedidos = pedidos.OrderByDescending(s => s.Cliente.RazaoSocial);
                    break;
                case "DataPedido":
                    pedidos = pedidos.OrderBy(s => s.DataPedido);
                    break;
                case "datapedido_desc":
                    pedidos = pedidos.OrderByDescending(s => s.DataPedido);
                    break;
                default:
                    pedidos = pedidos.OrderBy(s => s.Cliente.RazaoSocial);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return pedidos.ToPagedList(pageNumber, pageSize);
        }

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