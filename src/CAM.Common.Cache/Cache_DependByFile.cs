using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace CAM.Common.Cache
{
    public class Cache_DependByFile : ICache
    {
        private string _dependFile = "";
        public Cache_DependByFile(string FileName)
        {
            _dependFile = FileName;
        }

        public void add(string Key, object Value)
        {
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            if (!string.IsNullOrWhiteSpace(_dependFile))
            {
                cache.Insert(Key, Value, new CacheDependency(_dependFile));
            }
        }

        public object get(string Key)
        {
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            return cache.Get(Key);
        }

        public void delete(string Key)
        {
            System.Web.Caching.Cache cache = HttpContext.Current.Cache;
            cache.Remove(Key);
        }
    }
}
