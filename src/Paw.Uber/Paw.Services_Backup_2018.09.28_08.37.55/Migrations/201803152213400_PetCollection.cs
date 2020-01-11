namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PetCollection : DbMigration
    {
        public override void Up()
        {
            //DropIndex("dbo.Provider", new[] { "ProviderGroupId" });
            //DropIndex("dbo.Provider", new[] { "ProviderGroup_Id" });
            //DropIndex("dbo.Provider", new[] { "ProviderGroup_Id1" });
            //DropColumn("dbo.Pet", "ProviderGroupId");
            //DropColumn("dbo.Provider", "ProviderGroupId");
            //RenameColumn(table: "dbo.Pet", name: "ProviderGroup_Id", newName: "ProviderGroupId");
            //RenameColumn(table: "dbo.Provider", name: "ProviderGroup_Id1", newName: "ProviderGroupId");
            //AlterColumn("dbo.Provider", "ProviderGroupId", c => c.Guid(nullable: false));
            //CreateIndex("dbo.Provider", "ProviderGroupId");
            //DropColumn("dbo.Provider", "ProviderGroup_Id");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Provider", "ProviderGroup_Id", c => c.Guid());
            //DropIndex("dbo.Provider", new[] { "ProviderGroupId" });
            //AlterColumn("dbo.Provider", "ProviderGroupId", c => c.Guid());
            //RenameColumn(table: "dbo.Provider", name: "ProviderGroupId", newName: "ProviderGroup_Id1");
            //RenameColumn(table: "dbo.Pet", name: "ProviderGroupId", newName: "ProviderGroup_Id");
            //AddColumn("dbo.Provider", "ProviderGroupId", c => c.Guid(nullable: false));
            //AddColumn("dbo.Pet", "ProviderGroupId", c => c.Guid(nullable: false));
            //CreateIndex("dbo.Provider", "ProviderGroup_Id1");
            //CreateIndex("dbo.Provider", "ProviderGroup_Id");
            //CreateIndex("dbo.Provider", "ProviderGroupId");
        }
    }
}
