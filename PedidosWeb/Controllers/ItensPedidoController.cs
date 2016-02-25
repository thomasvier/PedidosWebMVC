using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedidosWeb.BLL;
using PedidosWeb.Models;
using PedidosWeb.DAL;
using PedidosWeb.BLL.Admin;
using PedidosWeb.Models.Admin;

namespace PedidosWeb.Controllers
{
    public class ItensPedidoController : Controller
    {
        // GET: ItensPedido
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ItemPedido(int? ID)
        {
            ProdutoBll produtoBll = new ProdutoBll();
            ViewBag.Produtos = produtoBll.ListarProdutosAtivos();

            if(ID != null)
            {
                ItemPedido itemPedido = PedidoBll.RetornaItemPedido(ID);

                return PartialView("~/Views/ItensPedido/ItemPedido.cshtml", itemPedido);
            }
            else
            {
                return PartialView("~/Views/ItensPedido/ItemPedido.cshtml");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ItemPedido([Bind(Include = "ID,IDPedido,Quantidade,PrecoCompra,ValorTotal,IDProduto")] ItemPedido itemPedido)
        {
            PedidoBll pedidoBll = new PedidoBll();

            if (itemPedido.ID > 0)
            {
                pedidoBll.Atualizar(itemPedido);
            }
            else
            {
                pedidoBll.Criar(itemPedido);
            }

            return RedirectToAction("Pedido", "Pedidos", new { ID = itemPedido.IDPedido });
        }        

        public ActionResult ListaItens(int? ID)
        {
            List<ItemPedido> itens = PedidoBll.RetornarItens(ID);

            return PartialView(itens);
        }
    }
}