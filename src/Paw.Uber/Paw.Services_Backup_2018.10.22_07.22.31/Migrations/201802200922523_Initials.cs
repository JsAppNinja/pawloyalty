namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initials : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "Initials", c => c.String(maxLength: 4));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "Initials");
        }
    }
}
