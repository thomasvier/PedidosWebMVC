namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuario", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "Role");
        }
    }
}
