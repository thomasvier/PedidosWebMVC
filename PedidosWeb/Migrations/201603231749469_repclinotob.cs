namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repclinotob : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Cliente", "IDRepresentante");
            AddForeignKey("dbo.Cliente", "IDRepresentante", "dbo.Representante", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "IDRepresentante", "dbo.Representante");
            DropIndex("dbo.Cliente", new[] { "IDRepresentante" });
        }
    }
}
