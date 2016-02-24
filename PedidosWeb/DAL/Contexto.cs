using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using PedidosWeb.Models.Admin;

namespace PedidosWeb.DAL
{
    public class Contexto : DbContext
    {
        public Contexto() : base("PedidosDBContext")
        {
        }
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<PedidosWeb.Models.Pedido> Pedidos { get; set; }

        public System.Data.Entity.DbSet<PedidosWeb.Models.Admin.Representante> Representantes { get; set; }

        public System.Data.Entity.DbSet<PedidosWeb.Models.ItemPedido> ItemPedidos { get; set; }
    }
}