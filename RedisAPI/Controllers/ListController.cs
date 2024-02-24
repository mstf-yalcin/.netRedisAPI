using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisAPI.services;
using StackExchange.Redis;
using System.Collections.Generic;

namespace RedisAPI.Controllers
{
 
    public class ListController : BaseController
    {
        private readonly string key = "nameList";
        public ListController(RedisService redisService) : base(redisService)
        {
        }

        [HttpPost]
        public void AddListCache(string name)
        {
            _redisDb.ListLeftPush(key, name);
            _redisDb.ListRightPush(key, name.ToCharArray().Reverse().ToString());
        }


        [HttpGet("GetListCache")]
        public List<String> GetListCache()
        {
            List<String> list = new();
            if (_redisDb.KeyExists(key))
            {
                var listRedis = _redisDb.ListRange(key, 0, -1);//optional 0,-1
                list = [.. listRedis];
            }

            //foreach (var item in listRedis)
            //{
            //    list.Add(item);
            //}

            return list;
        }

        [HttpDelete]
        public async Task DeleteListCache(string name)
        {
           await _redisDb.ListRemoveAsync(key, name);
        }


    }
}
