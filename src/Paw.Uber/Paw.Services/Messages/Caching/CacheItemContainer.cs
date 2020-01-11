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
    public class CacheItemContainer<T> : ICacheKey
        where T : class, IId
    {
        public T Item
        {
            get { return _Item; }
            set { _Item = value; }
        }
        private T _Item = null;

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

        public CacheItemContainer<T> Touch()
        {
            this.LastAccess = DateTime.UtcNow;
            return this;
        }

        #endregion

        #region CacheKey ...

        //prefix_typeName_key1_key2
        public string CacheKey
        {
            get { return _CacheKey; }
            set { _CacheKey = value; }
        }
        private string _CacheKey = String.Empty;

        public Func<T> Load
        {
            get { return _Load; }
            set { _Load = value; }
        }
        private Func<T> _Load = null;


        #endregion

        public void Refresh()
        {
            this.Item = this.Load.Invoke();
        }
        
    }
}
