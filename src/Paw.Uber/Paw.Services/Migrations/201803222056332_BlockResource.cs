namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlockResource : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chit",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        OwnerId = c.Guid(),
                        PetId = c.Guid(),
                        SkuId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Owner", t => t.OwnerId)
                .ForeignKey("dbo.Pet", t => t.PetId)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .ForeignKey("dbo.Sku", t => t.SkuId)
                .Index(t => t.ProviderId)
                .Index(t => t.OwnerId)
                .Index(t => t.PetId)
                .Index(t => t.SkuId);
            
            AddColumn("dbo.SchedulerEvent", "ResourceId", c => c.Guid());
            AddColumn("dbo.SchedulerEventPet", "Rank", c => c.Int());
            CreateIndex("dbo.SchedulerEvent", "ResourceId");
            AddForeignKey("dbo.SchedulerEvent", "ResourceId", "dbo.Resource", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chit", "SkuId", "dbo.Sku");
            DropForeignKey("dbo.Chit", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.Chit", "PetId", "dbo.Pet");
            DropForeignKey("dbo.Chit", "OwnerId", "dbo.Owner");
            DropForeignKey("dbo.SchedulerEvent", "ResourceId", "dbo.Resource");
            DropIndex("dbo.Chit", new[] { "SkuId" });
            DropIndex("dbo.Chit", new[] { "PetId" });
            DropIndex("dbo.Chit", new[] { "OwnerId" });
            DropIndex("dbo.Chit", new[] { "ProviderId" });
            DropIndex("dbo.SchedulerEvent", new[] { "ResourceId" });
            DropColumn("dbo.SchedulerEventPet", "Rank");
            DropColumn("dbo.SchedulerEvent", "ResourceId");
            DropTable("dbo.Chit");
        }
    }
}
