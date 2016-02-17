namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clipednovo : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Pedido", "ClienteID");
            AddForeignKey("dbo.Pedido", "ClienteID", "dbo.Cliente", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedido", "ClienteID", "dbo.Cliente");
            DropIndex("dbo.Pedido", new[] { "ClienteID" });
        }
    }
}
