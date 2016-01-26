namespace PedidosWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Usuarios", newName: "Usuario");
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RazaoSocial = c.String(nullable: false, maxLength: 500),
                        NomeFantasia = c.String(maxLength: 500),
                        CPFCNPJ = c.String(maxLength: 15),
                        InscricaoEstadual = c.String(),
                        Telefone = c.String(),
                        Celular = c.String(),
                        Email = c.String(),
                        Endereco = c.String(maxLength: 500),
                        Cidade = c.String(maxLength: 500),
                        Bairro = c.String(maxLength: 500),
                        Estado = c.String(maxLength: 2),
                        Numero = c.String(),
                        Cep = c.String(),
                        Complemento = c.String(maxLength: 500),
                        Ativo = c.Boolean(nullable: false),
                        Tipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AlterColumn("dbo.Usuario", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "Email", c => c.String(maxLength: 100));
            DropTable("dbo.Cliente");
            RenameTable(name: "dbo.Usuario", newName: "Usuarios");
        }
    }
}
