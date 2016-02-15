namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pedidos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pedido",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CodigoInterno = c.String(),
                        DataPedido = c.DateTime(nullable: false),
                        ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SituacaoPedido = c.Int(nullable: false),
                        Cliente_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cliente", t => t.Cliente_ID)
                .Index(t => t.Cliente_ID);
            
            CreateTable(
                "dbo.ItemPedido",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ValorItem = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Produto_ID = c.Int(),
                        Pedido_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Produto", t => t.Produto_ID)
                .ForeignKey("dbo.Pedido", t => t.Pedido_ID)
                .Index(t => t.Produto_ID)
                .Index(t => t.Pedido_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemPedido", "Pedido_ID", "dbo.Pedido");
            DropForeignKey("dbo.ItemPedido", "Produto_ID", "dbo.Produto");
            DropForeignKey("dbo.Pedido", "Cliente_ID", "dbo.Cliente");
            DropIndex("dbo.ItemPedido", new[] { "Pedido_ID" });
            DropIndex("dbo.ItemPedido", new[] { "Produto_ID" });
            DropIndex("dbo.Pedido", new[] { "Cliente_ID" });
            DropTable("dbo.ItemPedido");
            DropTable("dbo.Pedido");
        }
    }
}
