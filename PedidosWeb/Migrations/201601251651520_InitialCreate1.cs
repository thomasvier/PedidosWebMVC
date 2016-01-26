namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Usuarios", "PerfilID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuarios", "PerfilID", c => c.Int());
        }
    }
}
