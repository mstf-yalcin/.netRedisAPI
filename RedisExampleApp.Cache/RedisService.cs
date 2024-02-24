using StackExchange.Redis;
using System;

namespace RedisAPI.services
{
    public class RedisService
    {
        private readonly string _redisHost;
        private ConnectionMultiplexer _redis;

        public RedisService(string url)
        {
            _redisHost = url;
            Connect();
        }
        public async void Connect()
        {
            _redis = ConnectionMultiplexer.Connect(_redisHost);
        }

        public IDatabase GetDb(int db = 0)
        {
            return _redis.GetDatabase(db);
        }

        public async Task ConnectAsync()
        {

            _redis = await ConnectionMultiplexer.ConnectAsync(_redisHost);
        }


     


    }
}
