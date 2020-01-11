namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkuExternalId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sku", new[] { "ProviderId" });
            DropIndex("dbo.SkuCategory", new[] { "ProviderId" });
            DropIndex("dbo.Dimension", new[] { "ProviderId" });
            AddColumn("dbo.Sku", "ExternalId", c => c.Guid());
            AddColumn("dbo.Sku", "IsPrimary", c => c.Boolean(nullable: false));
            AddColumn("dbo.Sku", "DisplayOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Sku", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.SkuCategory", "ExternalId", c => c.Guid());
            AddColumn("dbo.SkuCategory", "IsPrimary", c => c.Boolean(nullable: false));
            AddColumn("dbo.SkuCategory", "DisplayOrder", c => c.Int(nullable: false));
            AddColumn("dbo.SkuCategory", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.SkuCategory", "MessageId", c => c.Guid(nullable: false));
            AddColumn("dbo.SkuCategory", "Created", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SkuCategory", "CreatedById", c => c.Guid());
            AddColumn("dbo.SkuCategory", "Updated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SkuCategory", "UpdatedById", c => c.Guid());
            AddColumn("dbo.SkuCategory", "MessageType", c => c.String(maxLength: 250));
            AddColumn("dbo.SkuCategory", "MachineName", c => c.String(maxLength: 250));
            AddColumn("dbo.SkuDimension", "ProviderId", c => c.Guid(nullable: false));
            AddColumn("dbo.SkuDimension", "ExternalId", c => c.Guid());
            AddColumn("dbo.Dimension", "ExternalId", c => c.Guid());
            CreateIndex("dbo.SkuCategory", "UpdatedById");

            //CreateIndex("dbo.Sku", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_Sku_ProviderId_ExternalId");
            //CreateIndex("dbo.SkuCategory", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_SkuCategory_ProviderId_ExternalId");            
            //CreateIndex("dbo.SkuDimension", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_SkuDimension_ProviderId_ExternalId");
            //CreateIndex("dbo.Dimension", new[] { "ProviderId", "ExternalId" }, unique: true, name: "UK_Dimension_ProviderId_ExternalId");

            this.Sql(@"CREATE UNIQUE NONCLUSTERED INDEX UK_Sku_ProviderId_ExternalId ON dbo.Sku (ProviderId, ExternalId) WHERE ExternalId IS NOT NULL");
            this.Sql(@"CREATE UNIQUE NONCLUSTERED INDEX UK_SkuCategory_ProviderId_ExternalId ON dbo.SkuCategory (ProviderId, ExternalId) WHERE ExternalId IS NOT NULL");
            this.Sql(@"CREATE UNIQUE NONCLUSTERED INDEX UK_SkuDimension_ProviderId_ExternalId ON dbo.SkuDimension (ProviderId, ExternalId) WHERE ExternalId IS NOT NULL");
            this.Sql(@"CREATE UNIQUE NONCLUSTERED INDEX UK_Dimension_ProviderId_ExternalId ON dbo.Dimension (ProviderId, ExternalId) WHERE ExternalId IS NOT NULL");

            AddForeignKey("dbo.SkuCategory", "UpdatedById", "dbo.User", "Id");
            AddForeignKey("dbo.SkuDimension", "ProviderId", "dbo.Provider", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkuDimension", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.SkuCategory", "UpdatedById", "dbo.User");
            DropIndex("dbo.Dimension", "UK_Dimension_ProviderId_ExternalId");
            DropIndex("dbo.SkuDimension", "UK_SkuDimension_ProviderId_ExternalId");
            DropIndex("dbo.SkuCategory", new[] { "UpdatedById" });
            DropIndex("dbo.SkuCategory", "UK_SkuCategory_ProviderId_ExternalId");
            DropIndex("dbo.Sku", "UK_Sku_ProviderId_ExternalId");
            DropColumn("dbo.Dimension", "ExternalId");
            DropColumn("dbo.SkuDimension", "ExternalId");
            DropColumn("dbo.SkuDimension", "ProviderId");
            DropColumn("dbo.SkuCategory", "MachineName");
            DropColumn("dbo.SkuCategory", "MessageType");
            DropColumn("dbo.SkuCategory", "UpdatedById");
            DropColumn("dbo.SkuCategory", "Updated");
            DropColumn("dbo.SkuCategory", "CreatedById");
            DropColumn("dbo.SkuCategory", "Created");
            DropColumn("dbo.SkuCategory", "MessageId");
            DropColumn("dbo.SkuCategory", "IsDeleted");
            DropColumn("dbo.SkuCategory", "DisplayOrder");
            DropColumn("dbo.SkuCategory", "IsPrimary");
            DropColumn("dbo.SkuCategory", "ExternalId");
            DropColumn("dbo.Sku", "IsDeleted");
            DropColumn("dbo.Sku", "DisplayOrder");
            DropColumn("dbo.Sku", "IsPrimary");
            DropColumn("dbo.Sku", "ExternalId");
            CreateIndex("dbo.Dimension", "ProviderId");
            CreateIndex("dbo.SkuCategory", "ProviderId");
            CreateIndex("dbo.Sku", "ProviderId");
        }
    }
}
