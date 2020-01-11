namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkuLine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SkuLine",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        PetReservationId = c.Guid(nullable: false),
                        SkuId = c.Guid(nullable: false),
                        Quantity = c.Int(),
                        ParentId = c.Guid(),
                        MessageId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.Guid(),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedById = c.Guid(),
                        MessageType = c.String(maxLength: 250),
                        MachineName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SkuLine", t => t.ParentId)
                .ForeignKey("dbo.User", t => t.UpdatedById)
                .ForeignKey("dbo.PetReservation", t => t.PetReservationId)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .ForeignKey("dbo.Sku", t => t.SkuId)
                .Index(t => t.ProviderId)
                .Index(t => t.PetReservationId)
                .Index(t => t.SkuId)
                .Index(t => t.ParentId)
                .Index(t => t.UpdatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkuLine", "SkuId", "dbo.Sku");
            DropForeignKey("dbo.SkuLine", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.SkuLine", "PetReservationId", "dbo.PetReservation");
            DropForeignKey("dbo.SkuLine", "UpdatedById", "dbo.User");
            DropForeignKey("dbo.SkuLine", "ParentId", "dbo.SkuLine");
            DropIndex("dbo.SkuLine", new[] { "UpdatedById" });
            DropIndex("dbo.SkuLine", new[] { "ParentId" });
            DropIndex("dbo.SkuLine", new[] { "SkuId" });
            DropIndex("dbo.SkuLine", new[] { "PetReservationId" });
            DropIndex("dbo.SkuLine", new[] { "ProviderId" });
            DropTable("dbo.SkuLine");
        }
    }
}
