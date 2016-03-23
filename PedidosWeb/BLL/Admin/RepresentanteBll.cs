using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PedidosWeb.Models.Admin;
using PedidosWeb.DAL;
using PedidosWeb.Models;
using PagedList;

namespace PedidosWeb.BLL.Admin
{
    public class RepresentanteBll
    {
        Contexto db;

        public RepresentanteBll()
        {
            db = new Contexto();
        }

        public IPagedList<Representante> ListaRepresentantesPaginacao(int? page, string filtro, string sortOrder,
                                                            string ativoFiltro, string codigoRepresentanteFiltro)
        {
            int ativo = int.TryParse(ativoFiltro, out ativo) ? ativo : 2;
            int ID = int.TryParse(codigoRepresentanteFiltro, out ID) ? ID : 0;

            var representantes = from c in db.Representantes
                           select c;

            if (!String.IsNullOrEmpty(filtro))
            {
                representantes = representantes.Where(s => s.Nome.Contains(filtro));
            }


            if (ativo < 2)
            {
                bool situacao = ativo.Equals(0) ? false : true;
                representantes = representantes.Where(x => x.Ativo == situacao);
            }

            if (ID > 0)
            {
                representantes = representantes.Where(x => x.ID.Equals(ID));
            }

            switch (sortOrder)
            {
                case "nome_desc":
                    representantes = representantes.OrderByDescending(s => s.Nome);
                    break;
                case "ID":
                    representantes = representantes.OrderBy(s => s.ID);
                    break;
                case "id_desc":
                    representantes = representantes.OrderByDescending(s => s.ID);
                    break;
                default:
                    representantes = representantes.OrderBy(s => s.Nome);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return representantes.ToPagedList(pageNumber, pageSize);
        }

        /// <summary>
        /// Retorna os representantes ativos
        /// </summary>
        /// <returns></returns>
        public static List<Representante> ListarRepresentantesAtivos()
        {
            Contexto db = new Contexto();

            List<Representante> representantes = (from r in db.Representantes
                                                  where r.Ativo.Equals(true)
                                                  orderby r.Nome ascending
                                                  select r).ToList();

            return representantes;
        }

        public static int RetornarNovoID()
        {
            Contexto db = new Contexto();

            int ID = (from r in db.Representantes
                      orderby r.ID descending
                      select r.ID).FirstOrDefault();

            ID++;

            return ID;
        }

        public static Representante RetornarRepresentante(int? ID)
        {
            Contexto db = new Contexto();

            Representante representante = db.Representantes.Where(x => x.ID == ID).FirstOrDefault();

            return representante;
        }
    }
}