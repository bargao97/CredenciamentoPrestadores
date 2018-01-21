namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criacaoVlaoresTabelaCredenciamento : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CREDENCIAMENTOS", "VLR_PRESTADOR", c => c.Double(nullable: false));
            AddColumn("dbo.CREDENCIAMENTOS", "VLR_100PLANO", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {

        }
    }
}
