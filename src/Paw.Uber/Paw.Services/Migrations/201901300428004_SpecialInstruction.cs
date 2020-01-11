namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecialInstruction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pet", "SpecialInstruction", c => c.String(maxLength: 400));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pet", "SpecialInstruction");
        }
    }
}
