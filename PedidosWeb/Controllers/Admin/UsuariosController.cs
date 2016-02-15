﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PedidosWeb.DAL;
using PedidosWeb.Models.Admin;
using PedidosWeb.Models;
using PedidosWeb.BLL.Admin;

namespace PedidosWeb.Controllers.Admin
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Usuarios
        public ActionResult Index(string sortOrder, string filtroAtual,
                                    string filtro, int? page,
                                    string tipoUsuarioFiltro,
                                    string tipoUsuarioFiltroAtual,
                                    string ativoFiltro,
                                    string ativoFiltroAtual,
                                    string codigoUsuarioFiltro,
                                    string codigoUsuarioFiltroAtual)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NomeSort = string.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";
                ViewBag.LoginSort = sortOrder == "Login" ? "login_desc" : "Login";
                ViewBag.IDSort = sortOrder == "ID" ? "id_desc" : "ID";

                if (filtro != null)
                {
                    page = 1;
                }
                else
                {
                    filtro = filtroAtual;
                }

                if (tipoUsuarioFiltro != null)
                {
                    page = 1;
                }
                else
                {
                    tipoUsuarioFiltro = tipoUsuarioFiltroAtual;
                }

                if (ativoFiltro != null)
                {
                    page = 1;
                }
                else
                {
                    ativoFiltro = ativoFiltroAtual;
                }

                if (codigoUsuarioFiltro != null)
                {
                    page = 1;
                }
                else
                {
                    codigoUsuarioFiltro = codigoUsuarioFiltroAtual;
                }

                ViewBag.FiltroAtual = filtro;

                UsuarioBll usuarioBll = new UsuarioBll();

                return View("~/Views/Admin/Usuarios/Index.cshtml", usuarioBll.ListaUsuariosPaginacao(page, filtro, tipoUsuarioFiltro, sortOrder, ativoFiltro, codigoUsuarioFiltro));
            }
            catch (Exception ex)
            {
                return View("~/Views/Admin/Usuarios/Index.cshtml").ComMensagem(string.Format(Resources.Geral.ContateAdminsitrador, ex.Message), TipoMensagem.Erro);
            }
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);            
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            Usuario usuario = new Usuario();

            return View("~/Views/Admin/Usuarios/Create.cshtml", usuario);
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Login,Senha,Email,Ativo,Role,Tipo")] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Usuarios.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View(usuario).ComMensagem(string.Format(Resources.Geral.ContateAdminsitrador, ex.Message), TipoMensagem.Erro);
            }

            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Login,Senha,Email,Ativo,Role,Tipo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
