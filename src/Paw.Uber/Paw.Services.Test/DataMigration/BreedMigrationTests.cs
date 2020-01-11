using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Paw.Services.Util;

namespace Paw.Services.Test.DataMigration
{
    [TestClass]
    public class BreedMigrationTests
    {
        
        [TestMethod]
        public void LoadBreedsTest()
        {
            // nva
            this.LoadBreeds();
            
        }

        public void LoadBreeds()
        {
            string tempTableName = "#Breed";
            string sourceDbConnection = "SourceDb";
            string targetDbConnection = "DataContext";
            
            string selectCommandText = $@"SELECT Id, Type, Name, AverageWeight, Country, Coat, Pattern FROM [Breeds]";
            string emptyTableCommandText = $@"SELECT * INTO [{tempTableName}] FROM (SELECT Id, Type, Name, AverageWeight, Country, Coat, Pattern FROM [Breeds]) a";


            string columnMappings = $"Type,Name,AverageWeight,Country,Coat,Pattern";
            string insertColumnMappings = columnMappings + $",Id";
            string updateColumnMappings = columnMappings;

            // Step 1. Merge
            string mergeCommand = $@"
                SET ANSI_WARNINGS OFF
                MERGE [Breed] AS [Target]
                USING #Breed AS [Source]
                ON [Target].Id = [Source].Id
                WHEN MATCHED THEN UPDATE SET {DataMigrationHelper.GetUpdateColumnClause(updateColumnMappings)} 
                WHEN NOT MATCHED THEN INSERT {DataMigrationHelper.GetInsertColumnClause(insertColumnMappings)};";

            List<string> commandList = new List<string>();
            commandList.Add(mergeCommand);
            
            DataMigrationHelper.LoadData(sourceDbConnection, targetDbConnection, emptyTableCommandText, selectCommandText, tempTableName, commandList);

        }
    }
}
