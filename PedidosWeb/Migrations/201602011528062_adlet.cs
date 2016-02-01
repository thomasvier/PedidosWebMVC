namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adlet : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Cliente", "CodigoInterno");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cliente", "CodigoInterno", c => c.Int(nullable: false));
        }
    }
}
