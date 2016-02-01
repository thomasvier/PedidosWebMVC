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
using PedidosWeb.BLL.Admin;
using PagedList;
using PedidosWeb.Models;

namespace PedidosWeb.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ClientesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Clientes
        public ActionResult Index(string sortOrder, string filtroAtual, 
                                    string filtro, int? page, 
                                    string tiposTitularFiltro, 
                                    string tipoTitularAtual,
                                    string ativoFiltro,
                                    string ativoFiltroAtual,
                                    string codigoInternoFiltro,
                                    string codigoInternoFiltroAtual)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.RazaoSocialSort = string.IsNullOrEmpty(sortOrder) ? "razaosocial_desc" : "";
            ViewBag.NomeFantasiaSort = sortOrder == "NomeFantasia" ? "nomefantasia_desc" : "NomeFantasia";
            ViewBag.CodigoInternoSort = sortOrder == "CodigoInterno" ? "codigointerno_desc" : "CodigoInterno";

            if (filtro != null)
            {
                page = 1;
            }
            else
            {
                filtro = filtroAtual;
            }
            
            if(tiposTitularFiltro != null)
            {
                page = 1;
            }
            else
            {
                tiposTitularFiltro = tipoTitularAtual;
            }

            if(ativoFiltro != null)
            {
                page = 1;
            }
            else
            {
                ativoFiltro = ativoFiltroAtual;
            }
            
            if(codigoInternoFiltro != null)
            {
                page = 1;
            }
            else
            {
                codigoInternoFiltro = codigoInternoFiltroAtual;
            }            

            ViewBag.FiltroAtual = filtro;
            ViewBag.TipoTitularAtual = tiposTitularFiltro;

            ClienteBll clientebll = new ClienteBll();

            return View("~/Views/Admin/Clientes/Index.cshtml", clientebll.ListaClientesPaginacao(page, filtro, tiposTitularFiltro, sortOrder, ativoFiltro, codigoInternoFiltro));
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Clientes/Details.cshtml", cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            Cliente Cliente = new Cliente();

            return View("~/Views/Admin/Clientes/Create.cshtml", Cliente);
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CodigoInterno,RazaoSocial,NomeFantasia,CPFCNPJ,InscricaoEstadual,Telefone,Celular,Email,Endereco,Cidade,Bairro,Estado,Numero,Cep,Complemento,Ativo,Tipo")] Cliente cliente)
        {
            try
            {
                if (ClienteBll.VericarCodigoExistente(cliente, TipoOperacao.Create))
                {
                    return View("~/Views/Admin/Clientes/Create.cshtml", cliente).ComMensagem(string.Format(Resources.Validations.ClienteExistente, cliente.CodigoInterno), TipoMensagem.Aviso);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        db.Clientes.Add(cliente);
                        db.SaveChanges();
                        return RedirectToAction("Index").ComMensagem(Resources.Geral.ItemSalvoSucesso, TipoMensagem.Sucesso);
                    }
                }
            }
            catch(Exception ex)
            {
                return View("~/Views/Admin/Clientes/Create.cshtml", cliente).ComMensagem(string.Format(Resources.Geral.ContateAdminsitrador, ex.Message), TipoMensagem.Erro);
            }

            return View("~/Views/Admin/Clientes/Create.cshtml", cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View("~/Views/Admin/Clientes/Edit.cshtml", cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CodigoInterno,RazaoSocial,NomeFantasia,CPFCNPJ,InscricaoEstadual,Telefone,Celular,Email,Endereco,Cidade,Bairro,Estado,Numero,Cep,Complemento,Ativo,Tipo")] Cliente cliente)
        {
            try
            {
                if (ClienteBll.VericarCodigoExistente(cliente, TipoOperacao.Update))
                {
                    return View("~/Views/Admin/Clientes/Edit.cshtml", cliente).ComMensagem(string.Format(Resources.Validations.ClienteExistente, cliente.CodigoInterno), TipoMensagem.Aviso);
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(cliente).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index").ComMensagem(Resources.Geral.ItemAlteradoSucesso, TipoMensagem.Sucesso);
                    }
                }
            }
            catch(Exception ex)
            {
                return View("~/Views/Admin/Clientes/Edit.cshtml", cliente).ComMensagem(string.Format(Resources.Geral.ContateAdminsitrador, ex.Message), TipoMensagem.Erro);
            }
            
            return View("~/Views/Admin/Clientes/Edit.cshtml", cliente);
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
