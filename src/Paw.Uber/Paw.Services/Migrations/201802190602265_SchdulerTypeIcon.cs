namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchdulerTypeIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchedulerType", "Icon", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SchedulerType", "Icon");
        }
    }
}
