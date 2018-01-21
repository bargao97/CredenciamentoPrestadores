namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Atualizado18102017 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.COLABORADORES", "REGIAO", c => c.String(maxLength: 100));
            AddColumn("dbo.CREDENCIAMENTOS", "REGIAO", c => c.String(maxLength: 100));
            DropColumn("dbo.COLABORADORES", "ESTADO");
        }
        
        public override void Down()
        {
            AddColumn("dbo.COLABORADORES", "ESTADO", c => c.String(maxLength: 100));
            DropColumn("dbo.CREDENCIAMENTOS", "REGIAO");
            DropColumn("dbo.COLABORADORES", "REGIAO");
        }
    }
}
