namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SkuLineType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SkuLine", "SkuLineType", c => c.Int(nullable: false));
            AddColumn("dbo.SkuLine", "Voided", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.SkuLine", "Cancelled", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SkuLine", "Cancelled");
            DropColumn("dbo.SkuLine", "Voided");
            DropColumn("dbo.SkuLine", "SkuLineType");
        }
    }
}
