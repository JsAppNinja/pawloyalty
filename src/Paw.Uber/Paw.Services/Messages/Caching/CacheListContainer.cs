using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Paw.Services.Common;
using Paw.Services.Messages.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Paw.Services.Messages
{
    public class CacheListContainer<T> : ICacheKey 
        where T : class, IId
    {
        public T Get(Guid id)
        {
            T result;
            if (this.Items.TryGetValue(id, out result))
            {
                return result;
            }

            return null;
        }

        public void Put(T item)
        {
            this.Items[item.Id] = item;
        }

        public bool Delete(Guid id)
        {
            return this.Items.Remove(id);
        }

        public List<T> FindAll(Func<T,bool> predicate)
        {
            return this.Items.Values.Where(predicate).ToList();
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return this.Items.Values.Where(predicate).FirstOrDefault();
        }

        #region CacheKey ...

        //prefix_typeName_key1_key2
        public string CacheKey
        {
            get { return _CacheKey; }
            set { _CacheKey = value; }
        }
        private string _CacheKey = String.Empty;
        

        #endregion

        public Dictionary<Guid,T> Items
        {
            get { return _Items; }
            set { _Items = value; }
        }
        private Dictionary<Guid,T> _Items = new Dictionary<Guid, T>();

        #region Info ...

        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }
        private DateTime _Created = DateTime.UtcNow;
                    
        public DateTime? LastAccess   
        {
            get { return _LastAccess; }
            set { _LastAccess = value; }
        }
        private DateTime? _LastAccess = null;
        
        public CacheListContainer<T> Touch()
        {
            this.LastAccess = DateTime.UtcNow;
            return this;
        }

        #endregion

        public static bool TryGet(string key, out CacheListContainer<T> result) 
        {
            result = null;

            // Step 1. Get httpContext
            HttpContext httpContext = HttpContext.Current;

            if (httpContext == null)
            {
                return false;
            }

            // Step 2. Get cache object
            result = httpContext.Cache[key] as CacheListContainer<T>;
            bool cacheHit = result != null;
            
            // Step 3. return all
            return cacheHit;
        }

        public static CacheListContainer<T> GetCache(string key)
        {
            throw new NotImplementedException();
        }
    }
}
