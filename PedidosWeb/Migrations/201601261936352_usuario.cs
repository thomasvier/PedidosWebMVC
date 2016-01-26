namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuario : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "Nome", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Usuario", "Login", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.Usuario", "Email", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "Email", c => c.String());
            AlterColumn("dbo.Usuario", "Login", c => c.String());
            AlterColumn("dbo.Usuario", "Nome", c => c.String());
        }
    }
}
