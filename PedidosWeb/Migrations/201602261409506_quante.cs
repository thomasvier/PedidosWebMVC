namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quante : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ItemPedido", "Quantidade");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ItemPedido", "Quantidade", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
