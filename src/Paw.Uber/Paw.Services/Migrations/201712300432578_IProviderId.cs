namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IProviderId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.InvoiceItem", "ProviderId", c => c.Guid(nullable: false));
            CreateIndex("dbo.InvoiceItem", "ProviderId");
            AddForeignKey("dbo.InvoiceItem", "ProviderId", "dbo.Provider", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItem", "ProviderId", "dbo.Provider");
            DropIndex("dbo.InvoiceItem", new[] { "ProviderId" });
            DropColumn("dbo.InvoiceItem", "ProviderId");
        }
    }
}
