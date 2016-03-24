namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pedasda : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pedido", "ClienteID", "dbo.Cliente");
            DropIndex("dbo.Pedido", new[] { "ClienteID" });
            AlterColumn("dbo.Pedido", "ClienteID", c => c.Int());
            CreateIndex("dbo.Pedido", "ClienteID");
            AddForeignKey("dbo.Pedido", "ClienteID", "dbo.Cliente", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedido", "ClienteID", "dbo.Cliente");
            DropIndex("dbo.Pedido", new[] { "ClienteID" });
            AlterColumn("dbo.Pedido", "ClienteID", c => c.Int(nullable: false));
            CreateIndex("dbo.Pedido", "ClienteID");
            AddForeignKey("dbo.Pedido", "ClienteID", "dbo.Cliente", "ID", cascadeDelete: true);
        }
    }
}
