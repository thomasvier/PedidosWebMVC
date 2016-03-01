namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pedido : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedido", "ValorFrete", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pedido", "ValorFrete");
        }
    }
}
