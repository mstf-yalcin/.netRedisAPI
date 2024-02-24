using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisAPI.services;
using StackExchange.Redis;

namespace RedisAPI.Controllers
{
    public class HashController : BaseController
    {
        private readonly string hashKey = "hashKey";
        public HashController(RedisService redisService) : base(redisService)
        {
        }

        [HttpPost]
        public async Task AddHashCache(string field, string data)
        {
            await _redisDb.HashSetAsync(hashKey, field, data);
        }

        [HttpGet]
        public Dictionary<String, String> GetAllCache()
        {
            Dictionary<String, String> dict = new();
            if (_redisDb.KeyExists(hashKey))
            {
                _redisDb.HashGetAll(hashKey).ToList().ForEach(x =>
                {
                    dict.Add(x.Name, x.Value);
                });
            }
            return dict;
        }

        [HttpGet("Get/{key}")]
        public string GetCache(string key)
        {
            if (_redisDb.HashExists(hashKey, key))
            {
                return _redisDb.HashGet(hashKey, key).ToString(); ;
            }
            return "";
        }

        [HttpDelete]
        public void DeleteCache(string key)
        {
            _redisDb.HashDelete(hashKey, key);

        }
    }
}
