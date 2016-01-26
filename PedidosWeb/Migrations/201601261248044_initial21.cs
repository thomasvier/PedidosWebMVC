namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cliente", "NomeFantasia", c => c.String(nullable: false, maxLength: 500));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cliente", "NomeFantasia", c => c.String(maxLength: 500));
        }
    }
}
