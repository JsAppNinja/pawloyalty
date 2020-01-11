namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExternalIdString : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sku", "UK_Sku_ProviderId_ExternalId");
            DropIndex("dbo.SkuCategory", "UK_SkuCategory_ProviderId_ExternalId");
            DropIndex("dbo.SkuDimension", "UK_SkuDimension_ProviderId_ExternalId");
            DropIndex("dbo.Dimension", "UK_Dimension_ProviderId_ExternalId");

            AlterColumn("dbo.Sku", "ExternalId", c => c.String(maxLength: 100));
            AlterColumn("dbo.SkuCategory", "ExternalId", c => c.String(maxLength: 100));
            AlterColumn("dbo.SkuDimension", "ExternalId", c => c.String(maxLength: 100));
            AlterColumn("dbo.Dimension", "ExternalId", c => c.String(maxLength: 100));

            //CreateIndex("dbo.Sku", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_Sku_ProviderId_ExternalId");
            //CreateIndex("dbo.SkuCategory", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_SkuCategory_ProviderId_ExternalId");
            //CreateIndex("dbo.SkuDimension", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_SkuDimension_ProviderId_ExternalId");
            //CreateIndex("dbo.Dimension", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_Dimension_ProviderId_ExternalId");

            this.Sql(@"CREATE UNIQUE NONCLUSTERED INDEX UK_Sku_ProviderId_ExternalId ON dbo.Sku (ProviderId, ExternalId) WHERE ExternalId IS NOT NULL");
            this.Sql(@"CREATE UNIQUE NONCLUSTERED INDEX UK_SkuCategory_ProviderId_ExternalId ON dbo.SkuCategory (ProviderId, ExternalId) WHERE ExternalId IS NOT NULL");
            this.Sql(@"CREATE UNIQUE NONCLUSTERED INDEX UK_SkuDimension_ProviderId_ExternalId ON dbo.SkuDimension (ProviderId, ExternalId) WHERE ExternalId IS NOT NULL");
            this.Sql(@"CREATE UNIQUE NONCLUSTERED INDEX UK_Dimension_ProviderId_ExternalId ON dbo.Dimension (ProviderId, ExternalId) WHERE ExternalId IS NOT NULL");

        }

        public override void Down()
        {
            DropIndex("dbo.Dimension", "UK_Dimension_ProviderId_ExternalId");
            DropIndex("dbo.SkuDimension", "UK_SkuDimension_ProviderId_ExternalId");
            DropIndex("dbo.SkuCategory", "UK_SkuCategory_ProviderId_ExternalId");
            DropIndex("dbo.Sku", "UK_Sku_ProviderId_ExternalId");
            AlterColumn("dbo.Dimension", "ExternalId", c => c.Guid());
            AlterColumn("dbo.SkuDimension", "ExternalId", c => c.Guid());
            AlterColumn("dbo.SkuCategory", "ExternalId", c => c.Guid());
            AlterColumn("dbo.Sku", "ExternalId", c => c.Guid());
            CreateIndex("dbo.Dimension", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_Dimension_ProviderId_ExternalId");
            CreateIndex("dbo.SkuDimension", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_SkuDimension_ProviderId_ExternalId");
            CreateIndex("dbo.SkuCategory", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_SkuCategory_ProviderId_ExternalId");
            CreateIndex("dbo.Sku", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_Sku_ProviderId_ExternalId");
        }
    }
}
