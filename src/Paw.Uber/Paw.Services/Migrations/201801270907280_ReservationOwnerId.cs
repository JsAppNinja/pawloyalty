namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReservationOwnerId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservation", "OwnerId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Reservation", "OwnerId");
            AddForeignKey("dbo.Reservation", "OwnerId", "dbo.Owner", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "OwnerId", "dbo.Owner");
            DropIndex("dbo.Reservation", new[] { "OwnerId" });
            DropColumn("dbo.Reservation", "OwnerId");
        }
    }
}
