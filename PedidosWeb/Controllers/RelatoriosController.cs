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

        /*
         * Retorna um PDF diretamente no browser com as configurações padrões
         * ViewName é setado somente para utilizar o próprio Modelo anterior
         * Caso não queira setar o ViewName, você deve gerar a view com o mesmo nome da action
         */
        public ActionResult PDFPadrao()
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
 
        /*
         * Configura algumas propriedades do PDF, inclusive o nome do arquivo gerado,
         * Porem agora ele baixa o pdf ao invés de mostrar no browser
         */
        public ActionResult PDFConfigurado()
        {
            var pdf = new ViewAsPdf
            {
                ViewName = "Produtos",
                FileName = "NomeDoArquivoPDF.pdf",
                PageSize = Size.A4,
                IsGrayScale = true,
                PageMargins = new Margins{Bottom = 5, Left = 5, Right = 5, Top = 5},
            };
            return pdf;
        }
 
        /*
         * Pode passar um modelo para a view que vai ser utilizada para gerar o PDF
         */
        public ActionResult PDFComModel()
        {
            Contexto db = new Contexto();

            var modelo = (from p in db.Produtos
                          where p.ID.Equals(1)
                          select p).FirstOrDefault();
 
            var pdf = new ViewAsPdf
            {
                ViewName = "Produtos",
                Model = modelo
            };
 
            return pdf;
        }
    }
}