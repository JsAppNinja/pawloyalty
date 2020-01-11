using Paw.Services.Attributes;
using Paw.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Paw.Services.Messages.Caching
{
    public static class CacheHelper2
    {
        #region Notify ...

        public static void Notify(object item, object message, CacheActionTypes cacheAction)
        {
            if (item == null)
            {
                return;
            }

            List<CacheAction> cacheActionList = new List<CacheAction>();

            foreach (CacheBehavior cacheBehavior in CacheConfig.CacheBehaviorList)
            {
                if (cacheBehavior.Type == item.GetType())
                {
                    cacheActionList.Add(cacheBehavior.CreateCacheAction(item, cacheAction));
                }
            }

            // TODO: Dispactch to new thread
            ProcessCacheActionList(cacheActionList);
        }

        public static void ProcessCacheActionList(List<CacheAction> cacheActionList)
        {
            foreach (CacheAction cacheAction in cacheActionList)
            {
                foreach (CacheBehavior cacheBehavior in CacheConfig.CacheBehaviorList)
                {
                    if (!cacheBehavior.TriggerTypeList.Any(x => x == cacheAction.SourceType))
                    {
                        continue;
                    }

                    if (cacheBehavior.ProcessCacheAction != null)
                    {
                        cacheBehavior.ProcessCacheAction(cacheBehavior, cacheAction);
                    }
                }
            }
        }

        #endregion

        public static bool AddCacheItem<T>(T item, object message) where T : class, IId
        {
            if (item == null)
            {
                return false;
            }

            // Step 1. Get CacheAttributes
            List<CacheAttribute> cacheAttributeList = AttributeHelper.GetAttributeList<CacheAttribute>(item.GetType());

            // Step 2. Execute CacheAttributes
            bool result = false;
            foreach (CacheAttribute cacheAttribute in cacheAttributeList)
            {
                // Step 1. Get cache Key
                string cacheKey = cacheAttribute.GetCacheKey(item);

                // Step 2. 
                result = result | cacheAttribute.AddItem<T>(item);
            }

            return result;

        }

        public static bool UpdateCacheItem(object item, object message)
        {
            if (item == null)
            {
                return false;
            }

            // Step 1. Get CacheAttributes
            List<CacheAttribute> cacheAttributeList = AttributeHelper.GetAttributeList<CacheAttribute>(item.GetType());

            // Step 2. Execute CacheAttributes
            bool result = false;
            foreach (CacheAttribute cacheAttribute in cacheAttributeList)
            {
                // Step 1. Get cache Key
                string cacheKey = cacheAttribute.GetCacheKey(item);

                // Step 2. 
                result = result | cacheAttribute.UpdateItem(item);
            }

            return result;

        }

        public static bool DeleteCacheItem(object item)
        {
            if (item == null)
            {
                return false;
            }

            // Step 1. Get CacheAttributes
            List<CacheAttribute> cacheAttributeList = AttributeHelper.GetAttributeList<CacheAttribute>(item.GetType());

            // Step 2. Execute CacheAttributes
            bool result = false;
            foreach (CacheAttribute cacheAttribute in cacheAttributeList)
            {
                // Step 1. Get cache Key
                string cacheKey = cacheAttribute.GetCacheKey(item);

                // Step 2. 
                result = result | cacheAttribute.DeleteItem(item);
            }

            return result;

        }

        #region Cache Events ...

        public static bool CacheAction<T>(T item, CacheActionTypes cacheAction)
        {
            string cacheKey = GetCacheKey<T>(item);



            return true;
        }

        #endregion

        #region Cache Access ...

        public static string GetCacheKey<T>(T item)
        {
            if (item == null)
            {
                return string.Empty;
            }

            // Step 1. Get CacheAttributes
            CacheAttribute cacheAttribute = AttributeHelper.GetAttribute<CacheAttribute>(item.GetType());

            if (cacheAttribute == null)
            {
                throw new NotImplementedException($"CacheAttribute not found for type [{item.GetType().Name}].");
            }

            return cacheAttribute.GetCacheKey(item);
        }

        public static Cache GetCache()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                return null;
            }

            return httpContext.Cache;
        }
        
        public static T GetCacheValue<T>(string key) where T : class
        {
            Cache cache = GetCache();

            if (cache == null)
            {
                return null;
            }

            return cache[key] as T;
        }
        

        public static bool SetCacheValue<T>(this T item) where T : ICacheKey
        {
            Cache cache = GetCache();

            if (cache == null)
            {
                return false;
            }

            cache[item.CacheKey] = item;

            return true;
        }

        #endregion
    }
}
