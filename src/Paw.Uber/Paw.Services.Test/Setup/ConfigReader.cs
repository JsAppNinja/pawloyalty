using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace Paw.Services.Test.Setup
{
    public class ConfigReader
    {
        public static List<T> GetItemList<T>(string directory) where T : class, new()
        {
            List<T> result = new List<T>();

            Type type = typeof(T);

            // Step 1. Get filename
            string fileName = Path.Combine(directory, type.Name + ".txt");

            // Step 2. readlines
            List<PropertyInfo> propertyInfoList = null;
            foreach (string line in File.ReadLines(fileName))
            {
                // Step 1. Skip 
                if (line.StartsWith("--") || line.StartsWith(" "))
                {
                    continue;
                }

                // Step 2. Get property list
                if (propertyInfoList == null)
                {
                    propertyInfoList = new List<PropertyInfo>();
                    foreach (string columnName in line.Split(new string[] { "|" }, StringSplitOptions.None))
                    {
                        PropertyInfo propertyInfo = type.GetProperty(columnName);

                        if(propertyInfo == null)
                        {
                            throw new Exception($"Property [{columnName.Trim()}] is not valid for type [{type.Name}].");
                        }
                    }
                }

                // Step 3. Token list
                List<string> valueList = line.Split(new string[] { "|" }, StringSplitOptions.None).ToList().ConvertAll( x => x.Trim());

                // Step 4. New Item
                T item = new T();

                // Step 5. Get Property values
                for (int i = 0; i < propertyInfoList.Count; i++)
                {
                    propertyInfoList[i].SetValue(item, valueList[i]);
                }

                result.Add(item);
            }

            return result;
        }
    }
}
