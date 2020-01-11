namespace Paw.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Dimension",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.Provider",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(maxLength: 100),
                        Name = c.String(),
                        Url = c.String(),
                        PhoneNumber = c.String(),
                        Status = c.Boolean(nullable: false),
                        TestFlag = c.Boolean(nullable: false, defaultValueSql: "'FALSE'"),
                        TestGroupId = c.Guid(),
                        LegacyId = c.Guid(),
                        ImportDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        ProviderGroupId = c.Guid(nullable: false),
                        MessageId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.Guid(),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedById = c.Guid(),
                        MessageType = c.String(maxLength: 250),
                        MachineName = c.String(maxLength: 250),
                        ProviderGroup_Id = c.Guid(),
                        ProviderGroup_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UpdatedById)
                .ForeignKey("dbo.ProviderGroup", t => t.ProviderGroup_Id)
                .ForeignKey("dbo.ProviderGroup", t => t.ProviderGroup_Id1)
                .ForeignKey("dbo.ProviderGroup", t => t.ProviderGroupId)
                .Index(t => t.Key, unique: true, name: "IX_Provider_Key")
                .Index(t => t.ProviderGroupId)
                .Index(t => t.UpdatedById)
                .Index(t => t.ProviderGroup_Id)
                .Index(t => t.ProviderGroup_Id1);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 254),
                        UserName = c.String(nullable: false, maxLength: 254),
                        EmailConfirmed = c.Boolean(nullable: false),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        TestFlag = c.Boolean(nullable: false, defaultValueSql: "'FALSE'"),
                        TestGroupId = c.Guid(),
                        LegacyId = c.Guid(),
                        ImportDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        LockoutEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 7, storeType: "datetime2"),
                        PasswordHash = c.String(),
                        PhoneNumber = c.String(maxLength: 20),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        SecurityStamp = c.String(maxLength: 200),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        UseLegacyPassword = c.Boolean(nullable: false),
                        LegacyPasswordAndSalt = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "IX_User_UserName");
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Guid(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Role", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        OwnerId = c.Guid(),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsOpen = c.Boolean(nullable: false),
                        MessageId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.Guid(),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedById = c.Guid(),
                        MessageType = c.String(maxLength: 250),
                        MachineName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UpdatedById)
                .ForeignKey("dbo.Owner", t => t.OwnerId)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .Index(t => t.ProviderId)
                .Index(t => t.OwnerId)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.InvoiceItem",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        InvoiceId = c.Guid(nullable: false),
                        SkuId = c.Guid(),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 250),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        ParentId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId)
                .ForeignKey("dbo.InvoiceItem", t => t.ParentId)
                .ForeignKey("dbo.Sku", t => t.SkuId)
                .Index(t => t.InvoiceId)
                .Index(t => t.SkuId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Sku",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        SkuCategoryId = c.Guid(),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 250),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        ParentId = c.Guid(),
                        MessageId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.Guid(),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedById = c.Guid(),
                        MessageType = c.String(maxLength: 250),
                        MachineName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UpdatedById)
                .ForeignKey("dbo.Sku", t => t.ParentId)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .ForeignKey("dbo.SkuCategory", t => t.SkuCategoryId)
                .Index(t => t.ProviderId)
                .Index(t => t.SkuCategoryId)
                .Index(t => t.ParentId)
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.SkuCategory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ProviderId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provider", t => t.ProviderId)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.SkuDimension",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SkuId = c.Guid(nullable: false),
                        DimensionId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Dimension", t => t.DimensionId)
                .ForeignKey("dbo.Sku", t => t.SkuId)
                .Index(t => new { t.SkuId, t.DimensionId }, unique: true, name: "IX_SkuDimension");

            CreateTable(
                "dbo.Owner",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    FirstName = c.String(maxLength: 100),
                    LastName = c.String(maxLength: 100),
                    PhoneNumber = c.String(maxLength: 20),
                    AltPhoneNumber = c.String(maxLength: 20),
                    Email = c.String(maxLength: 300),
                    StreetAddress = c.String(maxLength: 200),
                    City = c.String(maxLength: 50),
                    State = c.String(maxLength: 20),
                    PostalCode = c.String(maxLength: 20),
                    ProviderGroupId = c.Guid(nullable: false),
                    TestFlag = c.Boolean(nullable: false, defaultValueSql: "'FALSE'"),
                    TestGroupId = c.Guid(),
                    LegacyId = c.Guid(),
                    ImportDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    MessageId = c.Guid(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    CreatedById = c.Guid(),
                    Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    UpdatedById = c.Guid(),
                    MessageType = c.String(maxLength: 250),
                    MachineName = c.String(maxLength: 250),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UpdatedById)
                .ForeignKey("dbo.ProviderGroup", t => t.ProviderGroupId)
                .Index(t => t.Email, unique: true, name: "IX_Owner_Email")
                .Index(t => t.ProviderGroupId)
                .Index(t => t.UpdatedById);

            CreateTable(
                "dbo.Pet",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(maxLength: 100),
                    DOB = c.DateTime(storeType: "date"),
                    Gender = c.Int(),
                    Weight = c.Double(),
                    BreedId = c.Guid(),
                    VetId = c.Guid(),
                    ProviderGroupId = c.Guid(nullable: false),
                    OwnerId = c.Guid(nullable: false),
                    TestFlag = c.Boolean(nullable: false, defaultValueSql: "'FALSE'"),
                    TestGroupId = c.Guid(),
                    LegacyId = c.Guid(),
                    ImportDate = c.DateTime(precision: 7, storeType: "datetime2"),
                    MessageId = c.Guid(nullable: false),
                    Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    CreatedById = c.Guid(),
                    Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    UpdatedById = c.Guid(),
                    MessageType = c.String(maxLength: 250),
                    MachineName = c.String(maxLength: 250),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Breed", t => t.BreedId)
                .ForeignKey("dbo.User", t => t.UpdatedById)
                .ForeignKey("dbo.Owner", t => t.OwnerId)
                .ForeignKey("dbo.ProviderGroup", t => t.ProviderGroupId)
                .Index(t => t.BreedId)
                .Index(t => t.ProviderGroupId)
                .Index(t => t.OwnerId)
                .Index(t => t.UpdatedById);

            CreateTable(
                "dbo.Breed",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 50),
                        ProviderGroupId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProviderGroup", t => t.ProviderGroupId)
                .Index(t => t.Name, unique: true)
                .Index(t => t.ProviderGroupId);
            
            CreateTable(
                "dbo.ProviderGroup",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 200),
                        TestFlag = c.Boolean(nullable: false, defaultValueSql: "'FALSE'"),
                        TestGroupId = c.Guid(),
                        LegacyId = c.Guid(),
                        ImportDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        MessageId = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CreatedById = c.Guid(),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedById = c.Guid(),
                        MessageType = c.String(maxLength: 250),
                        MachineName = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UpdatedById)
                .Index(t => t.Name, unique: true, name: "IX_ProviderGroup_Name")
                .Index(t => t.UpdatedById);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.Dimension", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.Provider", "ProviderGroupId", "dbo.ProviderGroup");
            DropForeignKey("dbo.Invoice", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.Invoice", "OwnerId", "dbo.Owner");
            DropForeignKey("dbo.Pet", "ProviderGroupId", "dbo.ProviderGroup");
            DropForeignKey("dbo.Pet", "OwnerId", "dbo.Owner");
            DropForeignKey("dbo.Pet", "UpdatedById", "dbo.User");
            DropForeignKey("dbo.Pet", "BreedId", "dbo.Breed");
            DropForeignKey("dbo.Provider", "ProviderGroup_Id1", "dbo.ProviderGroup");
            DropForeignKey("dbo.Provider", "ProviderGroup_Id", "dbo.ProviderGroup");
            DropForeignKey("dbo.Owner", "ProviderGroupId", "dbo.ProviderGroup");
            DropForeignKey("dbo.ProviderGroup", "UpdatedById", "dbo.User");
            DropForeignKey("dbo.Breed", "ProviderGroupId", "dbo.ProviderGroup");
            DropForeignKey("dbo.Owner", "UpdatedById", "dbo.User");
            DropForeignKey("dbo.InvoiceItem", "SkuId", "dbo.Sku");
            DropForeignKey("dbo.SkuDimension", "SkuId", "dbo.Sku");
            DropForeignKey("dbo.SkuDimension", "DimensionId", "dbo.Dimension");
            DropForeignKey("dbo.Sku", "SkuCategoryId", "dbo.SkuCategory");
            DropForeignKey("dbo.SkuCategory", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.Sku", "ProviderId", "dbo.Provider");
            DropForeignKey("dbo.Sku", "ParentId", "dbo.Sku");
            DropForeignKey("dbo.Sku", "UpdatedById", "dbo.User");
            DropForeignKey("dbo.InvoiceItem", "ParentId", "dbo.InvoiceItem");
            DropForeignKey("dbo.InvoiceItem", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.Invoice", "UpdatedById", "dbo.User");
            DropForeignKey("dbo.Provider", "UpdatedById", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropIndex("dbo.ProviderGroup", new[] { "UpdatedById" });
            DropIndex("dbo.ProviderGroup", "IX_ProviderGroup_Name");
            DropIndex("dbo.Breed", new[] { "ProviderGroupId" });
            DropIndex("dbo.Breed", new[] { "Name" });
            DropIndex("dbo.Pet", new[] { "UpdatedById" });
            DropIndex("dbo.Pet", new[] { "OwnerId" });
            DropIndex("dbo.Pet", new[] { "ProviderGroupId" });
            DropIndex("dbo.Pet", new[] { "BreedId" });
            DropIndex("dbo.Owner", new[] { "UpdatedById" });
            DropIndex("dbo.Owner", new[] { "ProviderGroupId" });
            DropIndex("dbo.Owner", "IX_Owner_Email");
            DropIndex("dbo.SkuDimension", "IX_SkuDimension");
            DropIndex("dbo.SkuCategory", new[] { "ProviderId" });
            DropIndex("dbo.Sku", new[] { "UpdatedById" });
            DropIndex("dbo.Sku", new[] { "ParentId" });
            DropIndex("dbo.Sku", new[] { "SkuCategoryId" });
            DropIndex("dbo.Sku", new[] { "ProviderId" });
            DropIndex("dbo.InvoiceItem", new[] { "ParentId" });
            DropIndex("dbo.InvoiceItem", new[] { "SkuId" });
            DropIndex("dbo.InvoiceItem", new[] { "InvoiceId" });
            DropIndex("dbo.Invoice", new[] { "UpdatedById" });
            DropIndex("dbo.Invoice", new[] { "OwnerId" });
            DropIndex("dbo.Invoice", new[] { "ProviderId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.User", "IX_User_UserName");
            DropIndex("dbo.Provider", new[] { "ProviderGroup_Id1" });
            DropIndex("dbo.Provider", new[] { "ProviderGroup_Id" });
            DropIndex("dbo.Provider", new[] { "UpdatedById" });
            DropIndex("dbo.Provider", new[] { "ProviderGroupId" });
            DropIndex("dbo.Provider", "IX_Provider_Key");
            DropIndex("dbo.Dimension", new[] { "ProviderId" });
            DropTable("dbo.Role");
            DropTable("dbo.ProviderGroup");
            DropTable("dbo.Breed");
            DropTable("dbo.Pet");
            DropTable("dbo.Owner");
            DropTable("dbo.SkuDimension");
            DropTable("dbo.SkuCategory");
            DropTable("dbo.Sku");
            DropTable("dbo.InvoiceItem");
            DropTable("dbo.Invoice");
            DropTable("dbo.UserRole");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.Provider");
            DropTable("dbo.Dimension");
        }
    }
}
