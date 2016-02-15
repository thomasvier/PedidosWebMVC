namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requicodin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Produto", "CodigoInterno", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Produto", "CodigoInterno", c => c.String());
        }
    }
}
