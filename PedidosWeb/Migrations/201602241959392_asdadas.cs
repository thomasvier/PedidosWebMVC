namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdadas : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ItemPedido", "Quantidade", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.ItemPedido", "PrecoCompra", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ItemPedido", "PrecoCompra", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.ItemPedido", "Quantidade", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
