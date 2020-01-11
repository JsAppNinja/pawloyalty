namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Search2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gender",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PetClass",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderGroupId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProviderGroup", t => t.ProviderGroupId)
                .Index(t => t.ProviderGroupId);
            
            AddColumn("dbo.Pet", "GenderId", c => c.Guid());
            AddColumn("dbo.Pet", "IsDeceased", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pet", "IsRescue", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pet", "PetClassId", c => c.Guid());
            AddColumn("dbo.Owner", "EmergencyContactName", c => c.String(maxLength: 100));
            AddColumn("dbo.Owner", "EmergencyContactPhoneNumber", c => c.String(maxLength: 20));
            CreateIndex("dbo.Pet", "GenderId");
            CreateIndex("dbo.Pet", "PetClassId");
            AddForeignKey("dbo.Pet", "GenderId", "dbo.Gender", "Id");
            AddForeignKey("dbo.Pet", "PetClassId", "dbo.PetClass", "Id");
            DropColumn("dbo.Pet", "Gender");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pet", "Gender", c => c.Int());
            DropForeignKey("dbo.Pet", "PetClassId", "dbo.PetClass");
            DropForeignKey("dbo.PetClass", "ProviderGroupId", "dbo.ProviderGroup");
            DropForeignKey("dbo.Pet", "GenderId", "dbo.Gender");
            DropIndex("dbo.PetClass", new[] { "ProviderGroupId" });
            DropIndex("dbo.Pet", new[] { "PetClassId" });
            DropIndex("dbo.Pet", new[] { "GenderId" });
            DropColumn("dbo.Owner", "EmergencyContactPhoneNumber");
            DropColumn("dbo.Owner", "EmergencyContactName");
            DropColumn("dbo.Pet", "PetClassId");
            DropColumn("dbo.Pet", "IsRescue");
            DropColumn("dbo.Pet", "IsDeceased");
            DropColumn("dbo.Pet", "GenderId");
            DropTable("dbo.PetClass");
            DropTable("dbo.Gender");
        }
    }
}
