namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchedulerEventSkuCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchedulerEvent", "SkuCategoryId", c => c.Guid());
            CreateIndex("dbo.SchedulerEvent", "SkuCategoryId");
            AddForeignKey("dbo.SchedulerEvent", "SkuCategoryId", "dbo.SkuCategory", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchedulerEvent", "SkuCategoryId", "dbo.SkuCategory");
            DropIndex("dbo.SchedulerEvent", new[] { "SkuCategoryId" });
            DropColumn("dbo.SchedulerEvent", "SkuCategoryId");
        }
    }
}
