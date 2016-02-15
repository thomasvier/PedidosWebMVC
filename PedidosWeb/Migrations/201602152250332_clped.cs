namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clped : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "Cliente_ID", c => c.Int());
            CreateIndex("dbo.Pedido", "Cliente_ID");
            AddForeignKey("dbo.Pedido", "Cliente_ID", "dbo.Cliente", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedido", "Cliente_ID", "dbo.Cliente");
            DropIndex("dbo.Pedido", new[] { "Cliente_ID" });
            DropColumn("dbo.Pedido", "Cliente_ID");
        }
    }
}
