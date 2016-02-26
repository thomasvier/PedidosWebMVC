namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quante2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ItemPedido", "Quantidade", c => c.Decimal(precision: 18, scale: 3));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ItemPedido", "Quantidade");
        }
    }
}
