namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "CodigoInterno", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cliente", "CodigoInterno");
        }
    }
}
