using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisAPI.services;
using StackExchange.Redis;

namespace RedisAPI.Controllers;

public class SetController : BaseController
{
    public SetController(RedisService redisService) : base(redisService)
    {
    }

    [HttpPost]
    public void SetCache(string data)
    {
        //absolute
        if (!_redisDb.KeyExists("setKey"))
        {
            var expiration = TimeSpan.FromMinutes(5);
            _redisDb.SetAdd("setKey", data);
            _redisDb.KeyExpire("setKey", expiration);
        }
        else
            _redisDb.SetAdd("setKey", data);

    }

    [HttpGet]
    public HashSet<string> GetCache()
    {
        HashSet<string> cache = new HashSet<string>();

        if (_redisDb.KeyExists("setKey"))
        {
            foreach (var item in _redisDb.SetMembers("setKey").ToArray())
            {
                cache.Add(item);
            }
        }
        return cache;
    }

    [HttpDelete]
    public void DeleteCache(string data)
    {
        _redisDb.SetRemove("setKey", data);
    }

}
