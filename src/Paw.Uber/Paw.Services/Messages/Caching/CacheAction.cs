using Paw.Services.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paw.Services.Messages.Caching
{
    public class CacheAction
    {

        public Guid Id
        {
            get { return _Id; }
            set { _Id = value; }
        }
        private Guid _Id = Muid.Comb();

        public CacheActionTypes CacheActionType
        {
            get { return _CacheActionType; }
            set { _CacheActionType = value; }
        }
        private CacheActionTypes _CacheActionType = CacheActionTypes.Insert;

        public Type SourceType
        {
            get { return _SourceType; }
            set { _SourceType = value; }
        }
        private Type _SourceType = null;

        public object Item
        {
            get { return _Item; }
            set { _Item = value; }
        }
        private object _Item = null;

        public List<KeyValuePair<string,Guid>> PropertyList
        {
            get { return _PropertyList; }
            set { _PropertyList = value; }
        }
        private List<KeyValuePair<string,Guid>> _PropertyList = new List<KeyValuePair<string, Guid>>();

        #region Fluent ...

        public CacheAction AddProperty(string key, Guid value)
        {
            this.PropertyList.Add(new KeyValuePair<string, Guid>(key, value));
            return this;
        }

        public CacheAction SetSourceType(Type type)
        {
            this.SourceType = type;
            return this;
        }

        public CacheAction SetCacheActionType(CacheActionTypes cacheActionType)
        {
            this.CacheActionType = cacheActionType;
            return this;
        }

        public CacheAction SetItem(object item)
        {
            this.Item = item;
            return this;
        }

        #endregion
    }
}
