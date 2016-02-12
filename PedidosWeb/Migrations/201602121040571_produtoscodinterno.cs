namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class produtoscodinterno : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produto", "CodigoInterno", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produto", "CodigoInterno", c => c.Int(nullable: false));
        }
    }
}
