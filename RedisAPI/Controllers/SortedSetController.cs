using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisAPI.services;
using StackExchange.Redis;

namespace RedisAPI.Controllers
{
 
    public class SortedSetController : BaseController
    {
        public SortedSetController(RedisService redisService) : base(redisService)
        {
        }

        [HttpPost]
        public void SetCache(string data, int score)
        {

            _redisDb.SortedSetAdd("sortedSetKey", data, score);
            _redisDb.KeyExpire("sortedSetKey", TimeSpan.FromMinutes(5));

        }

        [HttpGet]
        public HashSet<string> GetCache()
        {
            HashSet<string> data = new HashSet<string>();
            if (_redisDb.KeyExists("sortedSetKey"))
            {
                //sorted data and score
                _redisDb.SortedSetRangeByRankWithScores("sortedSetKey", 0, -1, order: Order.Descending).ToList().ForEach(x =>
                {
                    data.Add(x.Score + ":" + x.Element);
                });

                //getData and score
                _redisDb.SortedSetScan("sortedSetKey").ToList().ForEach(x =>
                {
                    data.Add(x.Score + ":" + x.Element);
                });

                //getData
                _redisDb.SortedSetRangeByRank("sortedSetKey", order: Order.Descending).ToList().ForEach(x =>
                {
                    data.Add(x.ToString());
                });
            }

            return data;
        }

        [HttpDelete]
        public void DeleteCache(string data)
        {
            _redisDb.SortedSetRemove("sortedSetKey", data);

        }

    }
}
