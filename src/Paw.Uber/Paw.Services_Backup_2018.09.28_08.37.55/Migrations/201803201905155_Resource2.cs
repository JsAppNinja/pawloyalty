namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resource2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Resource", "UK_Resource_Key");
            DropIndex("dbo.Resource", new[] { "ProviderId" });
            CreateIndex("dbo.Resource", new[] { "Key", "ProviderId" }, unique: true, name: "UK_Resource_Key");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Resource", "UK_Resource_Key");
            CreateIndex("dbo.Resource", "ProviderId");
            CreateIndex("dbo.Resource", "Key", unique: true, name: "UK_Resource_Key");
        }
    }
}
