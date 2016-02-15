using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PedidosWeb.DAL;
using System.Data.Entity;
using PagedList;
using PedidosWeb.Models.Admin;
using PedidosWeb.Models;

namespace PedidosWeb.BLL.Admin
{
    public class ProdutoBll
    {
        Contexto db;

        public ProdutoBll()
        {
            db = new Contexto();
        }

        public IPagedList<Produto> ListarProdutosPaginacao(int? page, string filtro, string sortOrder,
                                                            string ativoFiltro, string codigoInternoFiltro)
        {
            int ativo = int.TryParse(ativoFiltro, out ativo) ? ativo : 2;

            var produtos = from c in db.Produtos
                           select c;

            if (!String.IsNullOrEmpty(filtro))
            {
                produtos = produtos.Where(s => s.Descricao.Contains(filtro));
            }

            if (ativo < 2)
            {
                bool situacao = ativo.Equals(0) ? false : true;
                produtos = produtos.Where(x => x.Ativo == situacao);
            }

            if (!string.IsNullOrEmpty(codigoInternoFiltro))
            {
                produtos = produtos.Where(x => x.CodigoInterno.Equals(codigoInternoFiltro));
            }

            switch (sortOrder)
            {
                case "descricao_desc":
                    produtos = produtos.OrderByDescending(s => s.Descricao);
                    break;
                case "CodigoInterno":
                    produtos = produtos.OrderBy(s => s.CodigoInterno);
                    break;
                case "codigointerno_desc":
                    produtos = produtos.OrderByDescending(s => s.CodigoInterno);
                    break;
                default:
                    produtos = produtos.OrderBy(s => s.Descricao);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return produtos.ToPagedList(pageNumber, pageSize);
        }

        public static bool VericarCodigoExistente(Produto produto, TipoOperacao tipoOperacao)
        {
            Contexto db = new Contexto();

            List<Produto> produtos = (from c in db.Produtos
                                      where c.CodigoInterno.Equals(produto.CodigoInterno)
                                      select c).ToList();

            if (!string.IsNullOrEmpty(produto.CodigoInterno))
            {
                if (tipoOperacao.Equals(TipoOperacao.Create))
                {
                    if (produtos.Count > 0)
                        return true;
                }
                else if (tipoOperacao.Equals(TipoOperacao.Update))
                {
                    if (produtos.Count > 0)
                    {
                        foreach (Produto prod in produtos)
                        {
                            if (prod.CodigoInterno.Equals(produto.CodigoInterno) && prod.ID != produto.ID)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public List<Produto> ListarProdutosAtivos()
        {
            List<Produto> Produtos = (from p in db.Produtos
                                      where p.Ativo == true
                                      select p).ToList();

            return Produtos;
        }
    }
}