using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Paw.Services.Attributes;

namespace Paw.Services.Util
{
    public static class NodeExtensions
    {
        public static void SetParentId<T>(this T @this, Guid? parentId = null) where T : IParentId
        {
            if (@this.ParentId != null && parentId == null)
            {
                // do nothing ... don't set to null
            }
            else
            {
                @this.ParentId = parentId;
            }

            foreach (PropertyInfo propertyInfo in @this.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType.GetInterfaces().Contains(typeof(IParentId)))
                {
                    IParentId property = propertyInfo.GetValue(@this) as IParentId;
                    if (property != null)
                    {
                        property.SetParentId(@this.Id);
                    }
                }

                if (propertyInfo.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    foreach (var item in propertyInfo.GetValue(@this) as IEnumerable)
                    {
                        IParentId child = item as IParentId;
                        if (child != null)
                        {
                            child.SetParentId(@this.Id);
                        }
                    }
                }
            }
        }

        public static T Get<T>(this T @this, Guid id) where T : class, INode<T>, IId, IParentId
        {
            if (@this.Id == id)
            {
                return @this;
            }

            foreach (T child in @this.ChildCollection)
            {
                T result = Get(child, id);
                if (result != null)
                {
                    return @this;
                }
            }

            return null;
        }

        public static void SetForeignKey<T>(this T @this, Guid? id = null) where T : IId
        {
            // Step 1. get a property with the SetForeignKeyAttribute
            foreach (PropertyInfo propertyInfo in @this.GetType().GetProperties())
            {
                // Set value
                if (propertyInfo.GetCustomAttribute<SetForeignKeyAttribute>() != null)
                {
                    // Set value
                    propertyInfo.SetValue(@this, @this.Id);
                }

                // Step in 1:1
                if (propertyInfo.PropertyType.GetInterfaces().Contains(typeof(IId)))
                {
                    IId child = propertyInfo.GetValue(@this) as IId;
                    if (child != null)
                    {
                        child.SetForeignKey(@this.Id);
                    }
                }

                // Step into collection 1:N
                if (propertyInfo.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    foreach (var item in propertyInfo.GetValue(@this) as IEnumerable)
                    {
                        IId child = item as IId;
                        if (child != null)
                        {
                            child.SetForeignKey(@this.Id);
                        }
                    }
                }
            }
        }
    }
}
