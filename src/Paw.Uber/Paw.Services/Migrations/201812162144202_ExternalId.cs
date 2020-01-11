namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExternalId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resource", "ExternalId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resource", "ExternalId");
        }
    }
}
