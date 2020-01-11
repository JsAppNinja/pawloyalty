using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Paw.Services.Util;
using System.IO;

namespace Paw.Services.Test.DataMigration
{
    public static class DataMigrationHelper
    {
        public static void Log(string message)
        {
            string pathName = @"C:\Users\dwbanks\Documents\Paw\Output\migration_log.txt";

            FileInfo fileInfo = new FileInfo(pathName);
            if (string.IsNullOrEmpty(pathName) || !fileInfo.Directory.Exists)
            {
                return;
            }
            
            using (StreamWriter fileStream = fileInfo.AppendText())
            {
                fileStream.WriteLine("----------------------------------------------------------");
                fileStream.WriteLine(string.Format("{0:H:mm:ss.fff} {1}", DateTime.Now, message));
               // fileStream.Flush();
                

            }
        }

        public static void Log(string connectionString, string tableName)
        {
            DataTable dataTable = GetDataTable(connectionString, $"SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"TABLE: {tableName}");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                sb.AppendLine($"{dataRow["TABLE_SCHEMA"]}.{dataRow["COLUMN_NAME"]} {dataRow["DATA_TYPE"]} nullable:{dataRow["IS_NULLABLE"]}");
            }

            Log(sb.ToString());
        }

        public static void Log(DataTable dataTable, int rows = 3)
        {
            // Add Schema List
            List<DataColumn> dataColumnList = new List<DataColumn>();
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                dataColumnList.Add(dataColumn);
            }

            // Get schema
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DataTable");
            foreach (DataColumn dataColumn in dataColumnList)
            {
                sb.AppendLine($"[{dataColumn.ColumnName}] allowNulls:{dataColumn.AllowDBNull} dataType:{dataColumn.DataType} maxLength:{dataColumn.MaxLength} ");
            }
            sb.AppendLine();

            // Header columns
            sb.AppendLine(dataColumnList.AsString(x => x.ColumnName, "|"));

            // Write rows
            int i = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                i++;
                if (i >= rows) break;

                sb.AppendLine(dataColumnList.AsString(x =>
                {
                    object value = dataRow[x.ColumnName];
                    if (value == null || value == DBNull.Value)
                    {
                        return "<NULL>";
                    }
                    return value.ToString();
                }, "|"));

            }
            sb.AppendLine();
            Log(sb.ToString());
        }

        public static void LoadData(string sourceDbConnection, string targetDbConnection, string emptyTableCommandText, string selectCommandText, string tempTableName, List<string> tranformAndLoadCommandList)
        {

            // Step 1. Get dataTable and scheama
            Log("Select Source Command:" + selectCommandText);
            DataTable dataTable = GetDataTable(sourceDbConnection, selectCommandText);
            Log(dataTable);

            string tempTableSchema = string.Empty;

            using (SqlConnection sqlConnection0 = new SqlConnection(GetConnectionString(sourceDbConnection)))
            {
                sqlConnection0.Open();

                // Step 1. Get temp table
                Log("Empty Table Command (SourceDb):" + (emptyTableCommandText));
                ExecuteNonQuery(sqlConnection0, emptyTableCommandText);
                Log(sourceDbConnection, tempTableName);

                // Step 2. 
                tempTableSchema = GetCreateTempTableStatement(sqlConnection0, tempTableName, "Id");
                Log("Export Table Schema (SourceDb):" + (emptyTableCommandText));

            }
            
            // Step 2. Load
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString(targetDbConnection)))
            {
                // Step 1. Open connection
                sqlConnection.Open();

                // Step 2. Create temp table
                ExecuteNonQuery(sqlConnection, tempTableSchema);
                Log("TempTableSchema(TargetDb):" + tempTableSchema);

                // Step 3. Bulk upload
                BulkInsert(sqlConnection, dataTable, tempTableName);

                // Temp
                // DataTable temp = GetDataTable(sqlConnection, string.Format("SELECT * FROM {0}", tempTableName));

                // Step 4. Execute Transform and Load Commands
                if (tranformAndLoadCommandList != null)
                {
                    foreach (string commandText in tranformAndLoadCommandList)
                    {
                        using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
                        {
                            Log("SqlCommands:" + commandText);
                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public static void BulkInsert(SqlConnection sqlConnection, DataTable dataTable, string tableName)
        {
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection))
            {
                sqlBulkCopy.DestinationTableName = tableName;

                sqlBulkCopy.WriteToServer(dataTable);
            }
        }
        
        public static string GetConnectionString(string nameOrConnectionString)
        {
            if (nameOrConnectionString.ToLower().Contains("server=") && nameOrConnectionString.Contains(";"))
            {
                return nameOrConnectionString;
            }

            return ConfigurationManager.ConnectionStrings[nameOrConnectionString].ConnectionString;
        }

        public static DataTable GetDataTable(string nameOrConnectionString, string selectCommandText)
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString(nameOrConnectionString)))
            {
                sqlConnection.Open();
                return GetDataTable(sqlConnection, selectCommandText);
            }
        }

        public static DataTable GetDataTable(SqlConnection sqlConnection, string selectCommandText)
        {
            DataTable result = new DataTable();
            using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectCommandText, sqlConnection))
            {
                sqlDataAdapter.Fill(result);
            }
            return result;
        }

        public static int Merge(SqlConnection sqlConnection, string sourceCommandText, string tagetTable, string columnMapping)
        {
            throw new NotImplementedException();
        }

        public static int ExecuteNonQuery(SqlConnection sqlConnection, string commandText)
        {
            using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
            {
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public static int ExecuteNonQuery(string nameOrConnectionString, string commandText)
        {
            using (SqlConnection sqlConnection = new SqlConnection(GetConnectionString(nameOrConnectionString)))
            using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
            {
                sqlConnection.Open();
                return sqlCommand.ExecuteNonQuery();
            }
        }

        #region Sql ...

        public static List<List<string>> GetColumnExpressionList(string columnMappings) // Target|Source
        {
            List<string> mappingDefinitionList = columnMappings.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();

            List<List<string>> result = new List<List<string>>();

            foreach (string mappingDefinition in mappingDefinitionList)
            {
                List<string> expressionList = mappingDefinition.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (expressionList.Count == 1) // Add implicit column name
                {
                    expressionList.Add(expressionList[0]);
                }

                result.Add(expressionList);
            }

            return result;
        }

        public static string GetUpdateColumnClause(string columnMappings)
        {
            List<List<string>> columnExpressionList = GetColumnExpressionList(columnMappings);
            List<string> result = columnExpressionList.ConvertAll(x => string.Format("{0} = {1}", x[0].AddColumnOrExpression("Target"), x[1].AddColumnOrExpression("Source")));

            return result.AsString(x => x, ", ");
        }

        public static string GetInsertColumnClause(string columnMappings)
        {
            List<List<string>> columnExpressionList = GetColumnExpressionList(columnMappings);
            
            return string.Format("({0}) VALUES ({1})", 
                columnExpressionList.ConvertAll(x => string.Format("[{0}]", x[0])).AsString(x => x, ", "),
                columnExpressionList.ConvertAll(x => x[1]).AsString(x => x.AddColumnOrExpression("Source"), ", "));
        }

        public static string AddColumnOrExpression(this string input, string identifier)
        {
            if (input.StartsWith("'") || input.StartsWith("(")) // Is Expression
            {
                return input;
            }

            return $"[{identifier}].[{input}]";
        }

        public static string GetSqlUniqueidentifier(Guid input)
        {
            return string.Format("CAST('{0}' AS UNIQUEIDENTIFIER)", input);
        }

        #endregion

        #region Schema ...

        public static int CreateEmptyTable(SqlConnection sqlConnection, string newTableName, string selectCommand)
        {
            // Step 1. Create empty select statement
            string emptySelectCommand = selectCommand;

            if (emptySelectCommand.StartsWith("SELECT TOP 0", StringComparison.CurrentCultureIgnoreCase))
            { }
            else
            {
                emptySelectCommand = selectCommand.ToUpper().Replace("SELECT ", "SELECT TOP 0 ");
            }
            
            string commandText = string.Format("SELECT * INTO [{0}] FROM ({1}) a", newTableName, emptySelectCommand);
            using (SqlCommand sqlCommand = new SqlCommand(commandText, sqlConnection))
            {
                return sqlCommand.ExecuteNonQuery();
            }
        }

        public static string GetCreateTableStatement(SqlConnection sqlConnection, string tableName, string newTableName = "")
        {
            if (string.IsNullOrWhiteSpace(newTableName))
            {
                newTableName = tableName;
            }

            using (SqlCommand sqlCommand = new SqlCommand(GetCreateTableSql(), sqlConnection))
            {
                sqlCommand.Parameters.Add(new SqlParameter("@TableName", tableName));
                sqlCommand.Parameters.Add(new SqlParameter("@NewTableName", newTableName));

                return sqlCommand.ExecuteScalar() as string;
            }
        }

        public static string GetCreateTempTableStatement(SqlConnection sqlConnection, string tableName, string key)
        {
            using (SqlCommand sqlCommand = new SqlCommand(GetTempTableColumnDefinitions(), sqlConnection))
            {
                sqlCommand.Parameters.Add(new SqlParameter("@TableName", tableName));

                string columnDefinitions = sqlCommand.ExecuteScalar() as string;
                Log("Sql: " + columnDefinitions);

                return $"CREATE TABLE [{tableName}]([Id] uniqueidentifier NOT NULL, {columnDefinitions} CONSTRAINT PK__Temp__{Guid.NewGuid():n} PRIMARY KEY CLUSTERED( [{key}] ASC )) ";
            }
        }

        public static string GetCreateTempTableStatementV2(SqlConnection sqlConnection, string tableName, string key)
        {
            using (SqlCommand sqlCommand = new SqlCommand(GetTempTableColumnDefinitions(), sqlConnection))
            {
                sqlCommand.Parameters.Add(new SqlParameter("@TableName", tableName));

                string columnDefinitions = sqlCommand.ExecuteScalar() as string;

                return $"CREATE TABLE [{tableName}]({columnDefinitions} CONSTRAINT PK__Temp__{Guid.NewGuid():n} PRIMARY KEY CLUSTERED( [{key}] ASC )) ";
            }
        }

        private static string GetCreateTableSql()
        {
            return $@"select  'create table [' + @NewTableName + '] (' + o.list + ')' + CASE WHEN tc.Constraint_Name IS NULL THEN '' ELSE 'ALTER TABLE [' + @NewTableName + '] ADD CONSTRAINT PK_' + @NewTableName  + ' PRIMARY KEY ' + ' (' + LEFT(j.List, Len(j.List)-1) + ')' END
                from    sysobjects so
                cross apply
                    (SELECT 
                        '  ['+column_name+'] ' + 
                        data_type + case data_type
                            when 'sql_variant' then ''
                            when 'text' then ''
                            when 'ntext' then ''
                            when 'xml' then ''
                            when 'decimal' then '(' + cast(numeric_precision as varchar) + ', ' + cast(numeric_scale as varchar) + ')'
                            else coalesce('('+case when character_maximum_length = -1 then 'MAX' else cast(character_maximum_length as varchar) end +')','') end + ' ' +
                        case when exists ( 
                        select id from syscolumns
                        where object_name(id)=so.name
                        and name=column_name
                        and columnproperty(id,name,'IsIdentity') = 1 
                        ) then
                        'IDENTITY(' + 
                        cast(ident_seed(so.name) as varchar) + ',' + 
                        cast(ident_incr(so.name) as varchar) + ')'
                        else ''
                        end + ' ' +
                         (case when IS_NULLABLE = 'No' then 'NOT ' else '' end ) + 'NULL ' + 
                          case when information_schema.columns.COLUMN_DEFAULT IS NOT NULL THEN 'DEFAULT '+ information_schema.columns.COLUMN_DEFAULT ELSE '' END + ', ' 

                     from information_schema.columns where table_name = so.name
                     order by ordinal_position
                    FOR XML PATH('')) o (list)
                left join
                    information_schema.table_constraints tc
                on  tc.Table_name       = so.Name
                AND tc.Constraint_Type  = 'PRIMARY KEY'
                cross apply
                    (select '[' + Column_Name + '], '
                     FROM   information_schema.key_column_usage kcu
                     WHERE  kcu.Constraint_Name = tc.Constraint_Name
                     ORDER BY
                        ORDINAL_POSITION
                     FOR XML PATH('')) j (list)
                where   xtype = 'U' AND so.Name = @TableName
                AND name    NOT IN ('dtproperties')";
        }

        private static string GetTempTableColumnDefinitions()
        {
            return @"SELECT
                '  [' + column_name + '] ' +
                data_type + case data_type
                    when 'sql_variant' then ''
                    when 'text' then ''
                    when 'ntext' then ''
                    when 'xml' then ''
                    when 'decimal' then '(' + cast(numeric_precision as varchar) + ', ' + cast(numeric_scale as varchar) + ')'
                    else coalesce('(' +case when character_maximum_length = -1 then 'MAX' else cast(character_maximum_length as varchar) end + ')', '') end + ' ' +
                           +' ' +
                       (case when IS_NULLABLE = 'No' then 'NOT ' else '' end ) +'NULL ' +
                    case when tempdb.information_schema.columns.COLUMN_DEFAULT IS NOT NULL THEN 'DEFAULT ' + tempdb.information_schema.columns.COLUMN_DEFAULT ELSE '' END + ', '

                from tempdb.information_schema.columns where CHARINDEX('___', table_name) > 0 AND SUBSTRING(table_name, 1, CHARINDEX('___', table_name) - 1) = @TableName
                AND column_name <> 'Id'
                order by ordinal_position
                FOR XML PATH('')";
        } // NOTE: AND column_name <> 'Id' is added

        #endregion

        /*
        public static DataTable GetDataTable(SqlConnection sqlConnection, string dataName)
        {
            DataTable result = null;
        }


        protected void BulkInsertTempTable(SqlConnection sqlConnection, ITableMap tableMapping, bool append = false)
        {
            // Step 1. Create temp table
            if (!append)
            {
                this.ExecuteSql(sqlConnection, string.Format("SELECT * INTO {1} FROM (SELECT TOP 0 * FROM {0}) a", tableMapping.TableName, tableMapping.GetTempTableName()));
            }
            // Step 2. Bulk Insert
            this.BulkInsert(sqlConnection, tableMapping.DataTable, tableMapping.GetTempTableName());
        }

        protected void RemapDuplicateRelationships(SqlConnection sqlConnection, string pkTableName, List<string> uniqueColumnList, string fkTableName, string fkColumnName)
        {
            string commandText = string.Format(@"UPDATE a SET a.[{0}] = b.NewId
			FROM [{1}] a
				JOIN (SELECT c.Id as OldId, d.NewId
					FROM [{2}] c
						JOIN (SELECT MAX([Id]) as NewId, {3} FROM {2} GROUP BY {3}) d ON {4}) b ON a.[{0}] = b.[OldId]", // Grabs the last PK Row by Id, relying on RemoveDuplicates method to retain last PK Row
                fkColumnName, // a
                fkTableName,
                pkTableName, // b
                uniqueColumnList.AsString(x => x, ", "),
                uniqueColumnList.AsString(x => string.Format("c.[{0}] = d.[{0}]", x), " AND "));

            int result = this.ExecuteSql(sqlConnection, commandText);
        }

        protected void RemoveDuplicates(SqlConnection sqlConnection, string tableName, List<string> uniqueColumnList, string orderByClause)
        {
            //string commandText = string.Format(@"DELETE FROM {0} WHERE [Id] NOT IN (SELECT MAX([Id]) as Id FROM {0} GROUP BY {1}) ",
            //        tableName,
            //        uniqueColumnList.AsString(x => x, " ,"));

            string commandText = string.Format(@"WITH cte AS (
					  SELECT {0}, 
						 row_number() OVER(PARTITION BY {0} ORDER BY {1}) AS [rn]
					  FROM {2}
					)
					DELETE cte WHERE[rn] > 1", // ORDER BY Should be DESC so the hightes PK is kept
                    uniqueColumnList.AsString(x => string.Format("[{0}]", x), ", "),
                    orderByClause,
                    tableName);

            this.ExecuteSql(sqlConnection, commandText);
        }

        protected static List<SqlParameter> GetSqlParameterList(object item)
        {
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();

            if (item == null)
            {
                return sqlParameterList;
            }

            foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
            {
                object value = propertyInfo.GetValue(item);
                if (value == null)
                {
                    value = DBNull.Value;
                }

                sqlParameterList.Add(new SqlParameter(string.Format("@{0}", propertyInfo.Name), value));
            }

            return sqlParameterList;
        }

        protected string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[this.ConnectionStringName].ConnectionString;
        }
        */
    }
}
