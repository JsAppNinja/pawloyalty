namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchedulerType : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SchedulerType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Action = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.SkuCategory", "SchedulerTypeId", c => c.Guid(nullable: false, defaultValue: Paw.Services.Common.SchedulerType.Appointment));
            CreateIndex("dbo.SkuCategory", "SchedulerTypeId");
            AddForeignKey("dbo.SkuCategory", "SchedulerTypeId", "dbo.SchedulerType", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkuCategory", "SchedulerTypeId", "dbo.SchedulerType");
            DropIndex("dbo.SkuCategory", new[] { "SchedulerTypeId" });
            DropColumn("dbo.SkuCategory", "SchedulerTypeId");
            DropTable("dbo.SchedulerType");
        }
    }
}
