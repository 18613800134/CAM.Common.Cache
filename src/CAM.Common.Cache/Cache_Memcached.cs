using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CAM.Common.Config;
using Memcached.ClientLibrary;

namespace CAM.Common.Cache
{
    public class Cache_Memcached : ICache
    {

        [Serializable]
        private class MemcachedServerConfig
        {
            public string Host { get; set; }
            public long Port { get; set; }

            public MemcachedServerConfig()
            {
                Host = "127.0.0.1";
                Port = 11211;
            }

            public override string ToString()
            {
                return string.Format("{0}:{1}", Host, Port);
            }
        }


        private MemcachedServerConfig _msc = null;
        private MemcachedClient _mc;
        private string _memcachedconfigname = "";

        public Cache_Memcached(string Host, long Port)
        {
            _msc = new MemcachedServerConfig()
            {
                Host = Host,
                Port = Port,
            };
            initMemCacheClient();
        }

        public Cache_Memcached(string MemCachedConfigName)
        {
            _memcachedconfigname = string.IsNullOrWhiteSpace(MemCachedConfigName) ? "MemCached" : MemCachedConfigName;
            readConfig();
            initMemCacheClient();
        }

        private void readConfig()
        {
            IConfig<MemcachedServerConfig> ic = ConfigFactory.createConfig<MemcachedServerConfig>(_memcachedconfigname);
            _msc = ic.ConfigObject;
        }

        private void initMemCacheClient()
        {
            try
            {
                SockIOPool pool = SockIOPool.GetInstance();

                string[] serverList = { _msc.ToString() };

                pool.SetServers(serverList);
                pool.InitConnections = 1;
                pool.MinConnections = 1;
                pool.MaxConnections = 10;
                pool.SocketConnectTimeout = 1000;
                pool.SocketTimeout = 3000;
                pool.MaintenanceSleep = 30;
                pool.Failover = true;
                pool.Nagle = false;
                pool.Initialize();

                _mc = new MemcachedClient();
                _mc.EnableCompression = false;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("连接缓存服务器失败：{0}", ex.Message));
            }
        }


        public void add(string Key, object Value)
        {
            _mc.Set(Key, Value);
        }

        public object get(string Key)
        {
            return _mc.Get(Key);
        }

        public void delete(string Key)
        {
            _mc.Delete(Key);
        }
    }
}
