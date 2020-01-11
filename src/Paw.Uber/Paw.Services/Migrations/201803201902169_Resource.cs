namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Resource : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resource",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(maxLength: 20),
                        Capacity = c.Int(),
                        SkuCategoryId = c.Guid(),
                        MaxPetWeight = c.Int(),
                        IsShared = c.Boolean(nullable: false),
                        ShortDescription = c.String(maxLength: 20),
                        Description = c.String(maxLength: 100),
                        LegacyId = c.String(maxLength: 20),
                        ProviderId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .Index(t => t.Key, unique: true, name: "UK_Resource_Key")
                .Index(t => t.ProviderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resource", "ProviderId", "dbo.Provider");
            DropIndex("dbo.Resource", new[] { "ProviderId" });
            DropIndex("dbo.Resource", "UK_Resource_Key");
            DropTable("dbo.Resource");
        }
    }
}
