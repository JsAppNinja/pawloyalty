namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkuLine1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SkuLine", new[] { "PetReservationId" });
            RenameColumn(table: "dbo.SkuLine", name: "PetReservationId", newName: "PetReservation_Id");
            CreateTable(
                "dbo.ReservationGroup",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        OwnerId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Owner", t => t.OwnerId)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .Index(t => t.ProviderId)
                .Index(t => t.OwnerId);
            
            AddColumn("dbo.Reservation", "ReservationGroupId", c => c.Guid());
            AddColumn("dbo.SkuLine", "ReservationId", c => c.Guid(nullable: false));
            AddColumn("dbo.SkuLine", "PetId", c => c.Guid());
            AddColumn("dbo.SkuLine", "Name", c => c.String(maxLength: 50));
            AddColumn("dbo.SkuLine", "Description", c => c.String(maxLength: 250));
            AddColumn("dbo.SkuLine", "Amount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.SkuLine", "ScheduleRuleId", c => c.Guid());
            AddColumn("dbo.SkuLine", "StartDate", c => c.DateTime(storeType: "date"));
            AddColumn("dbo.SkuLine", "StartTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.SkuLine", "PetReservation_Id", c => c.Guid());
            CreateIndex("dbo.Reservation", "ReservationGroupId");
            CreateIndex("dbo.SkuLine", "ReservationId");
            CreateIndex("dbo.SkuLine", "PetId");
            CreateIndex("dbo.SkuLine", "ScheduleRuleId");
            CreateIndex("dbo.SkuLine", "PetReservation_Id");
            AddForeignKey("dbo.SkuLine", "PetId", "dbo.Pet", "Id");
            AddForeignKey("dbo.SkuLine", "ReservationId", "dbo.Reservation", "Id");
            AddForeignKey("dbo.SkuLine", "ScheduleRuleId", "dbo.ScheduleRule", "Id");
            AddForeignKey("dbo.Reservation", "ReservationGroupId", "dbo.ReservationGroup", "Id");
            DropColumn("dbo.Reservation", "IsAllDay");
            DropColumn("dbo.Reservation", "Start");
            DropColumn("dbo.Reservation", "DurationMin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservation", "DurationMin", c => c.Int(nullable: false));
            AddColumn("dbo.Reservation", "Start", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Reservation", "IsAllDay", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Reservation", "ReservationGroupId", "dbo.ReservationGroup");
            DropForeignKey("dbo.ReservationGroup", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.ReservationGroup", "OwnerId", "dbo.Owner");
            DropForeignKey("dbo.SkuLine", "ScheduleRuleId", "dbo.ScheduleRule");
            DropForeignKey("dbo.SkuLine", "ReservationId", "dbo.Reservation");
            DropForeignKey("dbo.SkuLine", "PetId", "dbo.Pet");
            DropIndex("dbo.ReservationGroup", new[] { "OwnerId" });
            DropIndex("dbo.ReservationGroup", new[] { "ProviderId" });
            DropIndex("dbo.SkuLine", new[] { "PetReservation_Id" });
            DropIndex("dbo.SkuLine", new[] { "ScheduleRuleId" });
            DropIndex("dbo.SkuLine", new[] { "PetId" });
            DropIndex("dbo.SkuLine", new[] { "ReservationId" });
            DropIndex("dbo.Reservation", new[] { "ReservationGroupId" });
            AlterColumn("dbo.SkuLine", "PetReservation_Id", c => c.Guid(nullable: false));
            DropColumn("dbo.SkuLine", "StartTime");
            DropColumn("dbo.SkuLine", "StartDate");
            DropColumn("dbo.SkuLine", "ScheduleRuleId");
            DropColumn("dbo.SkuLine", "Amount");
            DropColumn("dbo.SkuLine", "Description");
            DropColumn("dbo.SkuLine", "Name");
            DropColumn("dbo.SkuLine", "PetId");
            DropColumn("dbo.SkuLine", "ReservationId");
            DropColumn("dbo.Reservation", "ReservationGroupId");
            DropTable("dbo.ReservationGroup");
            RenameColumn(table: "dbo.SkuLine", name: "PetReservation_Id", newName: "PetReservationId");
            CreateIndex("dbo.SkuLine", "PetReservationId");
        }
    }
}
