using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Test.DataMigration
{
    public class MigrationManagerV2
    {
        public static void UpsertResourceList(string sourceDbConnection, string targetDbConnection, Guid providerId, Guid? skuCategoryId)
        {
            // Step 1. Temp Table Name
            string tempTableName = "#Resource";
            //string sourceDbConnection = "SourceDb";
            //string targetDbConnection = "DataContext";

            // Step[ 2. Column Mappings target|source
            string columnMappings = $"LegacyId,ShortDescription|Name,Key,Description,IsShared,Capacity,MaxPetWeight,ProviderId|Provider_Id,SkuCategoryId|('{skuCategoryId}')";
            string insertColumnMappings = columnMappings;
            string updateColumnMappings = columnMappings;

            // Step 3. Merge statement
            string mergeCommand = $@"MERGE [Resource] AS [Target]
                USING {tempTableName} AS [Source]
                ON [Target].LegacyId = [Source].LegacyId
                WHEN MATCHED THEN UPDATE SET {DataMigrationHelper.GetUpdateColumnClause(updateColumnMappings)} 
                WHEN NOT MATCHED THEN INSERT {DataMigrationHelper.GetInsertColumnClause(insertColumnMappings + ",Id")};";
            
            // Step 4. Select from source db
            string selectCommandText = $@"SELECT NEWID() as Id, Id as LegacyId, Substring([Name], 0, 20) as [Name], [Key], [Description], IsShared, [Capacity], MaxPetWeight, CAST('{providerId}' AS UNIQUEIDENTIFIER) as Provider_Id FROM Resources WHERE Provider_Id = '{providerId}'";

            // Step 5. Create Temp Table Statement
            string emptyTableCommandText = $@"SELECT * INTO [{tempTableName}] FROM ({selectCommandText}) a; ALTER TABLE [{tempTableName}] ALTER COLUMN Id uniqueidentifier NOT NULL;";

            // Step 6. Update ids
            //string updateTempTableIds = $@" UPDATE t SET t.Id = r.Id FROM [{tempTableName}] t INNER JOIN [Resource] r ON t.LegacyId = r.Legacyid WHERE r.ProviderId = ('{providerId}') AND r.ID IS NOT NULL";
            
            List<string> commandList = new List<string>();
            //commandList.Add(updateTempTableIds);
            commandList.Add(mergeCommand);

            DataMigrationHelper.LoadData(sourceDbConnection, targetDbConnection, emptyTableCommandText, selectCommandText, tempTableName, commandList);
        }
    }
}
