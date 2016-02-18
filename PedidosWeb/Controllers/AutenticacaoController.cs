using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PedidosWeb.BLL.Admin;
using PedidosWeb.Models;
using PedidosWeb.Models.Admin;
using System.Web.Security;
using PedidosWeb.BLL;

namespace PedidosWeb.Controllers
{
    public class AutenticacaoController : Controller
    {
        // GET: Autenticacao
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            if (this.HttpContext.User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home").ComMensagem("Você não tem permissão para acesso.", TipoMensagem.Info);
            }
            else
            {
                return View();
            }
        }

        public ActionResult RedirecionaPedidos()
        {
            string login = HttpContext.User.Identity.Name;

            TipoUsuario tipoUsuario = AutenticacaoBll.RetornarTipoUsuario(login);

            switch (tipoUsuario)
            {
                case TipoUsuario.Administrador:
                    return RedirectToAction("Index", "Pedidos");
                    break;
                case TipoUsuario.Cliente:
                    return RedirectToAction("Index", "PedidosCli");
                    break;
                case TipoUsuario.Representante:
                    return RedirectToAction("Index", "PedidosRep");
                    break;
                default:
                    return HttpNotFound();
            }
            
        }

        [HttpPost]
        public ActionResult LogOn(string login, string senha, string returnUrl)
        {
            try
            {

                    UsuarioBll usuarioBll = new UsuarioBll();

                    Usuario usuario = usuarioBll.LogOn(login, senha);

                    if (usuario != null)
                    {
                        FormsAuthentication.SetAuthCookie(login, false);

                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return View().ComMensagem(Resources.Geral.UsuarioSenhaInvalidos, TipoMensagem.Aviso);
                    }
            }
            catch(Exception ex)
            {
                return View().ComMensagem(string.Format(Resources.Geral.ContateAdminsitrador, ex.Message), TipoMensagem.Erro);
            }
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn", "Autenticacao");
        }
    }
}