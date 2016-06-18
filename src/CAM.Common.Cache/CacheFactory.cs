using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAM.Common.Cache
{
    public class CacheFactory
    {
        public static ICache createMemcached()
        {
            return createMemcached("");
        }

        public static ICache createMemcached(string MemcachedName)
        {
            ICache ic = new Cache_Memcached(MemcachedName);
            return ic;
        }


        public static ICache createDependFile(string FileName)
        {
            ICache ic = new Cache_DependByFile(FileName);
            return ic;
        }
    }
}
