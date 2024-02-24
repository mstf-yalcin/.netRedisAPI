using StackExchange.Redis;

namespace RedisAPI.services
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly string _redisPort;
        private ConnectionMultiplexer _redis;

        public RedisService(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];
            _redisPort = configuration["Redis:Port"];
        }
        public async void Connect()
        {
            var configString = $"{_redisHost}:{_redisPort}";

            _redis = ConnectionMultiplexer.Connect(configString);
        }

        public async Task ConnectAsync()
        {
            var configString = $"{_redisHost}:{_redisPort}";

            _redis = await ConnectionMultiplexer.ConnectAsync(configString);
        }


        public IDatabase GetDb(int db = 0)
        {
            return _redis.GetDatabase(db);
        }


    }
}
