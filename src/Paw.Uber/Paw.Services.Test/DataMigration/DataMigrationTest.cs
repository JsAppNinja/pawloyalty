using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Messages.Util.Users;
using System.Collections.Generic;
using Paw.Services.Common;
using System.Data.Entity.Migrations;
using Paw.Services.Util;
using System.Data.SqlClient;

namespace Paw.Services.Test.DataMigration
{
    [TestClass]
    public class DataMigrationTest
    {
        List<Guid> wagAPIGroupList = new List<Guid>() {
                new Guid("31DBE1F0-934D-4AAD-96A0-389577A15D8A"),
                new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB"),
                new Guid("682B7E3E-2E19-4E26-9000-200770293CE7"),
                new Guid("CFAD7E70-6A83-4CE1-B5F5-73C85D7D3C90"),
                new Guid("D99A49FF-B896-4FA3-847B-ABB906D172B5")
            };

        List<Guid> dogtopiaAPIGroupList = new List<Guid>() { new Guid("738C7397-D20D-4999-802F-B0465B7CA6F4") };

        List<Guid> nvaAPIGroupList = new List<Guid>() { new Guid("61B88032-2863-47B5-94B3-63B9765CCD2C") };


        Guid wagProviderGroupId = new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB");
        Guid dogtopiaProviderGroupId = new Guid("4E234298-2477-4E6C-865C-A12F0C72DED9");
        Guid nvaProviderGroupId = new Guid("{9B5FBC0E-CCD7-45BC-9B87-E58EB1009742}");

        [TestMethod]
        public void LoadOwner()
        {
            //string tempTableName = "#Owner";
            //string sourceDbConnection = "SourceDb";
            //string targetDbConnection = "DataContext";

            //Guid providerGroupId = new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB"); // Wag = '284A43DD-F676-41E4-A713-EBC848CF85DB'
            //string selectCommandText = $@"SELECT o1.*, s.Street as StreetAddress, s.City, s.State, s.PostalCode, CAST('{providerGroupId}' AS UNIQUEIDENTIFIER) as ProviderGroupId 
            //    FROM [Owners] o1
            //    LEFT JOIN [StreetAddresses] s ON o1.AddressId = s.Id
            //    WHERE o1.Id IN 
            //        (SELECT DISTINCT o.Id FROM [Owners] o 
            //        JOIN [OwnerProviderDetails] opd ON o.Id = opd.Owner_Id 
            //        JOIN [Providers] p ON opd.Provider_Id = p.Id
            //        WHERE p.ProviderGroupId = '{providerGroupId}') ";
            //string emptyTableCommandText = $@"SELECT * INTO [{tempTableName}] FROM (SELECT o.*, s.Street as StreetAddress, s.City, s.State, s.PostalCode FROM [Owners] o JOIN [StreetAddresses] s ON o.AddressId = s.Id) a; ALTER TABLE [{tempTableName}] ADD ProviderGroupId uniqueidentifier NULL;";


            //string columnMappings = $"FirstName,LastName,Email,PhoneNumber,AltPhoneNumber|AlternativePhoneNumber,StreetAddress,City,State,PostalCode,ProviderGroupId,LegacyId|Id,ImportDate|(GETUTCDATE())";
            //string insertColumnMappings = columnMappings + $",Id,Created|(GETUTCDATE()),CreatedById|('{BootstrapUser.UserId}'),Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";
            //string updateColumnMappings = columnMappings + $",Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";

            //// Step 1. Merge
            //string mergeCommand = $@"MERGE [Owner] AS [Target]
            //    USING #Owner AS [Source]
            //    ON [Target].Id = [Source].Id
            //    WHEN MATCHED THEN UPDATE SET {DataMigrationHelper.GetUpdateColumnClause(updateColumnMappings)} 
            //    WHEN NOT MATCHED THEN INSERT {DataMigrationHelper.GetInsertColumnClause(insertColumnMappings)};";

            //List<string> commandList = new List<string>();
            //commandList.Add(mergeCommand);

            ////
            //string disableIndexSql = "ALTER INDEX IX_Owner_Email ON [Owner] DISABLE; ";
            //DataMigrationHelper.ExecuteNonQuery(targetDbConnection, disableIndexSql);

            ////TODO: Clean up emails with 'NULL' index, enable IX_Owner_Email

            //DataMigrationHelper.LoadData(sourceDbConnection, targetDbConnection, emptyTableCommandText, selectCommandText, tempTableName, commandList);

            this.LoadOwner(wagAPIGroupList, wagProviderGroupId);
            this.LoadOwner(dogtopiaAPIGroupList, dogtopiaProviderGroupId);
            this.LoadOwner(nvaAPIGroupList, nvaProviderGroupId);


        }

        public void LoadOwner(List<Guid> apiGroupList, Guid providerGroupId)
        {
            string tempTableName = "#Owner";
            string sourceDbConnection = "SourceDb";
            string targetDbConnection = "DataContext";

            string predicate = $@"p.APIGroupId IN ({apiGroupList.AsString(x => $"'{x}'", ", ")})";

            string selectCommandText = $@"SELECT o1.*, s.Street as StreetAddress, s.City, s.State, s.PostalCode, CAST('{providerGroupId}' AS UNIQUEIDENTIFIER) as ProviderGroupId 
                FROM [Owners] o1
                LEFT JOIN [StreetAddresses] s ON o1.AddressId = s.Id
                WHERE o1.Id IN 
                    (SELECT DISTINCT o.Id FROM [Owners] o 
                    JOIN [OwnerProviderDetails] opd ON o.Id = opd.Owner_Id 
                    JOIN [Providers] p ON opd.Provider_Id = p.Id
                    WHERE {predicate}) ";
            string emptyTableCommandText = $@"SELECT * INTO [{tempTableName}] FROM (SELECT o.*, s.Street as StreetAddress, s.City, s.State, s.PostalCode FROM [Owners] o JOIN [StreetAddresses] s ON o.AddressId = s.Id) a; ALTER TABLE [{tempTableName}] ADD ProviderGroupId uniqueidentifier NULL;";


            string columnMappings = $"FirstName,LastName,Email,PhoneNumber,AltPhoneNumber|AlternativePhoneNumber,StreetAddress,City,State,PostalCode,ProviderGroupId|'{providerGroupId}',LegacyId|Id,ImportDate|(GETUTCDATE())";
            string insertColumnMappings = columnMappings + $",Id,Created|(GETUTCDATE()),CreatedById|('{BootstrapUser.UserId}'),Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";
            string updateColumnMappings = columnMappings + $",Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";

            // Step 1. Merge
            string mergeCommand = $@"
SET ANSI_WARNINGS OFF
MERGE [Owner] AS [Target]
                USING #Owner AS [Source]
                ON [Target].Id = [Source].Id
                WHEN MATCHED THEN UPDATE SET {DataMigrationHelper.GetUpdateColumnClause(updateColumnMappings)} 
                WHEN NOT MATCHED THEN INSERT {DataMigrationHelper.GetInsertColumnClause(insertColumnMappings)};";

            List<string> commandList = new List<string>();
            commandList.Add(mergeCommand);

            //
            string disableIndexSql = "ALTER INDEX IX_Owner_Email ON [Owner] DISABLE; ";
            DataMigrationHelper.ExecuteNonQuery(targetDbConnection, disableIndexSql);

            //TODO: Clean up emails with 'NULL' index, enable IX_Owner_Email

            DataMigrationHelper.LoadData(sourceDbConnection, targetDbConnection, emptyTableCommandText, selectCommandText, tempTableName, commandList);

        }

        [TestMethod]
        public void LoadProviderGroup()
        {
            string tempTableName = "#ProviderGroup";
            string sourceDbConnection = "SourceDb";
            string targetDbConnection = "DataContext";

            Guid providerGroupId = new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB"); // Wag = '284A43DD-F676-41E4-A713-EBC848CF85DB'

            string columnMappings = $"Name,ImportDate|(GETUTCDATE())";
            string insertColumnMappings = columnMappings + $",Id,Created|(GETUTCDATE()),CreatedById|('{BootstrapUser.UserId}'),Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";
            string updateColumnMappings = columnMappings + $",Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";

            // Step 1. Merge
            string mergeCommand = $@"MERGE [ProviderGroup] AS [Target]
                USING #ProviderGroup AS [Source]
                ON [Target].Id = [Source].Id
                WHEN MATCHED THEN UPDATE SET {DataMigrationHelper.GetUpdateColumnClause(updateColumnMappings)} 
                WHEN NOT MATCHED THEN INSERT {DataMigrationHelper.GetInsertColumnClause(insertColumnMappings)};";
            string emptyTableCommandText = $@"SELECT * INTO [{tempTableName}] FROM (SELECT * FROM [ProviderGroup]) a; ALTER TABLE [{tempTableName}] ADD ProviderGroupId uniqueidentifier NULL;";

            string selectCommandText = $@"SELECT * FROM [ProviderGroup]";

            List<string> commandList = new List<string>();
            commandList.Add(mergeCommand);

            DataMigrationHelper.LoadData(sourceDbConnection, targetDbConnection, emptyTableCommandText, selectCommandText, tempTableName, commandList);


            // Inserts
            using (DataContext context = new DataContext() { CurrentUserId = BootstrapUser.UserId })
            {
                context.ProviderGroupSet.AddOrUpdate(

                    new ProviderGroup() { Id = new Guid("{9B5FBC0E-CCD7-45BC-9B87-E58EB1009742}"), Name = "NVA", ImportDate = DateTime.UtcNow }

                    );
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void LoadProvider()
        {
            //string tempTableName = "#Provider";
            //string sourceDbConnection = "SourceDb";
            //string targetDbConnection = "DataContext";

            // Predicates
            // string predicate = $"ProviderGroupId = '284A43DD-F676-41E4-A713-EBC848CF85DB'"; // Wag = '284A43DD-F676-41E4-A713-EBC848CF85DB'
            //string predicate = $"APIGroup = '738C7397-D20D-4999-802F-B0465B7CA6F4'"; // Dog Topia
            //string predicate = $"APIGroup = '61B88032-2863-47B5-94B3-63B9765CCD2C'"; // NVA

//            string predicate = $@"APIGroupId IN (
//-- Wag
//'31DBE1F0-934D-4AAD-96A0-389577A15D8A',
//'284A43DD-F676-41E4-A713-EBC848CF85DB',
//'682B7E3E-2E19-4E26-9000-200770293CE7',
//'CFAD7E70-6A83-4CE1-B5F5-73C85D7D3C90',
//'D99A49FF-B896-4FA3-847B-ABB906D172B5',
//-- Dogtopia
//'738C7397-D20D-4999-802F-B0465B7CA6F4',
//-- NVA
//'61B88032-2863-47B5-94B3-63B9765CCD2C'
//)";

            // this.LoadProvider(wagAPIGroupList, wagProviderGroupId );
            // this.LoadProvider(dogtopiaAPIGroupList, dogtopiaProviderGroupId);
            // this.LoadProvider(nvaAPIGroupList, nvaProviderGroupId);

            var kvList = new List<KeyValuePair<string, string>>() {
            new KeyValuePair<string, string>("4B4B2764-A221-4597-A973-0C7C47FBD2BC", "waghollywod"),
            new KeyValuePair<string, string>("1543DD05-83D2-484D-9D59-16278995D4F1", "wagrwc"),
            new KeyValuePair<string, string>("22D0DA43-C420-4028-8722-271077969218", "wagsf"),
            new KeyValuePair<string, string>("C53F958A-B6B2-4C0A-958C-482209250255", "wagwsac"),
            new KeyValuePair<string, string>("B005D298-EAFC-46AD-AB7A-4E5E11708842", "wagcarson"),
            new KeyValuePair<string, string>("9D36379D-D03E-4045-B1C7-6C99DA29D8C4", "wagsandiego"),
            new KeyValuePair<string, string>("F5A4D065-710F-4FB6-B3DE-9719A871E18A", "wagoakland"),
            new KeyValuePair<string, string>("93A55FD0-49A6-4443-B9B8-A661FAF1719D", "wagsantaclara"),
            new KeyValuePair<string, string>("2ADB8733-F35A-4C42-8BF5-F4D365A98F52", "wagwestla"),


        };

            using (DataContext context = new DataContext())
            {
                foreach (KeyValuePair<string, string> item in kvList)
                {
                    context.Database.ExecuteSqlCommand("UPDATE [Provider] SET Domain = @Domain WHERE Id = @Id", new SqlParameter("@Domain", item.Value), new SqlParameter("@Id", new Guid(item.Key)));
                }

                context.Database.ExecuteSqlCommand("UPDATE [Provider] SET Domain = [Key] WHERE Domain = ''");
            }

            //string columnMappings = $"Name,Key,PhoneNumber,Status,ProviderGroupId,ImportDate|(GETUTCDATE())";
            //// string columnMappings = $"Name,Key,PhoneNumber,Gender,Status,ProviderGroupId,ImportDate|(GETUTCDATE())";
            //string insertColumnMappings = columnMappings + $",Id,Created|(GETUTCDATE()),CreatedById|('{BootstrapUser.UserId}'),Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";
            //string updateColumnMappings = columnMappings + $",Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";

            //// Step 1. Merge
            //string mergeCommand = $@"MERGE [Provider] AS [Target]
            //    USING #Provider AS [Source]
            //    ON [Target].Id = [Source].Id
            //    WHEN MATCHED THEN UPDATE SET {DataMigrationHelper.GetUpdateColumnClause(updateColumnMappings)} 
            //    WHEN NOT MATCHED THEN INSERT {DataMigrationHelper.GetInsertColumnClause(insertColumnMappings)};";
            //string emptyTableCommandText = $@"SELECT * INTO [{tempTableName}] FROM (SELECT p.*, a.Street, a.City, a.State, a.PostalCode FROM [Providers] p JOIN [StreetAddresses] a ON p.Address_Id = a.Id) a;";

            //string selectCommandText = $@"SELECT p.*, a.Street, a.City, a.State, a.PostalCode FROM [Providers] p JOIN [StreetAddresses] a ON p.Address_Id = a.Id WHERE {predicate}";


            //List<string> commandList = new List<string>();
            //commandList.Add(mergeCommand);

            //DataMigrationHelper.LoadData(sourceDbConnection, targetDbConnection, emptyTableCommandText, selectCommandText, tempTableName, commandList);


        }

        public void LoadProvider(List<Guid> apiGroupList, Guid providerGroupId)
        {
            string tempTableName = "#Provider";
            string sourceDbConnection = "SourceDb";
            string targetDbConnection = "DataContext";

            string predicate = $@"p.APIGroupId IN ({apiGroupList.AsString(x => $"'{x}'", ", ")})";

            string columnMappings = $"Name,Key,PhoneNumber,Status,ProviderGroupId|'{providerGroupId}',ImportDate|(GETUTCDATE())";
            // string columnMappings = $"Name,Key,PhoneNumber,Gender,Status,ProviderGroupId,ImportDate|(GETUTCDATE())";
            string insertColumnMappings = columnMappings + $",Id,Created|(GETUTCDATE()),CreatedById|('{BootstrapUser.UserId}'),Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";
            string updateColumnMappings = columnMappings + $",Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";

            // Step 1. Merge
            string mergeCommand = $@"MERGE [Provider] AS [Target]
                USING #Provider AS [Source]
                ON [Target].Id = [Source].Id
                WHEN MATCHED THEN UPDATE SET {DataMigrationHelper.GetUpdateColumnClause(updateColumnMappings)} 
                WHEN NOT MATCHED THEN INSERT {DataMigrationHelper.GetInsertColumnClause(insertColumnMappings)};";
            string emptyTableCommandText = $@"SELECT * INTO [{tempTableName}] FROM (SELECT p.*, a.Street, a.City, a.State, a.PostalCode FROM [Providers] p JOIN [StreetAddresses] a ON p.Address_Id = a.Id) a;";

            string selectCommandText = $@"SELECT p.*, a.Street, a.City, a.State, a.PostalCode FROM [Providers] p JOIN [StreetAddresses] a ON p.Address_Id = a.Id WHERE {predicate}";


            List<string> commandList = new List<string>();
            commandList.Add(mergeCommand);

            DataMigrationHelper.LoadData(sourceDbConnection, targetDbConnection, emptyTableCommandText, selectCommandText, tempTableName, commandList);

        }

        [TestMethod]
        public void LoadPet()
        {
            //       string tempTableName = "#Pet";
            //       string sourceDbConnection = "SourceDb";
            //       string targetDbConnection = "DataContext";

            //       Guid providerGroupId = new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB"); // Wag = '284A43DD-F676-41E4-A713-EBC848CF85DB'

            //       string columnMappings = $"Name,DOB|BirthDate,Weight,ProviderGroupId,OwnerId|Owner_Id,ImportDate|(GETUTCDATE()),Created|Created";
            //       string insertColumnMappings = columnMappings + $",Id,CreatedById|('{BootstrapUser.UserId}'),Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";
            //       string updateColumnMappings = columnMappings + $",Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";

            //       // Step 1. Merge
            //       string mergeCommand = $@"MERGE [Pet] AS [Target]
            //           USING #Pet AS [Source]
            //           ON [Target].Id = [Source].Id
            //           WHEN MATCHED THEN UPDATE SET {DataMigrationHelper.GetUpdateColumnClause(updateColumnMappings)} 
            //           WHEN NOT MATCHED THEN INSERT {DataMigrationHelper.GetInsertColumnClause(insertColumnMappings)};";
            //       string emptyTableCommandText = $@"SELECT * INTO [{tempTableName}] FROM (SELECT *, CAST('{providerGroupId}' AS UNIQUEIDENTIFIER) as ProviderGroupId  FROM [Pets]) a;";

            //       string selectCommandText = $@"
            //           SELECT a.*, CAST('{providerGroupId}' AS UNIQUEIDENTIFIER) as ProviderGroupId FROM [Pets] a WHERE a.Id IN
            //               (SELECT DISTINCT p.Id FROM [Pets] p
            //               JOIN [PetProviderDetails] pp ON p.Id = pp.Pet_Id
            //               JOIN [Providers] pr ON pp.Provider_Id = pr.Id
            //               JOIN [Owners] o ON o.Id = p.Owner_Id
            //               WHERE p.Owner_Id IS NOT NULL AND pr.ProviderGroupId = '{providerGroupId}')";

            //       string deletePetsWithoutOwners = @"DELETE #Pet
            //FROM #Pet i
            //LEFT JOIN [Owner] o ON i.Owner_ID = o.Id
            //WHERE o.Id IS NULL";

            //       string testImport = "SELECT * INTO PetImport FROM #Pet";

            //       List<string> commandList = new List<string>();
            //       commandList.Add(deletePetsWithoutOwners);
            //       commandList.Add(mergeCommand);

            //       DataMigrationHelper.LoadData(sourceDbConnection, targetDbConnection, emptyTableCommandText, selectCommandText, tempTableName, commandList);

            this.LoadPet(wagAPIGroupList, wagProviderGroupId);
            // this.LoadPet(dogtopiaAPIGroupList, dogtopiaProviderGroupId);
             //this.LoadPet(nvaAPIGroupList, nvaProviderGroupId);

        }
        

        public void LoadPet(List<Guid> apiGroupList, Guid providerGroupId)
        {
            string tempTableName = "#Pet";
            string sourceDbConnection = "SourceDb";
            string targetDbConnection = "DataContext";

            string predicate = $@"pr.APIGroupId IN ({apiGroupList.AsString(x => $"'{x}'", ", ")})";
            //Guid providerGroupId = new Guid("284A43DD-F676-41E4-A713-EBC848CF85DB"); // Wag = '284A43DD-F676-41E4-A713-EBC848CF85DB'

            string columnMappings = $"Name,DOB|BirthDate,Weight,BreedId,ProviderGroupId|'{providerGroupId}',OwnerId|Owner_Id,ImportDate|(GETUTCDATE()),Created|Created";
            string insertColumnMappings = columnMappings + $",Id,CreatedById|('{BootstrapUser.UserId}'),Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";
            string updateColumnMappings = columnMappings + $",Updated|(GETUTCDATE()),UpdatedById|('{BootstrapUser.UserId}'),MessageId|(NewId()),MessageType|('SQL Import')";

            // Step 1. Merge
            string mergeCommand = $@"MERGE [Pet] AS [Target]
                USING #Pet AS [Source]
                ON [Target].Id = [Source].Id
                WHEN MATCHED THEN UPDATE SET {DataMigrationHelper.GetUpdateColumnClause(updateColumnMappings)} 
                WHEN NOT MATCHED THEN INSERT {DataMigrationHelper.GetInsertColumnClause(insertColumnMappings)};";
            string emptyTableCommandText = $@"SELECT * INTO [{tempTableName}] FROM (SELECT *, CAST('{providerGroupId}' AS UNIQUEIDENTIFIER) as ProviderGroupId  FROM [Pets]) a;";

            string selectCommandText = $@"
                SELECT a.*, CAST('{providerGroupId}' AS UNIQUEIDENTIFIER) as ProviderGroupId FROM [Pets] a WHERE a.Id IN
                    (SELECT DISTINCT p.Id FROM [Pets] p
                    JOIN [PetProviderDetails] pp ON p.Id = pp.Pet_Id
                    JOIN [Providers] pr ON pp.Provider_Id = pr.Id
                    JOIN [Owners] o ON o.Id = p.Owner_Id
                    WHERE p.Owner_Id IS NOT NULL AND {predicate})";

            string deletePetsWithoutOwners = @"DELETE #Pet
					FROM #Pet i
					LEFT JOIN [Owner] o ON i.Owner_ID = o.Id
					WHERE o.Id IS NULL";

            string testImport = "SELECT * INTO PetImport FROM #Pet";

            List<string> commandList = new List<string>();
            commandList.Add(deletePetsWithoutOwners);
            commandList.Add(mergeCommand);

            DataMigrationHelper.LoadData(sourceDbConnection, targetDbConnection, emptyTableCommandText, selectCommandText, tempTableName, commandList);

        }


        
    }
}
