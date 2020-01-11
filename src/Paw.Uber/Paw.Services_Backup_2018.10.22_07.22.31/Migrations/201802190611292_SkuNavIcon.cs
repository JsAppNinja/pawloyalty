namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkuNavIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sku", "NavIcon", c => c.String(maxLength: 50));
            AddColumn("dbo.SkuCategory", "NavIcon", c => c.String(maxLength: 50));
            AddColumn("dbo.SkuCategory", "NavDisplayOrder", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SkuCategory", "NavDisplayOrder");
            DropColumn("dbo.SkuCategory", "NavIcon");
            DropColumn("dbo.Sku", "NavIcon");
        }
    }
}
