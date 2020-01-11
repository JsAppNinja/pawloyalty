using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Web.Mvc;

namespace Paw.Services.Attributes
{
    public static class AttributeHelper
    {
        public static List<IAddViewData> GetAddViewDataList(Type type)
        {
            List<IAddViewData> result = new List<IAddViewData>();

            foreach (var attribute in type.GetCustomAttributes())
            {
                IAddViewData addViewData = attribute as IAddViewData;
                if (addViewData != null) result.Add(addViewData);
            }

            foreach (var propertyInfo in type.GetProperties())
            {
                foreach (var attribute in propertyInfo.GetCustomAttributes())
                {
                    IAddViewData addViewData = attribute as IAddViewData;
                    if (addViewData != null)
                    {
                        IPropertyInfoInit propertyInfoInit = addViewData as IPropertyInfoInit;
                        if (propertyInfoInit != null)
                            propertyInfoInit.Init(propertyInfo);

                        result.Add(addViewData);
                    }
                }
            }

            return result;
        }

        public static bool HasPropertyAttribute<T>(PropertyInfo propertyInfo) where T : Attribute
        {
            return propertyInfo.GetCustomAttribute<T>() != null;
        }

        public static List<T> GetPropertyAttributeList<T>(PropertyInfo propertyInfo) where T : class 
        {
            List<T> result = new List<T>();
            foreach (var attribute in propertyInfo.GetCustomAttributes())
            {
                T targetType = attribute as T;
                if (targetType != null)
                {
                    IPropertyInfoInit propertyInfoInit = targetType as IPropertyInfoInit;
                    if (propertyInfoInit != null)
                        propertyInfoInit.Init(propertyInfo);

                    result.Add(targetType);
                }
            }

            return result;
        }

        public static T GetAttribute<T>(Type type) where T : class
        {
            foreach (var attribute in type.GetCustomAttributes())
            {
                T item = attribute as T;
                if (item != null)
                {
                    return item;
                }
            }

            return null;
        }

        public static List<T> GetAttributeList<T>(Type type) where T : class
        { 
            List<T> result = new List<T>();

            foreach (var attribute in type.GetCustomAttributes())
            {
                T item = attribute as T;
                if (item != null)
                {
                    result.Add(item);
                }
            }

            foreach (var propertyInfo in type.GetProperties())
            {
                foreach (var attribute in propertyInfo.GetCustomAttributes())
                {
                    T item = attribute as T;
                    if (item != null)
                    {
                        IPropertyInfoInit propertyInfoInit = item as IPropertyInfoInit;
                        if (propertyInfoInit != null)
                            propertyInfoInit.Init(propertyInfo);

                        result.Add(item);
                    }
                }
            }

            return result;
        }

        public static List<T> GetPropertyAttributeList<T>(Type type) where T : class
        {
            List<T> result = new List<T>();
            if (type == null) return result;

            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                result.AddRange(GetPropertyAttributeList<T>(propertyInfo));
            }

            return result;
        }

        // TODO: Move this.
        public static void BindController(object model, ControllerBase controller, object item)
        {
            if (item == null) return;

            Type type = item.GetType();
            
            // Get the attributes
            List<IBindControllerValue> bindControllerValueList = GetPropertyAttributeList<IBindControllerValue>(type);

            // Get List
            foreach (var bindControllerValue in bindControllerValueList)
            {
                // bind value
                bindControllerValue.BindControllerValue(model, controller, item);
            }
        }
    }
}
