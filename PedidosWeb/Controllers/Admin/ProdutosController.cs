using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PedidosWeb.DAL;
using PedidosWeb.Models.Admin;
using PagedList;
using PedidosWeb.BLL.Admin;

namespace PedidosWeb.Controllers.Admin
{
    public class ProdutosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Produtos
        public ActionResult Index(string sortOrder, string filtroAtual,
                                    string filtro, int? page,
                                    string ativoFiltro,
                                    string ativoFiltroAtual,
                                    string codigoInternoFiltro,
                                    string codigoInternoFiltroAtual)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DescricaoSort = string.IsNullOrEmpty(sortOrder) ? "descricao_desc" : "";
            ViewBag.CodigoInternoSort = sortOrder == "CodigoInterno" ? "codigointerno_desc" : "CodigoInterno";

            if (filtro != null)
            {
                page = 1;
            }
            else
            {
                filtro = filtroAtual;
            }

            if (ativoFiltro != null)
            {
                page = 1;
            }
            else
            {
                ativoFiltro = ativoFiltroAtual;
            }

            if (codigoInternoFiltro != null)
            {
                page = 1;
            }
            else
            {
                codigoInternoFiltro = codigoInternoFiltroAtual;
            }

            ViewBag.FiltroAtual = filtro;

            ProdutoBll produtobll = new ProdutoBll();

            return View("~/Views/Admin/Produtos/Index.cshtml", produtobll.ListarProdutosPaginacao(page, filtro, sortOrder, ativoFiltro, codigoInternoFiltro));            
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Produtos/Details.cshtml", produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            Produto produto = new Produto();

            return View("~/Views/Admin/Produtos/Create.cshtml", produto);
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CodigoInterno,Descricao,PrecoUnitario,PrecoQuantidade,QuantidadePreco,Ativo")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Produtos.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(produto);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Produtos/Edit.cshtml", produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CodigoInterno,Descricao,PrecoUnitario,PrecoQuantidade,QuantidadePreco,Ativo")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produto produto = db.Produtos.Find(id);
            db.Produtos.Remove(produto);
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
    }
}
