namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reservation2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.SkuLine", new[] { "ReservationId" });
            AlterColumn("dbo.SkuLine", "ReservationId", c => c.Guid());
            CreateIndex("dbo.SkuLine", "ReservationId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.SkuLine", new[] { "ReservationId" });
            AlterColumn("dbo.SkuLine", "ReservationId", c => c.Guid(nullable: false));
            CreateIndex("dbo.SkuLine", "ReservationId");
        }
    }
}
