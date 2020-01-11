namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Breed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Breed", "ProviderGroupId", "dbo.ProviderGroup");
            DropIndex("dbo.Breed", new[] { "Name" });
            DropIndex("dbo.Breed", new[] { "ProviderGroupId" });
            AddColumn("dbo.Breed", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.Breed", "AverageWeight", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Breed", "ExternalId", c => c.String(maxLength: 200));
            AddColumn("dbo.Breed", "Country", c => c.String(maxLength: 200));
            AddColumn("dbo.Breed", "Coat", c => c.String(maxLength: 200));
            AddColumn("dbo.Breed", "Pattern", c => c.String(maxLength: 200));
            DropColumn("dbo.Breed", "ProviderGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Breed", "ProviderGroupId", c => c.Guid(nullable: false));
            DropColumn("dbo.Breed", "Pattern");
            DropColumn("dbo.Breed", "Coat");
            DropColumn("dbo.Breed", "Country");
            DropColumn("dbo.Breed", "ExternalId");
            DropColumn("dbo.Breed", "AverageWeight");
            DropColumn("dbo.Breed", "Type");
            CreateIndex("dbo.Breed", "ProviderGroupId");
            CreateIndex("dbo.Breed", "Name", unique: true);
            AddForeignKey("dbo.Breed", "ProviderGroupId", "dbo.ProviderGroup", "Id");
        }
    }
}
