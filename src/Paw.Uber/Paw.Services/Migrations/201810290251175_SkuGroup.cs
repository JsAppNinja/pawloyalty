namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkuGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkuGroup",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        ExternalId = c.String(maxLength: 100),
                        Name = c.String(maxLength: 50),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .Index(t => new { t.ProviderId, t.ExternalId }, unique: true, name: "UK_SkuGroup_ProviderId_ExternalId");
            
            CreateTable(
                "dbo.SkuGroupSku",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        ExternalId = c.String(maxLength: 100),
                        Type = c.Int(nullable: false),
                        SkuGroupId = c.Guid(nullable: false),
                        SkuId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .ForeignKey("dbo.Sku", t => t.SkuId)
                .ForeignKey("dbo.SkuGroup", t => t.SkuGroupId)
                .Index(t => new { t.ProviderId, t.ExternalId }, unique: true, name: "UK_SkuGroupSku_ProviderId_ExternalId")
                .Index(t => t.SkuGroupId)
                .Index(t => t.SkuId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkuGroupSku", "SkuGroupId", "dbo.SkuGroup");
            DropForeignKey("dbo.SkuGroupSku", "SkuId", "dbo.Sku");
            DropForeignKey("dbo.SkuGroupSku", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.SkuGroup", "ProviderId", "dbo.Provider");
            DropIndex("dbo.SkuGroupSku", new[] { "SkuId" });
            DropIndex("dbo.SkuGroupSku", new[] { "SkuGroupId" });
            DropIndex("dbo.SkuGroupSku", "UK_SkuGroupSku_ProviderId_ExternalId");
            DropIndex("dbo.SkuGroup", "UK_SkuGroup_ProviderId_ExternalId");
            DropTable("dbo.SkuGroupSku");
            DropTable("dbo.SkuGroup");
        }
    }
}
