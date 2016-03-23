namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repclinotob2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cliente", "IDRepresentante", "dbo.Representante");
            DropIndex("dbo.Cliente", new[] { "IDRepresentante" });
            AlterColumn("dbo.Cliente", "IDRepresentante", c => c.Int());
            CreateIndex("dbo.Cliente", "IDRepresentante");
            AddForeignKey("dbo.Cliente", "IDRepresentante", "dbo.Representante", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "IDRepresentante", "dbo.Representante");
            DropIndex("dbo.Cliente", new[] { "IDRepresentante" });
            AlterColumn("dbo.Cliente", "IDRepresentante", c => c.Int(nullable: false));
            CreateIndex("dbo.Cliente", "IDRepresentante");
            AddForeignKey("dbo.Cliente", "IDRepresentante", "dbo.Representante", "ID", cascadeDelete: true);
        }
    }
}
