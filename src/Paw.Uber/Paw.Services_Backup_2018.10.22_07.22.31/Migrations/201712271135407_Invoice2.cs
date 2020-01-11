namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Invoice2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "Total", c => c.Double());
            AddColumn("dbo.InvoiceItem", "PetId", c => c.Guid());
            AlterColumn("dbo.InvoiceItem", "Quantity", c => c.Int());
            CreateIndex("dbo.InvoiceItem", "PetId");
            AddForeignKey("dbo.InvoiceItem", "PetId", "dbo.Pet", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItem", "PetId", "dbo.Pet");
            DropIndex("dbo.InvoiceItem", new[] { "PetId" });
            AlterColumn("dbo.InvoiceItem", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.InvoiceItem", "PetId");
            DropColumn("dbo.Invoice", "Total");
        }
    }
}
