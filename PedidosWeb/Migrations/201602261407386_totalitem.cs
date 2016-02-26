namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totalitem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemPedido", "TotalItem", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.ItemPedido", "ValorTotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemPedido", "ValorTotal", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.ItemPedido", "TotalItem");
        }
    }
}
