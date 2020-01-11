using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Util;

namespace Paw.Services.Test.DataMigration
{
    [TestClass]
    public class EmployeeMigrationTests
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
        public void LoadEmployeesTest()
        {
            // nva
            this.LoadEmployees(nvaAPIGroupList);

            // dogtopia
            this.LoadEmployees(dogtopiaAPIGroupList);

            // wag
            this.LoadEmployees(wagAPIGroupList);
        }

        public void LoadEmployees(List<Guid> apiGroupList)
        {
            string tempTableName = "#Employee";
            string sourceDbConnection = "SourceDb";
            string targetDbConnection = "DataContext";
            
            string selectCommandText = $@"SELECT
                e.Id,
                REPLACE(COALESCE(
	                CASE WHEN CHARINDEX(' ', e.[Name]) = 0 THEN e.[Name] ELSE NULL END,
	                SUBSTRING(e.[Name], 1, CHARINDEX(' ', e.[Name]))
	                ), '-removed', '') AS FirstName,
                REPLACE(COALESCE(
	                CASE WHEN CHARINDEX(' ', e.[Name]) = 0 THEN '' ELSE NULL END,
	                SUBSTRING(e.[Name], CHARINDEX(' ', e.[Name]) + 1, 1000)
	                ), '-removed', '') AS LastName,
                ProviderId, 
                Termination as TerminationDate,
                e.EmailAddress as Email
                FROM [Employees] e JOIN Providers p
                    ON e.ProviderId = p.Id
                    WHERE p.APIGroupId IN ({apiGroupList.AsString(x => $"'{x}'", ", ")})";

            string columnMappings = $"FirstName,LastName,ProviderId,TerminationDate,Email";
            string insertColumnMappings = columnMappings + $",Id";
            string updateColumnMappings = columnMappings;

            string mergeCommand = $@"MERGE [Employee] AS [Target]
                USING #Employee AS [Source]
                ON [Target].Id = [Source].Id
                WHEN MATCHED THEN UPDATE SET {DataMigrationHelper.GetUpdateColumnClause(updateColumnMappings)} 
                WHEN NOT MATCHED THEN INSERT {DataMigrationHelper.GetInsertColumnClause(insertColumnMappings)};";

            string emptyTableCommandText = $@"SELECT * INTO [{tempTableName}] FROM (SELECT Id, Name as FirstName, Name as LastName, ProviderId, Termination as TerminationDate, SUBSTRING(Name, 1, 4) AS Initials, EmailAddress as Email FROM [Employees]) a;";

            List<string> commandList = new List<string>();
            commandList.Add(mergeCommand);

            DataMigrationHelper.LoadData(sourceDbConnection, targetDbConnection, emptyTableCommandText, selectCommandText, tempTableName, commandList);
        }
    }
}
