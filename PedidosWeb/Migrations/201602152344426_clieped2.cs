namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clieped2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pedido", "Cliente_ID", "dbo.Cliente");
            DropIndex("dbo.Pedido", new[] { "Cliente_ID" });
            AddColumn("dbo.Pedido", "ClienteID", c => c.Int(nullable: false));
            DropColumn("dbo.Pedido", "Cliente_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pedido", "Cliente_ID", c => c.Int());
            DropColumn("dbo.Pedido", "ClienteID");
            CreateIndex("dbo.Pedido", "Cliente_ID");
            AddForeignKey("dbo.Pedido", "Cliente_ID", "dbo.Cliente", "ID");
        }
    }
}
