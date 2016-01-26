namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clientes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cliente", "CPFCNPJ", c => c.String(maxLength: 18));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cliente", "CPFCNPJ", c => c.String(maxLength: 15));
        }
    }
}
