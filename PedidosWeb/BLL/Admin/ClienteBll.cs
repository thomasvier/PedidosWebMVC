using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PedidosWeb.DAL;
using PedidosWeb.Models.Admin;
using PagedList;

namespace PedidosWeb.BLL.Admin
{
    public class ClienteBll
    {
        Contexto db;

        public ClienteBll()
        {
            db = new Contexto();
        }

        public IPagedList<Cliente> ListaClientesPaginacao(int? page, string filtro, string tipoTitularFiltro)
        {
            var clientes = from c in db.Clientes
                           select c;
            
            if (!string.IsNullOrEmpty(filtro))
            {
                clientes = clientes.Where(x => x.RazaoSocial.Contains(filtro));
            }

            int tipo = int.TryParse(tipoTitularFiltro, out tipo) ? tipo : 2;

            if (tipo < 2)
            {
                TipoTitular tipoTitular = (TipoTitular)tipo;

                clientes = clientes.Where(x => x.Tipo == tipoTitular);
            }

            IPagedList<Cliente> clientesPaginados = clientes.OrderBy(x => x.RazaoSocial).ToPagedList(page ?? 1, 10);

            return clientesPaginados;
        }
    }
}