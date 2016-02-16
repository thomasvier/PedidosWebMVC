namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clienterep : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cliente", "IDRepresentante", c => c.Int(nullable: false));
            AddColumn("dbo.Usuario", "IDCliente", c => c.Int(nullable: false));
            AddColumn("dbo.Usuario", "IDRepresentante", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuario", "IDRepresentante");
            DropColumn("dbo.Usuario", "IDCliente");
            DropColumn("dbo.Cliente", "IDRepresentante");
        }
    }
}
