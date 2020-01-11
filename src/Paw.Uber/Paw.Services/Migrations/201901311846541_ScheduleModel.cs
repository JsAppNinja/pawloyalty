namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SkuCategory", "ScheduleModel", c => c.String(maxLength: 60));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SkuCategory", "ScheduleModel");
        }
    }
}
