using PedidosWeb.BLL.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PedidosWeb.Controllers
{
    public class UteisController : Controller
    {
        // GET: Uteis
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult RetornarPreco(string id, string quant)
        {
            try
            {
                int idProduto = int.TryParse(id, out idProduto) ? idProduto : 0;
                decimal quantidade = decimal.TryParse(quant, out quantidade) ? quantidade : 0;
                decimal? precoProduto = ProdutoBll.RetornarPreco(idProduto, quantidade);
                decimal totalProduto = quantidade * (precoProduto != null ? (decimal)precoProduto : 0);

                string preco = string.Format("{0:N2}", precoProduto);
                string total = string.Format("{0:N2}", totalProduto);

                var result = new { preco = preco, total = total };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { preco = "0,00", total = "0,00" });
            }
        }
    }
}