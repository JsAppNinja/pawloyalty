using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace Paw.Services.Test.Setup
{
    public class ConfigLoader
    {
        
        public static int Load<T>(Guid userId, Guid providerId, string fileName) where T : class, IProviderId, IExternalId, new()
        {
            using (DataContext dataContext = DataContext.Create(userId))
            {
                // Step 1. Read File
                List<T> itemList = ReadFile<T>(dataContext, fileName, externalId => {
                    
                    // Create new item if externalId is empty
                    if (string.IsNullOrWhiteSpace(externalId))
                    {
                        T item0 = new T() { ProviderId = providerId, ExternalId = externalId };
                        dataContext.Set<T>().Add(item0);
                        return item0;
                    }

                    // Try get existing item
                    T item1 = dataContext.Set<T>().Where(x => x.ExternalId == externalId).SingleOrDefault();
                    if (item1 == null)
                    {
                        T item2 = new T() { ProviderId = providerId, ExternalId = externalId };
                        dataContext.Set<T>().Add(item2);
                        return item2;
                    }

                    return item1;

                });

                dataContext.Set<T>().AddOrUpdate(
                    itemList.ToArray()
                    );

                return dataContext.SaveChanges();
            }
        }

        public static Dictionary<string, Guid> GetExternalIdDirectory(string typeName)
        {
            Dictionary<string, Guid> result = new Dictionary<string, Guid>();
            using (DataContext dataContext = DataContext.Create())
            {
                foreach (var item in dataContext.Database.SqlQuery<Lookup>($"SELECT Id, ExternalId FROM [{typeName}]").ToList())
                {
                    if (string.IsNullOrWhiteSpace(item.ExternalId))
                    {
                        continue;
                    }

                    result.Add(item.ExternalId.Trim(), item.Id); 
                }

                return result;
            }
        }

        public static List<T> ReadFile<T>(DataContext dataContext, string fileName, Func<string, T> intialize) where T : class, IProviderId, IExternalId, new()
        {
            // Step 1.
            List<T> result = new List<T>();

            Type type = typeof(T);
            
            // Step 2. readlines
            List<PropertyInfoSetter> propertySetterList = null;
            foreach (string line in File.ReadLines(fileName))
            {
                // Step 1. Skip 
                if (line.StartsWith("--") || line.StartsWith(" ") || string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                // Step 2. Get property setters
                if (propertySetterList == null)
                {
                    propertySetterList = new List<PropertyInfoSetter>();
                    foreach (string columnName in line.Split(new string[] { "|" }, StringSplitOptions.None))
                    {
                        if (columnName.Contains(".")) // Add ExternalLookup
                        {
                            List<string> tokens = columnName.Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries).ToList();

                            // ie. SkuCategory.ExternalId => SkuCategoryId
                            propertySetterList.Add(new LookupSetter() { PropertyInfo = type.GetProperty(tokens[0] + "Id"), ExternalIdLookup = GetExternalIdDirectory(tokens[0]), TypeName = tokens[0]});
                        }
                        else // Property+Info
                        {
                            PropertyInfo propertyInfo = type.GetProperty(columnName);

                            if (propertyInfo == null)
                            {
                                throw new Exception($"Property [{columnName.Trim()}] is not valid for type [{type.Name}].");
                            }

                            propertySetterList.Add(new PropertyInfoSetter() { PropertyInfo = propertyInfo });
                        }
                    }



                    continue;
                }

                // Step 3. Token list & trim
                List<string> valueList = line.Split(new string[] { "|" }, StringSplitOptions.None).ToList().ConvertAll(x => x.Trim());

                // Step 4. New Item
                T item = intialize(valueList[0]);

                // Step 5. Get Property values
                for (int i = 0; i < propertySetterList.Count; i++)
                {
                    propertySetterList[i].SetValue(item, valueList[i]);
                }

                result.Add(item);
            }

            // Step 3. 

            return result;
        }

        public class PropertyInfoSetter
        {
            public PropertyInfo PropertyInfo
            {
                get { return _PropertyInfo; }
                set { _PropertyInfo = value; }
            }
            private PropertyInfo _PropertyInfo = null;

            public virtual void SetValue(object item, string value)
            {
                if (this.PropertyInfo.PropertyType == typeof(bool))
                {
                    if (value == "0")
                    {
                        value = "false";
                    }
                    else if (value == "1")
                    {
                        value = "true";
                    }

                    bool boolValue;
                    if (bool.TryParse(value, out boolValue))
                    {
                        this.PropertyInfo.SetValue(item, boolValue);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Invalid property [{item.GetType().Name}.{this.PropertyInfo.Name}] value [{value}]");
                    }
                }
                else if (this.PropertyInfo.PropertyType == typeof(decimal?) || this.PropertyInfo.PropertyType == typeof(decimal))
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        this.PropertyInfo.SetValue(item, (decimal?)null);
                    }
                    else
                    {
                        Decimal decimalValue;
                        if (decimal.TryParse(value, out decimalValue))
                        {
                            this.PropertyInfo.SetValue(item, decimalValue);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Invalid property [{item.GetType().Name}.{this.PropertyInfo.Name}] value [{value}]");
                        }
                    }
                }
                else if (this.PropertyInfo.PropertyType == typeof(int?) || this.PropertyInfo.PropertyType == typeof(int))
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        this.PropertyInfo.SetValue(item, (int?)null);
                    }
                    else
                    {
                        int intValue;
                        if (int.TryParse(value, out intValue))
                        {
                            this.PropertyInfo.SetValue(item, intValue);
                        }
                        else
                        {
                            throw new InvalidOperationException($"Invalid property [{item.GetType().Name}.{this.PropertyInfo.Name}] value [{value}]");
                        }
                    }
                }
                else
                {
                    this.PropertyInfo.SetValue(item, value);
                }
            }
        }

        public class LookupSetter : PropertyInfoSetter
        {
            public string TypeName
            {
                get { return _TypeName; }
                set { _TypeName = value; }
            }
            private string _TypeName = String.Empty;


            public Dictionary<string, Guid> ExternalIdLookup
            {
                get { return _ExternalIdLookup; }
                set { _ExternalIdLookup = value; }
            }
            private Dictionary<string, Guid> _ExternalIdLookup = new Dictionary<string, Guid>();

            public override void SetValue(object item, string value)
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    // Do nothing
                    return;
                }

                if (!this.ExternalIdLookup.ContainsKey(value))
                {
                    throw new InvalidOperationException($"Unable to find ExternalId [{value}] for type [{this.TypeName}]");
                }

                this.PropertyInfo.SetValue(item, this.ExternalIdLookup[value]);
            }
        }

        public class Lookup
        {
            public string ExternalId
            {
                get { return _ExternalId; }
                set { _ExternalId = value; }
            }
            private string _ExternalId = string.Empty;

            public Guid Id
            {
                get { return _Id; }
                set { _Id = value; }
            }
            private Guid _Id = Guid.Empty;


        }
    }
}
