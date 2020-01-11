namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "ProviderId", c => c.Guid());
            AddColumn("dbo.User", "ProviderGroupId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "ProviderGroupId");
            DropColumn("dbo.User", "ProviderId");
        }
    }
}
