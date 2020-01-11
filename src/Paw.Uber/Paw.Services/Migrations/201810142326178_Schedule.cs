namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Schedule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservation", "IsAllDay", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reservation", "Start", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Reservation", "DurationMin", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservation", "DurationMin");
            DropColumn("dbo.Reservation", "Start");
            DropColumn("dbo.Reservation", "IsAllDay");
        }
    }
}
