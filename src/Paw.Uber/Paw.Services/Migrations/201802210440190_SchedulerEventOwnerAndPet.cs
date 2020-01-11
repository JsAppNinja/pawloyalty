namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SchedulerEventOwnerAndPet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SchedulerEvent", "OwnerId", c => c.Guid());
            AddColumn("dbo.SchedulerEvent", "PetId", c => c.Guid());
            CreateIndex("dbo.SchedulerEvent", "OwnerId");
            CreateIndex("dbo.SchedulerEvent", "PetId");
            AddForeignKey("dbo.SchedulerEvent", "OwnerId", "dbo.Owner", "Id");
            AddForeignKey("dbo.SchedulerEvent", "PetId", "dbo.Pet", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SchedulerEvent", "PetId", "dbo.Pet");
            DropForeignKey("dbo.SchedulerEvent", "OwnerId", "dbo.Owner");
            DropIndex("dbo.SchedulerEvent", new[] { "PetId" });
            DropIndex("dbo.SchedulerEvent", new[] { "OwnerId" });
            DropColumn("dbo.SchedulerEvent", "PetId");
            DropColumn("dbo.SchedulerEvent", "OwnerId");
        }
    }
}
