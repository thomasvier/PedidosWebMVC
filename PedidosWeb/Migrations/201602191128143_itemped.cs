namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemped : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ItemPedido", "Pedido_ID", "dbo.Pedido");
            DropIndex("dbo.ItemPedido", new[] { "Pedido_ID" });
            DropTable("dbo.ItemPedido");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ItemPedido",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        IDProduto = c.Int(nullable: false),
                        ValorItem = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pedido_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.ItemPedido", "Pedido_ID");
            AddForeignKey("dbo.ItemPedido", "Pedido_ID", "dbo.Pedido", "ID");
        }
    }
}
