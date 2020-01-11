namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PetUpdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Owner", "EmergencyContactName", c => c.String(maxLength: 100));
            AddColumn("dbo.Owner", "EmergencyContactPhoneNumber", c => c.String(maxLength: 20));
            AddColumn("dbo.Pet", "Deceased", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pet", "Blacklisted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ReferredBy", "Name", c => c.String());
            DropColumn("dbo.Pet", "IsDeceased");
            DropColumn("dbo.Pet", "IsBlacklisted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pet", "IsBlacklisted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pet", "IsDeceased", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ReferredBy", "Name", c => c.String(maxLength: 100));
            DropColumn("dbo.Pet", "Blacklisted");
            DropColumn("dbo.Pet", "Deceased");
            DropColumn("dbo.Owner", "EmergencyContactPhoneNumber");
            DropColumn("dbo.Owner", "EmergencyContactName");
        }
    }
}
