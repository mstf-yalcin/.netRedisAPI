using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedisAPI.services;
using StackExchange.Redis;

namespace RedisAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {

        private readonly RedisService _redisService;
        protected readonly IDatabase _redisDb;
        public BaseController(RedisService redisService)
        {
            _redisService = redisService;
            _redisDb = _redisService.GetDb(2);
        }
    }
}
