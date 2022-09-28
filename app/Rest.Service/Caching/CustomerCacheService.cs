using Rest.Domain.App;
using Rest.Domain.App.Interfaces.Service;
using StackExchange.Redis;

namespace Rest.Service.Caching
{
    public class CustomerCacheService : RedisCacheService<Customer>, ICustomerCacheService
    {
        private const int _dataBaseNumber = 1;
        public CustomerCacheService(IConnectionMultiplexer redisCache) : base(redisCache, _dataBaseNumber)
        {
            _databaseRedis = redisCache.GetDatabase(_dataBaseNumber);
        }
    }
}
