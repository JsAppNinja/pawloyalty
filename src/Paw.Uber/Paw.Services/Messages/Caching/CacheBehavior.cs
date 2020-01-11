using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Caching
{
    public class CacheBehavior
    {
        public Type Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        private Type _Type = null;

        public string GetCacheKey(object item)
        {
            if (this.CacheKeyAccessor == null)
            {
                throw new InvalidOperationException($"CacheKeyAccessor not implmented for type [{this.Type.FullName}].");
            }

            return this.CacheKeyAccessor.Invoke(item);
        }

        public Func<object,string> CacheKeyAccessor
        {
            get { return _CacheKeyAccessor; }
            set { _CacheKeyAccessor = value; }
        }
        private Func<object,string> _CacheKeyAccessor = null;

        public Func<object, CacheActionTypes, CacheAction> CreateCacheAction
        {
            get { return _CreateCacheAction;  }
            set { _CreateCacheAction = value; }
        }
        private Func<object, CacheActionTypes, CacheAction> _CreateCacheAction = null;

        public Action<CacheBehavior, CacheAction> ProcessCacheAction
        {
            get { return _ProcessCacheAction; }
            set { _ProcessCacheAction = value; }
        }
        private Action<CacheBehavior, CacheAction> _ProcessCacheAction = null;
        
        public Func<Guid,Guid,ICacheKey> CreateCacheContainer
        {
            get { return _CreateCacheContainer; }
            set { _CreateCacheContainer = value; }
        }
        private Func<Guid, Guid, ICacheKey> _CreateCacheContainer = null;
        
        public List<Type> TriggerTypeList
        {
            get { return _TriggerTypeList; }
            set { _TriggerTypeList = value; }
        }
        private List<Type> _TriggerTypeList = new List<Type>();

    }
}
