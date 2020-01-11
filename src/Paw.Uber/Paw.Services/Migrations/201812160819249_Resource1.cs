namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resource1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Resource", "SkuCategoryId", "dbo.SkuCategory");
            DropIndex("dbo.Resource", "UK_Resource_ProviderId_Key");
            DropIndex("dbo.Resource", new[] { "SkuCategoryId" });
            AddColumn("dbo.Resource", "SkuId", c => c.Guid());
            AddColumn("dbo.SkuLine", "In", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.SkuLine", "Out", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.SkuLine", "ResourceId", c => c.Guid());
            AddColumn("dbo.SkuLine", "Slot", c => c.Int());
            CreateIndex("dbo.Resource", "ProviderId");
            CreateIndex("dbo.Resource", "SkuId");
            CreateIndex("dbo.SkuLine", "ResourceId");
            AddForeignKey("dbo.Resource", "SkuId", "dbo.Sku", "Id");
            AddForeignKey("dbo.SkuLine", "ResourceId", "dbo.Resource", "Id");
            DropColumn("dbo.Resource", "Key");
            DropColumn("dbo.Resource", "SkuCategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resource", "SkuCategoryId", c => c.Guid());
            AddColumn("dbo.Resource", "Key", c => c.String(maxLength: 55));
            DropForeignKey("dbo.SkuLine", "ResourceId", "dbo.Resource");
            DropForeignKey("dbo.Resource", "SkuId", "dbo.Sku");
            DropIndex("dbo.SkuLine", new[] { "ResourceId" });
            DropIndex("dbo.Resource", new[] { "SkuId" });
            DropIndex("dbo.Resource", new[] { "ProviderId" });
            DropColumn("dbo.SkuLine", "Slot");
            DropColumn("dbo.SkuLine", "ResourceId");
            DropColumn("dbo.SkuLine", "Out");
            DropColumn("dbo.SkuLine", "In");
            DropColumn("dbo.Resource", "SkuId");
            CreateIndex("dbo.Resource", "SkuCategoryId");
            CreateIndex("dbo.Resource", new[] { "ProviderId", "Key" }, unique: true, name: "UK_Resource_ProviderId_Key");
            AddForeignKey("dbo.Resource", "SkuCategoryId", "dbo.SkuCategory", "Id");
        }
    }
}
