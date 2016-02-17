using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PedidosWeb.DAL;
using PedidosWeb.BLL.Admin;
using PedidosWeb.Models.Admin;
using PedidosWeb.Models;

namespace PedidosWeb.Controllers.Admin
{
    public class RepresentantesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Representantes
        public ActionResult Index(string sortOrder, string filtroAtual,
                                    string filtro, int? page,
                                    string ativoFiltro,
                                    string ativoFiltroAtual,
                                    string idFiltro,
                                    string IDFiltroAtual)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NomeSort = string.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
                ViewBag.IDSort = sortOrder == "ID" ? "id_desc" : "ID";

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

                if (idFiltro != null)
                {
                    page = 1;
                }
                else
                {
                    idFiltro = IDFiltroAtual;
                }

                ViewBag.FiltroAtual = filtro;

                RepresentanteBll representanteBll = new RepresentanteBll();

                return View("~/Views/Admin/Representantes/Index.cshtml", representanteBll.ListaRepresentantesPaginacao(page, filtro, sortOrder, ativoFiltro, idFiltro));
            }
            catch (Exception ex)
            {
                return View("~/Views/Admin/Representantes/Index.cshtml").ComMensagem(string.Format(Resources.Geral.ContateAdminsitrador, ex.Message), TipoMensagem.Erro);
            }
        }

        // GET: Representantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Representante representante = db.Representantes.Find(id);
            if (representante == null)
            {
                return HttpNotFound();
            }
            return View(representante);
        }

        // GET: Representantes/Create
        public ActionResult Create()
        {
            Representante representante = new Representante();

            try
            {
                ViewBag.IDRepresentante = string.Format("{0:000000}", RepresentanteBll.RetornarNovoID());

                return View("~/Views/Admin/Representantes/Create.cshtml", representante);
            }
            catch
            {
                return View("~/Views/Admin/Representantes/Create.cshtml", representante).ComMensagem(Resources.Geral.TenteNovamente, TipoMensagem.Erro);
            }            
        }

        // POST: Representantes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Email,Telefone,Celular,Ativo")] Representante representante)
        {
            if (ModelState.IsValid)
            {
                db.Representantes.Add(representante);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("~/Views/Admin/Representantes/Create.cshtml", representante);
        }

        // GET: Representantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Representante representante = db.Representantes.Find(id);
            if (representante == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Representantes/Edit.cshtml", representante);
        }

        // POST: Representantes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Email,Telefone,Celular,Ativo")] Representante representante)
        {
            if (ModelState.IsValid)
            {
                db.Entry(representante).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("~/Views/Admin/Representantes/Edit.cshtml", representante);
        }

        // GET: Representantes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Representante representante = db.Representantes.Find(id);
            if (representante == null)
            {
                return HttpNotFound();
            }
            return View(representante);
        }

        // POST: Representantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Representante representante = db.Representantes.Find(id);
            db.Representantes.Remove(representante);
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
