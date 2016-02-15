namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idclipro : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pedido", "Cliente_ID", "dbo.Cliente");
            DropForeignKey("dbo.ItemPedido", "Produto_ID", "dbo.Produto");
            DropIndex("dbo.Pedido", new[] { "Cliente_ID" });
            DropIndex("dbo.ItemPedido", new[] { "Produto_ID" });
            AddColumn("dbo.Pedido", "IDCliente", c => c.Int(nullable: false));
            AddColumn("dbo.ItemPedido", "IDProduto", c => c.Int(nullable: false));
            DropColumn("dbo.Pedido", "Cliente_ID");
            DropColumn("dbo.ItemPedido", "Produto_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemPedido", "Produto_ID", c => c.Int());
            AddColumn("dbo.Pedido", "Cliente_ID", c => c.Int());
            DropColumn("dbo.ItemPedido", "IDProduto");
            DropColumn("dbo.Pedido", "IDCliente");
            CreateIndex("dbo.ItemPedido", "Produto_ID");
            CreateIndex("dbo.Pedido", "Cliente_ID");
            AddForeignKey("dbo.ItemPedido", "Produto_ID", "dbo.Produto", "ID");
            AddForeignKey("dbo.Pedido", "Cliente_ID", "dbo.Cliente", "ID");
        }
    }
}
