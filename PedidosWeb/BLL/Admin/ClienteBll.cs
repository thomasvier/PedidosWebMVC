using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PedidosWeb.DAL;
using PedidosWeb.Models.Admin;
using PagedList;
using PedidosWeb.Models;

namespace PedidosWeb.BLL.Admin
{
    public class ClienteBll
    {
        Contexto db;

        public ClienteBll()
        {
            db = new Contexto();
        }

        public IPagedList<Cliente> ListaClientesPaginacao(int? page, string filtro, 
                                                            string tipoTitularFiltro, string sortOrder, 
                                                            string ativoFiltro, string codigoInternoFiltro)
        {
            int ativo = int.TryParse(ativoFiltro, out ativo) ? ativo : 2;
            int tipo = int.TryParse(tipoTitularFiltro, out tipo) ? tipo : 2;

            var clientes = from c in db.Clientes
                           select c;

            if (!String.IsNullOrEmpty(filtro))
            {
                clientes = clientes.Where(s => s.RazaoSocial.Contains(filtro));
            }            

            if(tipo < 2)
            {
                TipoTitular tipoTitular = (TipoTitular)tipo;
                clientes = clientes.Where(x => x.Tipo == tipoTitular);
            }

            if(ativo < 2)
            {
                bool situacao = ativo.Equals(0) ? false : true;
                clientes = clientes.Where(x => x.Ativo == situacao);
            }

            if (!string.IsNullOrEmpty(codigoInternoFiltro))
            {
                clientes = clientes.Where(x => x.CodigoInterno.Equals(codigoInternoFiltro));
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
                case "CodigoInterno":
                    clientes = clientes.OrderBy(s => s.CodigoInterno);
                    break;
                case "codigointerno_desc":
                    clientes = clientes.OrderByDescending(s => s.CodigoInterno);
                    break;
                default:
                    clientes = clientes.OrderBy(s => s.RazaoSocial);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return clientes.ToPagedList(pageNumber, pageSize);
        }
        
        public static List<Cliente> ListarClientes()
        {
            Contexto db = new Contexto();

            List<Cliente> Clientes = (from c in db.Clientes
                                      orderby c.RazaoSocial ascending
                                      select c).ToList();

            return Clientes;
        }

        public static Cliente RetornarCliente(int ID)
        {
            Contexto db = new Contexto();

            Cliente cliente = (from c in db.Clientes
                               where c.ID.Equals(ID)
                               select c).FirstOrDefault();

            return cliente;
        }

        public static string RetornarRazaoSocial(int ID)
        {
            Contexto db = new Contexto();

            string razaoSocial = (from c in db.Clientes
                                  where c.ID.Equals(ID)
                                  select c.RazaoSocial).FirstOrDefault();

            return razaoSocial;
        }

        public List<Cliente> ListarClientesAtivos()
        {
            List<Cliente> clientes = (from c in db.Clientes
                                      where c.Ativo == true
                                      select c).ToList();

            return clientes;
        }

        public static bool VericarCodigoExistente(Cliente cliente, TipoOperacao tipoOperacao)
        {
            Contexto db = new Contexto();

            List<Cliente> clientes = (from c in db.Clientes
                          where c.CodigoInterno.Equals(cliente.CodigoInterno)
                          select c).ToList();

            if (!string.IsNullOrEmpty(cliente.CodigoInterno))
            {
                if (tipoOperacao.Equals(TipoOperacao.Create))
                {
                    if (clientes.Count > 0)
                        return true;
                }
                else if (tipoOperacao.Equals(TipoOperacao.Update))
                {
                    if (clientes.Count > 0)
                    {
                        foreach (Cliente cli in clientes)
                        {
                            if (cli.CodigoInterno.Equals(cliente.CodigoInterno) && cli.ID != cliente.ID)
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