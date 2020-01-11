namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Domain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Provider", "Domain", c => c.String(maxLength: 100, nullable: true));

            this.Sql(@"CREATE UNIQUE NONCLUSTERED INDEX [IX_Provider_Domain] ON [dbo].[Provider] 
                    (
                        [Domain] ASC
                    )
                    WHERE ([Domain] IS NOT NULL)");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Provider", "IX_Provider_Domain");
            DropColumn("dbo.Provider", "Domain");
        }
    }
}
