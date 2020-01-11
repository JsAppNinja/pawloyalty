namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventScheduler : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.SchedulerEventType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SchedulerEventPet",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        SchedulerEventId = c.Guid(nullable: false),
                        PetId = c.Guid(nullable: false),
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
                .ForeignKey("dbo.SchedulerEvent", t => t.SchedulerEventId)
                .Index(t => t.ProviderId)
                .Index(t => t.SchedulerEventId)
                .Index(t => t.PetId)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.SchedulerEvent",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        Title = c.String(maxLength: 250),
                        Description = c.String(maxLength: 1000),
                        IsAllDay = c.Boolean(nullable: false),
                        Start = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        End = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        StartTimezone = c.String(maxLength: 50),
                        EndTimezone = c.String(maxLength: 50),
                        RecurrenceRule = c.String(maxLength: 1000),
                        RecurrenceException = c.String(maxLength: 1000),
                        SchedulerEventTypeId = c.Guid(nullable: false),
                        EmployeeId = c.Guid(),
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
                .ForeignKey("dbo.Employee", t => t.EmployeeId)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .ForeignKey("dbo.SchedulerEventType", t => t.SchedulerEventTypeId)
                .Index(t => t.ProviderId)
                .Index(t => t.SchedulerEventTypeId)
                .Index(t => t.EmployeeId)
                .Index(t => t.UpdatedById);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchedulerEvent", "SchedulerEventTypeId", "dbo.SchedulerEventType");
            DropForeignKey("dbo.SchedulerEventPet", "SchedulerEventId", "dbo.SchedulerEvent");
            DropForeignKey("dbo.SchedulerEvent", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.SchedulerEvent", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.SchedulerEvent", "UpdatedById", "dbo.User");
            DropForeignKey("dbo.SchedulerEventPet", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.SchedulerEventPet", "PetId", "dbo.Pet");
            DropForeignKey("dbo.SchedulerEventPet", "UpdatedById", "dbo.User");
            DropForeignKey("dbo.Employee", "ProviderId", "dbo.Provider");
            DropIndex("dbo.SchedulerEvent", new[] { "UpdatedById" });
            DropIndex("dbo.SchedulerEvent", new[] { "EmployeeId" });
            DropIndex("dbo.SchedulerEvent", new[] { "SchedulerEventTypeId" });
            DropIndex("dbo.SchedulerEvent", new[] { "ProviderId" });
            DropIndex("dbo.SchedulerEventPet", new[] { "UpdatedById" });
            DropIndex("dbo.SchedulerEventPet", new[] { "PetId" });
            DropIndex("dbo.SchedulerEventPet", new[] { "SchedulerEventId" });
            DropIndex("dbo.SchedulerEventPet", new[] { "ProviderId" });
            DropIndex("dbo.Employee", new[] { "ProviderId" });
            DropTable("dbo.SchedulerEvent");
            DropTable("dbo.SchedulerEventPet");
            DropTable("dbo.SchedulerEventType");
            DropTable("dbo.Employee");
        }
    }
}
