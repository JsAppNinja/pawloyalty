using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Util
{
    public class VersionHelper
    {
        // Step 1. Create the columns and mappings

        // Step 2. if relationship is versioned, get the relationship versionId

        private static string DropTriggerSql(string table)
        {
            string trigger = string.Format(@"IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'InsertVersion{0}Trigger' AND [type] = 'TR')
                                    BEGIN
                                    DROP TRIGGER [InsertVersion{0}Trigger];
                                END; 
                                IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'UpdateVersion{0}Trigger' AND [type] = 'TR')
                                    BEGIN
                                    DROP TRIGGER [UpdateVersion{0}Trigger];
                                END; 
                                IF EXISTS (SELECT * FROM sys.objects WHERE [name] = N'DeleteVersion{0}Trigger' AND [type] = 'TR')
                                    BEGIN
                                    DROP TRIGGER [DeleteVersion{0}Trigger];
                                END; ");

            return trigger;
        }

        private static string DropTriggerSql(Type type)
        {
            return DropTriggerSql(type.Name);
        }

        public static string UpdateTriggerSql(Type version)
        {
            // Step 0. Get source type
            Type source = version
                .GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IVersion<>))
                .SelectMany(i => i.GetGenericArguments())
                .FirstOrDefault();

            // Step 1. Get properties
            List<PropertyInfo> versionPropertyList =  version.GetProperties().ToList();
            List<PropertyInfo> sourcePropertyList = source.GetProperties().ToList();

            // Step 2. Sort version properties
            versionPropertyList.Sort((x, y) => x.Name.CompareTo(y.Name));

            // Step 3. Get the property list
            List<PropertyInfo> mappedPropertyList = versionPropertyList.FindAll(x => {

                // Step 1. Filter non direct or Original_* matches
                string versionPropertyName = GetUnderlyingColumnName(x);

                if (!sourcePropertyList.Any(y => y.Name == versionPropertyName)) return false;

                // Step 2. Drop Id
                if (versionPropertyName == "Id") return false;
                
                return true;
            });

            // step 4. Get column list
            string versionColumnList = versionPropertyList.AsString(x => string.Format("[{0}]", x.Name), ",\r\n");
            string sourceColumnList = versionPropertyList.AsString(x => string.Format("{0}", GetSourceColumn(x)), ",\r\n");

            // step 5. Create the sql
            string insertSql = string.Format(@"IF EXISTS(SELECT* FROM sys.objects WHERE [name] = N'InsertVersion{0}Trigger' AND [type] = 'TR')
                                BEGIN
                                DROP TRIGGER [InsertVersion{0}Trigger];
                            END;
                            
                            GO;
                            
                            CREATE TRIGGER [InsertVersion{0}Trigger] ON [{0}]
                            FOR AFTER INSERT
                            AS
                            BEGIN

                                INSERT [{0}Version] ( 
                                [Id],
                                [Version_CurrentId],
                                [Version_Operation],
                                {1})
                                SELECT 
                                i.Version_Id,
                                i.Id,
                                'INSERT',
                                {2} 
                                FROM Inserted i
                                    INNER JOIN Deleted d ON i.Id = d.Id
                                    INNER JOIN [{0}] s ON i.Id = s.Id

                            END;

                            GO", source.Name, versionColumnList, sourceColumnList);

            string updateSql = string.Format(@"IF EXISTS(SELECT* FROM sys.objects WHERE [name] = N'UpdateVersion{0}Trigger' AND [type] = 'TR')
                                BEGIN
                                DROP TRIGGER [UpdateVersion{0}Trigger];
                            END;
                            
                            GO;
                            
                            CREATE TRIGGER [UpdateVersion{0}Trigger] ON [{0}]
                            FOR UPDATE
                            AS
                            BEGIN

                                INSERT [{0}Version] ( 
                                [Id],
                                [Version_CurrentId],
                                [Version_Operation],
                                {1})
                                SELECT 
                                i.Version_Id,
                                i.Id,
                                'UPDATE',
                                {2} 
                                FROM Inserted i
                                    INNER JOIN Deleted d ON i.Id = d.Id
                                    INNER JOIN [{0}] s ON i.Id = s.Id

                            END;

                            GO", source.Name, versionColumnList, sourceColumnList);

            string deleteSql = string.Format(@"IF EXISTS(SELECT* FROM sys.objects WHERE [name] = N'DeleteVersion{0}Trigger' AND [type] = 'TR')
                                BEGIN
                                DROP TRIGGER [DeleteVersion{0}Trigger];
                            END;
                            
                            GO;
                            
                            CREATE TRIGGER [DeleteVersion{0}Trigger] ON [{0}]
                            FOR DELETE
                            AS
                            BEGIN

                                INSERT [{0}Version] ( 
                                [Id],
                                [Version_CurrentId],
                                [Version_Operation],
                                {1})
                                SELECT 
                                i.Version_Id,
                                i.Id,
                                'DELETE',
                                {2} 
                                FROM Deleted d 
                                    INNER JOIN [{0}] s ON i.Id = s.Id

                            END;

                            GO;", source.Name, versionColumnList, sourceColumnList);

            return string.Format("{0}\r\n {1}\r\n {2}\r\n", insertSql, updateSql, deleteSql);
        }

        private static string GetUnderlyingColumnName(PropertyInfo propertyInfo)
        {
            string result = propertyInfo.Name;
            if (result.StartsWith("Original_", StringComparison.InvariantCultureIgnoreCase))
            {
                result = result.Substring(8);
            }
            return result;
        }

        private static string GetSourceColumn(PropertyInfo propertyInfo)
        {
            string result = propertyInfo.Name;
            if (result.StartsWith("Original_", StringComparison.InvariantCultureIgnoreCase))
            {
                result = "i." + result.Substring(8);
            }
            return "d." + result;
        }
    }
}
