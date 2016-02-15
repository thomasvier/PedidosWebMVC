using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PedidosWeb.Models.Admin;
using PedidosWeb.DAL;
using PagedList;
using PedidosWeb.Models;

namespace PedidosWeb.BLL.Admin
{
    public class UsuarioBll
    {
        Contexto db;

        public UsuarioBll()
        {
            db = new Contexto();
        }

        public Usuario LogOn(string login, string senha)
        {
            Usuario usuario = (from u in db.Usuarios
                               where u.Login.Equals(login) && u.Senha.Equals(senha)
                               select u).FirstOrDefault();

            
            return usuario;
        }

        public IPagedList<Usuario> ListaUsuariosPaginacao(int? page, string filtro,
                                                            string tipoUsuarioFiltro, string sortOrder,
                                                            string ativoFiltro, string codigoUsuarioFiltro)
        {
            int ativo = int.TryParse(ativoFiltro, out ativo) ? ativo : 2;
            int tipo = int.TryParse(tipoUsuarioFiltro, out tipo) ? tipo : 2;
            int ID = int.TryParse(codigoUsuarioFiltro, out ID) ? ID : 0;

            var usuarios = from c in db.Usuarios
                           select c;

            if (!String.IsNullOrEmpty(filtro))
            {
                usuarios = usuarios.Where(s => s.Nome.Contains(filtro));
            }

            if (tipo < 2)
            {
                TipoUsuario tipoUsuario = (TipoUsuario)tipo;
                usuarios = usuarios.Where(x => x.Tipo == tipoUsuario);
            }

            if (ativo < 2)
            {
                bool situacao = ativo.Equals(0) ? false : true;
                usuarios = usuarios.Where(x => x.Ativo == situacao);
            }

            if (ID > 0)
            {
                usuarios = usuarios.Where(x => x.ID.Equals(ID));
            }

            switch (sortOrder)
            {
                case "nome_desc":
                    usuarios = usuarios.OrderByDescending(s => s.Nome);
                    break;
                case "Login":
                    usuarios = usuarios.OrderBy(s => s.Login);
                    break;
                case "login_desc":
                    usuarios = usuarios.OrderByDescending(s => s.Login);
                    break;
                case "ID":
                    usuarios = usuarios.OrderBy(s => s.ID);
                    break;
                case "id_desc":
                    usuarios = usuarios.OrderByDescending(s => s.ID);
                    break;
                default:
                    usuarios = usuarios.OrderBy(s => s.Nome);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return usuarios.ToPagedList(pageNumber, pageSize);
        }

        public static bool VericarLoginExistente(Usuario usuario, TipoOperacao tipoOperacao)
        {
            Contexto db = new Contexto();

            List<Usuario> usuarios = (from c in db.Usuarios
                                      where c.Login.Equals(usuario.Login)
                                      select c).ToList();

            if (!string.IsNullOrEmpty(usuario.Login))
            {
                if (tipoOperacao.Equals(TipoOperacao.Create))
                {
                    if (usuarios.Count > 0)
                        return true;
                }
                else if (tipoOperacao.Equals(TipoOperacao.Update))
                {
                    if (usuarios.Count > 0)
                    {
                        foreach (Usuario user in usuarios)
                        {
                            if (user.Login.Equals(usuario.Login) && user.ID != usuario.ID)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}