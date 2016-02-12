namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produtosrequ : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produto", "PrecoUnitario", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Produto", "PrecoQuantidade", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Produto", "QuantidadePreco", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produto", "QuantidadePreco", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Produto", "PrecoQuantidade", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Produto", "PrecoUnitario", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
