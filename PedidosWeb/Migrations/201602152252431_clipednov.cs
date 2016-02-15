namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clipednov : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Pedido", "IDCliente");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pedido", "IDCliente", c => c.Int(nullable: false));
        }
    }
}
