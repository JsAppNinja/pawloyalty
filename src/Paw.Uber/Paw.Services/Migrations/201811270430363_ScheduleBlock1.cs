namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleBlock1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScheduleBlock", "EmployeeId", "dbo.Employee");
            DropIndex("dbo.ScheduleBlock", new[] { "EmployeeId" });
            AddColumn("dbo.ScheduleRule", "SkuCategoryId", c => c.Guid());
            AddColumn("dbo.ScheduleRule", "Capacity", c => c.Int());
            AddColumn("dbo.ScheduleRule", "StartTime", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.ScheduleRule", "Duration", c => c.Int());
            AlterColumn("dbo.ScheduleBlock", "End", c => c.DateTime(precision: 7, storeType: "datetime2"));
            CreateIndex("dbo.ScheduleRule", "ProviderId");
            CreateIndex("dbo.ScheduleRule", "SkuCategoryId");
            AddForeignKey("dbo.ScheduleRule", "ProviderId", "dbo.Provider", "Id");
            AddForeignKey("dbo.ScheduleRule", "SkuCategoryId", "dbo.SkuCategory", "Id");
            DropColumn("dbo.ScheduleBlock", "StartHour");
            DropColumn("dbo.ScheduleBlock", "StartMinute");
            DropColumn("dbo.ScheduleBlock", "EndHour");
            DropColumn("dbo.ScheduleBlock", "EndMinute");
            DropColumn("dbo.ScheduleBlock", "StartDay");
            DropColumn("dbo.ScheduleBlock", "StartMonth");
            DropColumn("dbo.ScheduleBlock", "StartYear");
            DropColumn("dbo.ScheduleBlock", "EndDay");
            DropColumn("dbo.ScheduleBlock", "EndMonth");
            DropColumn("dbo.ScheduleBlock", "EndYear");
            DropColumn("dbo.ScheduleBlock", "EmployeeId");
            DropColumn("dbo.ScheduleRule", "StartHour");
            DropColumn("dbo.ScheduleRule", "StartMinute");
            DropColumn("dbo.ScheduleRule", "EndHour");
            DropColumn("dbo.ScheduleRule", "EndMinute");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ScheduleRule", "EndMinute", c => c.Int());
            AddColumn("dbo.ScheduleRule", "EndHour", c => c.Int());
            AddColumn("dbo.ScheduleRule", "StartMinute", c => c.Int());
            AddColumn("dbo.ScheduleRule", "StartHour", c => c.Int());
            AddColumn("dbo.ScheduleBlock", "EmployeeId", c => c.Guid());
            AddColumn("dbo.ScheduleBlock", "EndYear", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduleBlock", "EndMonth", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduleBlock", "EndDay", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduleBlock", "StartYear", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduleBlock", "StartMonth", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduleBlock", "StartDay", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduleBlock", "EndMinute", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduleBlock", "EndHour", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduleBlock", "StartMinute", c => c.Int(nullable: false));
            AddColumn("dbo.ScheduleBlock", "StartHour", c => c.Int(nullable: false));
            DropForeignKey("dbo.ScheduleRule", "SkuCategoryId", "dbo.SkuCategory");
            DropForeignKey("dbo.ScheduleRule", "ProviderId", "dbo.Provider");
            DropIndex("dbo.ScheduleRule", new[] { "SkuCategoryId" });
            DropIndex("dbo.ScheduleRule", new[] { "ProviderId" });
            AlterColumn("dbo.ScheduleBlock", "End", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            DropColumn("dbo.ScheduleRule", "Duration");
            DropColumn("dbo.ScheduleRule", "StartTime");
            DropColumn("dbo.ScheduleRule", "Capacity");
            DropColumn("dbo.ScheduleRule", "SkuCategoryId");
            CreateIndex("dbo.ScheduleBlock", "EmployeeId");
            AddForeignKey("dbo.ScheduleBlock", "EmployeeId", "dbo.Employee", "Id");
        }
    }
}
