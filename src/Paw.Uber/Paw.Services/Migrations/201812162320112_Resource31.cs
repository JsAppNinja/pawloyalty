namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resource31 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resource", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.Resource", "Description", c => c.String(maxLength: 500));
            AlterColumn("dbo.Resource", "LegacyId", c => c.String(maxLength: 200));
            DropColumn("dbo.Resource", "ShortDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resource", "ShortDescription", c => c.String(maxLength: 55));
            AlterColumn("dbo.Resource", "LegacyId", c => c.String(maxLength: 20));
            AlterColumn("dbo.Resource", "Description", c => c.String(maxLength: 100));
            DropColumn("dbo.Resource", "Name");
        }
    }
}
