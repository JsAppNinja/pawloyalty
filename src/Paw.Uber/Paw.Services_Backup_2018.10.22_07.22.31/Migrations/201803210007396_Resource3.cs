namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resource3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Resource", "UK_Resource_Key");
            AlterColumn("dbo.Resource", "Key", c => c.String(maxLength: 55));
            AlterColumn("dbo.Resource", "ShortDescription", c => c.String(maxLength: 55));
            CreateIndex("dbo.Resource", new[] { "ProviderId", "Key" }, unique: true, name: "UK_Resource_ProviderId_Key");
            CreateIndex("dbo.Resource", "SkuCategoryId");
            AddForeignKey("dbo.Resource", "SkuCategoryId", "dbo.SkuCategory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resource", "SkuCategoryId", "dbo.SkuCategory");
            DropIndex("dbo.Resource", new[] { "SkuCategoryId" });
            DropIndex("dbo.Resource", "UK_Resource_ProviderId_Key");
            AlterColumn("dbo.Resource", "ShortDescription", c => c.String(maxLength: 20));
            AlterColumn("dbo.Resource", "Key", c => c.String(maxLength: 20));
            CreateIndex("dbo.Resource", new[] { "Key", "ProviderId" }, unique: true, name: "UK_Resource_Key");
        }
    }
}
