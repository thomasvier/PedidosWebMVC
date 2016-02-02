using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PedidosWeb.Models.Admin;
using PedidosWeb.DAL;

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
    }
}