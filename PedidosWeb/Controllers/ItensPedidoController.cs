using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PedidosWeb.Controllers
{
    public class ItensPedidoController : Controller
    {
        // GET: ItensPedido
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastrarItem()
        {
            return View("~/Views/ItensPedido/CadastrarItem.cshtml");
        }
    }
}