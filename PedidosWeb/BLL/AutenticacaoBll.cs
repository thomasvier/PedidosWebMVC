using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PedidosWeb.Models;
using PedidosWeb.DAL;
using PedidosWeb.Models.Admin;

namespace PedidosWeb.BLL
{
    public class AutenticacaoBll
    {
        public static TipoUsuario RetornarTipoUsuario(string login)
        {
            Contexto db = new Contexto();

            TipoUsuario tipoUsuario = (from c in db.Usuarios
                        where c.Login.Equals(login)
                        select c.Role).FirstOrDefault();

            return tipoUsuario;
        }
    }
}