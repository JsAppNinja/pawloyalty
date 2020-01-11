namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employee", "Email", c => c.String(maxLength: 150));
            AddColumn("dbo.Employee", "TerminationDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employee", "TerminationDate");
            DropColumn("dbo.Employee", "Email");
        }
    }
}
