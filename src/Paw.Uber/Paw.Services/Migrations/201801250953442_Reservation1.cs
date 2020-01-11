namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reservation1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservation", "PetId", "dbo.Pet");
            DropIndex("dbo.Reservation", new[] { "PetId" });
            CreateTable(
                "dbo.PetReservation",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        ReservationId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.Reservation", t => t.ReservationId)
                .Index(t => t.ProviderId)
                .Index(t => t.ReservationId)
                .Index(t => t.PetId)
                .Index(t => t.UpdatedById);
            
            AddColumn("dbo.Service", "PetReservation_Id", c => c.Guid());
            CreateIndex("dbo.Service", "PetReservation_Id");
            AddForeignKey("dbo.Service", "PetReservation_Id", "dbo.PetReservation", "Id");
            DropColumn("dbo.Reservation", "PetId");
            DropColumn("dbo.Reservation", "ScheduledCheckin");
            DropColumn("dbo.Reservation", "ScheduledCheckout");
            DropColumn("dbo.Reservation", "ActualCheckin");
            DropColumn("dbo.Reservation", "ActualCheckout");
            DropColumn("dbo.Reservation", "Cancelled");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservation", "Cancelled", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Reservation", "ActualCheckout", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Reservation", "ActualCheckin", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Reservation", "ScheduledCheckout", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Reservation", "ScheduledCheckin", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Reservation", "PetId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Service", "PetReservation_Id", "dbo.PetReservation");
            DropForeignKey("dbo.PetReservation", "ReservationId", "dbo.Reservation");
            DropForeignKey("dbo.PetReservation", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.PetReservation", "PetId", "dbo.Pet");
            DropForeignKey("dbo.PetReservation", "UpdatedById", "dbo.User");
            DropIndex("dbo.Service", new[] { "PetReservation_Id" });
            DropIndex("dbo.PetReservation", new[] { "UpdatedById" });
            DropIndex("dbo.PetReservation", new[] { "PetId" });
            DropIndex("dbo.PetReservation", new[] { "ReservationId" });
            DropIndex("dbo.PetReservation", new[] { "ProviderId" });
            DropColumn("dbo.Service", "PetReservation_Id");
            DropTable("dbo.PetReservation");
            CreateIndex("dbo.Reservation", "PetId");
            AddForeignKey("dbo.Reservation", "PetId", "dbo.Pet", "Id");
        }
    }
}
