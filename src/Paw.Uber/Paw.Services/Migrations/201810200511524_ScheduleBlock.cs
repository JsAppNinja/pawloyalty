namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleBlock : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleBlock",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        ScheduleRuleId = c.Guid(),
                        StartHour = c.Int(nullable: false),
                        StartMinute = c.Int(nullable: false),
                        EndHour = c.Int(nullable: false),
                        EndMinute = c.Int(nullable: false),
                        StartDay = c.Int(nullable: false),
                        StartMonth = c.Int(nullable: false),
                        StartYear = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        End = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDay = c.Int(nullable: false),
                        EndMonth = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        IsAllDay = c.Boolean(nullable: false),
                        EmployeeId = c.Guid(),
                        Saved = c.DateTime(precision: 7, storeType: "datetime2"),
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
                .ForeignKey("dbo.ScheduleRule", t => t.ScheduleRuleId)
                .Index(t => t.ProviderId)
                .Index(t => t.ScheduleRuleId)
                .Index(t => t.EmployeeId)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.ScheduleRule",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        EmployeeId = c.Guid(),
                        StartHour = c.Int(),
                        StartMinute = c.Int(),
                        EndHour = c.Int(),
                        EndMinute = c.Int(),
                        IsAllDay = c.Boolean(nullable: false),
                        Saved = c.DateTime(precision: 7, storeType: "datetime2"),
                        Sunday = c.Boolean(nullable: false),
                        Monday = c.Boolean(nullable: false),
                        Tuesday = c.Boolean(nullable: false),
                        Wednesday = c.Boolean(nullable: false),
                        Thursday = c.Boolean(nullable: false),
                        Friday = c.Boolean(nullable: false),
                        Saturday = c.Boolean(nullable: false),
                        IsRule = c.Boolean(nullable: false),
                        RuleStartDate = c.DateTime(nullable: false, storeType: "date"),
                        RuleEndDate = c.DateTime(storeType: "date"),
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
                .Index(t => t.EmployeeId)
                .Index(t => t.UpdatedById);
            
            AddColumn("dbo.Employee", "TestFlag", c => c.Boolean());
            AddColumn("dbo.Employee", "TestGroupId", c => c.Guid());
            AlterColumn("dbo.ProviderGroup", "TestFlag", c => c.Boolean());
            AlterColumn("dbo.User", "TestFlag", c => c.Boolean());
            AlterColumn("dbo.Owner", "TestFlag", c => c.Boolean());
            AlterColumn("dbo.Pet", "TestFlag", c => c.Boolean());
            AlterColumn("dbo.Provider", "TestFlag", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleBlock", "ScheduleRuleId", "dbo.ScheduleRule");
            DropForeignKey("dbo.ScheduleRule", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.ScheduleRule", "UpdatedById", "dbo.User");
            DropForeignKey("dbo.ScheduleBlock", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.ScheduleBlock", "EmployeeId", "dbo.Employee");
            DropForeignKey("dbo.ScheduleBlock", "UpdatedById", "dbo.User");
            DropIndex("dbo.ScheduleRule", new[] { "UpdatedById" });
            DropIndex("dbo.ScheduleRule", new[] { "EmployeeId" });
            DropIndex("dbo.ScheduleBlock", new[] { "UpdatedById" });
            DropIndex("dbo.ScheduleBlock", new[] { "EmployeeId" });
            DropIndex("dbo.ScheduleBlock", new[] { "ScheduleRuleId" });
            DropIndex("dbo.ScheduleBlock", new[] { "ProviderId" });
            AlterColumn("dbo.Provider", "TestFlag", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Pet", "TestFlag", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Owner", "TestFlag", c => c.Boolean(nullable: false));
            AlterColumn("dbo.User", "TestFlag", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ProviderGroup", "TestFlag", c => c.Boolean(nullable: false));
            DropColumn("dbo.Employee", "TestGroupId");
            DropColumn("dbo.Employee", "TestFlag");
            DropTable("dbo.ScheduleRule");
            DropTable("dbo.ScheduleBlock");
        }
    }
}
