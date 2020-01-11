namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Service : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        PetId = c.Guid(nullable: false),
                        ScheduledCheckin = c.DateTime(precision: 7, storeType: "datetime2"),
                        ScheduledCheckout = c.DateTime(precision: 7, storeType: "datetime2"),
                        ActualCheckin = c.DateTime(precision: 7, storeType: "datetime2"),
                        ActualCheckout = c.DateTime(precision: 7, storeType: "datetime2"),
                        Cancelled = c.DateTime(precision: 7, storeType: "datetime2"),
                        MessageId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.Guid(),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedById = c.Guid(),
                        MessageType = c.String(maxLength: 250),
                        MachineName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UpdatedById)
                .ForeignKey("dbo.Pet", t => t.PetId)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .Index(t => t.ProviderId)
                .Index(t => t.PetId)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Service",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Start = c.DateTime(precision: 7, storeType: "datetime2"),
                        ProviderId = c.Guid(nullable: false),
                        ReservationId = c.Guid(nullable: false),
                        SkuId = c.Guid(),
                        IsPrimary = c.Boolean(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Service", t => t.ParentId)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .ForeignKey("dbo.Reservation", t => t.ReservationId)
                .ForeignKey("dbo.Sku", t => t.SkuId)
                .Index(t => t.ProviderId)
                .Index(t => t.ReservationId)
                .Index(t => t.SkuId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Service", "SkuId", "dbo.Sku");
            DropForeignKey("dbo.Service", "ReservationId", "dbo.Reservation");
            DropForeignKey("dbo.Service", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.Service", "ParentId", "dbo.Service");
            DropForeignKey("dbo.Reservation", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.Reservation", "PetId", "dbo.Pet");
            DropForeignKey("dbo.Reservation", "UpdatedById", "dbo.User");
            DropIndex("dbo.Service", new[] { "ParentId" });
            DropIndex("dbo.Service", new[] { "SkuId" });
            DropIndex("dbo.Service", new[] { "ReservationId" });
            DropIndex("dbo.Service", new[] { "ProviderId" });
            DropIndex("dbo.Reservation", new[] { "UpdatedById" });
            DropIndex("dbo.Reservation", new[] { "PetId" });
            DropIndex("dbo.Reservation", new[] { "ProviderId" });
            DropTable("dbo.Service");
            DropTable("dbo.Reservation");
        }
    }
}
