namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cadastraritem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ItemPedido",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDProduto = c.Int(nullable: false),
                        IDPedido = c.Int(nullable: false),
                        Quantidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecoCompra = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pedido", t => t.IDPedido, cascadeDelete: true)
                .ForeignKey("dbo.Produto", t => t.IDProduto, cascadeDelete: true)
                .Index(t => t.IDProduto)
                .Index(t => t.IDPedido);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemPedido", "IDProduto", "dbo.Produto");
            DropForeignKey("dbo.ItemPedido", "IDPedido", "dbo.Pedido");
            DropIndex("dbo.ItemPedido", new[] { "IDPedido" });
            DropIndex("dbo.ItemPedido", new[] { "IDProduto" });
            DropTable("dbo.ItemPedido");
        }
    }
}
