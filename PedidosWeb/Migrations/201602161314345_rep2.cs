namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rep2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Representante",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 500),
                        Email = c.String(maxLength: 500),
                        Telefone = c.String(maxLength: 20),
                        Celular = c.String(maxLength: 20),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Representante");
        }
    }
}
