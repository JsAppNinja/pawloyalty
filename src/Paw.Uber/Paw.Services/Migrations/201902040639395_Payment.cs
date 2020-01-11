namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Payment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        ReservationId = c.Guid(),
                        TypeName = c.String(maxLength: 200),
                        Amount = c.Double(),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .ForeignKey("dbo.Reservation", t => t.ReservationId)
                .Index(t => t.ProviderId)
                .Index(t => t.ReservationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payment", "ReservationId", "dbo.Reservation");
            DropForeignKey("dbo.Payment", "ProviderId", "dbo.Provider");
            DropIndex("dbo.Payment", new[] { "ReservationId" });
            DropIndex("dbo.Payment", new[] { "ProviderId" });
            DropTable("dbo.Payment");
        }
    }
}
