namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tonne : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ItemPedido", "ValorTotal", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ItemPedido", "ValorTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
