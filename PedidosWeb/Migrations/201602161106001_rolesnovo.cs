namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rolesnovo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "Role", c => c.Int(nullable: false));
            DropColumn("dbo.Usuario", "Tipo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuario", "Tipo", c => c.Int(nullable: false));
            AlterColumn("dbo.Usuario", "Role", c => c.String());
        }
    }
}
