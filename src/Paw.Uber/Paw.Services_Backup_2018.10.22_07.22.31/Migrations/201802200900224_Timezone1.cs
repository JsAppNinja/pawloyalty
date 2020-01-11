namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Timezone1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Timezone",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100),
                        TZString = c.String(maxLength: 100),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Provider", "TimezoneInfoId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Provider", "TimezoneInfoId");
            DropTable("dbo.Timezone");
        }
    }
}
