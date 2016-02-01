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

        public IPagedList<Cliente> ListaClientesPaginacao(int? page, string filtro, string tipoTitularFiltro, string sortOrder, string ativoFiltro)
        {
            var clientes = from c in db.Clientes
                           select c;

            if (!String.IsNullOrEmpty(filtro))
            {
                clientes = clientes.Where(s => s.RazaoSocial.Contains(filtro));
            }

            int tipo = int.TryParse(tipoTitularFiltro, out tipo) ? tipo : 2;

            if(tipo < 2)
            {
                TipoTitular tipoTitular = (TipoTitular)tipo;
                clientes = clientes.Where(x => x.Tipo == tipoTitular);
            }

            int ativo = int.TryParse(ativoFiltro, out ativo) ? ativo : 2;

            if(ativo < 2)
            {
                bool situacao = bool.Parse(ativo.ToString());
                clientes = clientes.Where(x => x.Ativo == situacao);
            }

            switch (sortOrder)
            {
                case "razaosocial_desc":
                    clientes = clientes.OrderByDescending(s => s.RazaoSocial);
                    break;
                case "NomeFantasia":
                    clientes = clientes.OrderBy(s => s.NomeFantasia);
                    break;
                case "nomefantasia_desc":
                    clientes = clientes.OrderByDescending(s => s.NomeFantasia);
                    break;
                default:
                    clientes = clientes.OrderBy(s => s.RazaoSocial);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return clientes.ToPagedList(pageNumber, pageSize);
        }
    }
}