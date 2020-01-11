namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Reservation : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VaccinationRecord",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderGroupId = c.Guid(nullable: false),
                        VaccineId = c.Guid(nullable: false),
                        PetId = c.Guid(nullable: false),
                        Date = c.DateTime(precision: 7, storeType: "datetime2"),
                        Expiration = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pet", t => t.PetId)
                .ForeignKey("dbo.ProviderGroup", t => t.ProviderGroupId)
                .ForeignKey("dbo.Vaccine", t => t.VaccineId)
                .Index(t => t.ProviderGroupId)
                .Index(t => t.VaccineId)
                .Index(t => t.PetId);
            
            CreateTable(
                "dbo.Vaccine",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Pet", "Deceased", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pet", "Blacklisted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VaccinationRecord", "VaccineId", "dbo.Vaccine");
            DropForeignKey("dbo.VaccinationRecord", "ProviderGroupId", "dbo.ProviderGroup");
            DropForeignKey("dbo.VaccinationRecord", "PetId", "dbo.Pet");
            DropIndex("dbo.VaccinationRecord", new[] { "PetId" });
            DropIndex("dbo.VaccinationRecord", new[] { "VaccineId" });
            DropIndex("dbo.VaccinationRecord", new[] { "ProviderGroupId" });
            DropColumn("dbo.Pet", "Blacklisted");
            DropColumn("dbo.Pet", "Deceased");
            DropTable("dbo.Vaccine");
            DropTable("dbo.VaccinationRecord");
        }
    }
}
