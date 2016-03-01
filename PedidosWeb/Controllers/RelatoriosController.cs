using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Rotativa.Options;
using PedidosWeb.Models.Admin;
using PedidosWeb.DAL;
using PedidosWeb.BLL.Admin;
using System.Net;
using PedidosWeb.BLL;
using PedidosWeb.Models;

namespace PedidosWeb.Controllers
{
    public class RelatoriosController : Controller
    {
        // GET: Relatorios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pedido(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pedido pedido = PedidoBll.RetornarPedido(id);
            List<ItemPedido> itens = PedidoBll.RetornarItens(id);
            ViewBag.Itens = itens;
            ViewBag.Vendedor = RepresentanteBll.RetornarRepresentante(pedido.Cliente.IDRepresentante).Nome;
            ViewBag.TotalItens = string.Format("{0:N2}", itens.Sum(x => x.TotalItem));

            if (pedido == null)
            {
                return HttpNotFound();
            }

            return new Rotativa.ViewAsPdf("~/Views/Relatorios/Pedido.cshtml", pedido);
        }

        public ActionResult Clientes()
        {
            List<Cliente> clientes = ClienteBll.ListarClientes();

            var pdf = new ViewAsPdf
            {
                ViewName = "~/Views/Relatorios/Admin/Clientes.cshtml",
                PageSize = Size.A4,
                IsGrayScale = true,
                PageMargins = new Margins { Bottom = 5, Left = 5, Right = 5, Top = 5 },
                Model = clientes
            };

            return pdf;            
        }

        public ActionResult Produtos()
        {
            ProdutoBll produtoBll = new ProdutoBll();

            List<Produto> produtos = produtoBll.ListarProdutosAtivos();

            var pdf = new ViewAsPdf
            {
                ViewName = "Produtos",
                Model = produtos
            };
            return pdf;
        }

        public ActionResult Produto()
        {
            ProdutoBll produtoBll = new ProdutoBll();

            List<Produto> produtos = produtoBll.ListarProdutosAtivos();

            return new Rotativa.ViewAsPdf("~/Views/Relatorios/Produto.cshtml", produtos);
        }

        public ActionResult PedidosFiltro()
        {
            ViewBag.Clientes = ClienteBll.ListarClientes();

            return View();
        }
        
        public ActionResult Pedidos(int? idCliente, string dataInicial, string dataFinal)
        {
            DateTime inicio = DateTime.TryParse(dataInicial, out inicio) ? inicio : DateTime.MinValue;
            DateTime fim = DateTime.TryParse(dataFinal, out fim) ? fim : DateTime.MaxValue;

            PedidoBll pedidoBll = new PedidoBll();

            List<Pedido> pedidos = pedidoBll.RelatorioPedidos(idCliente, inicio, fim);

            return new Rotativa.ViewAsPdf("Pedidos", pedidos);
        }
    }
}