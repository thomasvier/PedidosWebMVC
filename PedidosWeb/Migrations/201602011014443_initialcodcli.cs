namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcodcli : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "CodigoInterno", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cliente", "CodigoInterno");
        }
    }
}
