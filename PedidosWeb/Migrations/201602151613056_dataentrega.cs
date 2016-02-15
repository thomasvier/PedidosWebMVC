namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataentrega : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "DataEntrega", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "DataEntrega");
        }
    }
}
