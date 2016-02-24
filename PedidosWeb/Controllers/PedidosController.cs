using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PedidosWeb.DAL;
using PedidosWeb.Models;
using PedidosWeb.Models.Admin;
using PedidosWeb.BLL.Admin;
using PedidosWeb.BLL;
using System.Web.Routing;

namespace PedidosWeb.Controllers
{
    [Authorize(Roles="Admin")]
    public class PedidosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Pedidos
        public ActionResult Index()
        {
            try
            {
                List<Cliente> clientes = ClienteBll.ListarClientes();
                ClienteBll clienteBll = new ClienteBll();
                
                ViewBag.Clientes = clientes;

                List<Pedido> pedidos = new List<Pedido>();
                pedidos = db.Pedidos.ToList();
                ViewBag.NomeCliente = clienteBll.ListarClientesAtivos();
            }

            catch (Exception ex) { }
            return View(db.Pedidos.ToList());
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Pedido(int? id)
        {            
            ProdutoBll produtoBll = new ProdutoBll();
            List<Cliente> clientes = ClienteBll.ListarClientes();

            ViewBag.Produtos = produtoBll.ListarProdutosAtivos();
            ViewBag.Clientes = clientes;


            Pedido pedido = new Pedido();

            if (id != null)
            {
                pedido = db.Pedidos.Where(x => x.ID == id).FirstOrDefault();

                return View(pedido);
            }
            else
            {
                return View(pedido);
            }
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Pedido([Bind(Include = "ID,CodigoInterno,DataPedido, DataEntrega,ValorTotal,SituacaoPedido,ClienteID")] Pedido pedido, string ClienteID)
        {
            if (ModelState.IsValid)
            {
                pedido.ClienteID = int.Parse(ClienteID);

                if (pedido.ID > 0)
                {
                    db.Entry(pedido).State = EntityState.Modified;
                }
                else
                {
                    db.Pedidos.Add(pedido);
                }

                db.SaveChanges();
                return RedirectToAction("Pedido", new { id = pedido.ID });
            }

            ProdutoBll produtoBll = new ProdutoBll();
            List<Cliente> clientes = ClienteBll.ListarClientes();

            ViewBag.Produtos = produtoBll.ListarProdutosAtivos();
            ViewBag.Clientes = clientes;

            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CodigoInterno,DataPedido,DataEntrega,ValorTotal,SituacaoPedido,ClienteID")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
            catch(Exception ex)
            {
                return Json(new { preco = "0,00", total = "0,00" });
            }
        }

        public JsonResult CalcularTotal(string valores)
        {
            try
            {
                return Json(UteisBll.CalcularTotal(valores), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("0,00");
            }
        }

        public JsonResult RetornarClientes(string term)
        {
            Contexto db = new Contexto();

            var clientes = (from c in db.Clientes
                            where c.RazaoSocial.ToLower().Contains(term)
                            select c).ToList();

            return Json(clientes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClienteSelecionado(int id)
        {
            Contexto db = new Contexto();

            Cliente cli = (from c in db.Clientes
                           where c.ID.Equals(id)
                           select c).FirstOrDefault();

            return Json(cli, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalvarPedido([Bind(Include = "ID,CodigoInterno,DataPedido, DataEntrega,ValorTotal,SituacaoPedido,ClienteID")] Pedido pedido, string idCliente)
        {
            using(Contexto db = new Contexto())
            {
                pedido.ClienteID = Convert.ToInt32(idCliente);

                db.Pedidos.Add(pedido);
                db.SaveChanges();
            }
            
            return Pedido(pedido.ID);
        }
    }
}
