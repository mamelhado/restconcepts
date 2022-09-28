using Rest.Domain.App.Interfaces.Service;
using StackExchange.Redis;
using System.Text.Json;

namespace Rest.Service.Caching
{
    public class RedisCacheService<TModel> : IRedisCacheService<TModel> where TModel : class
    {
        protected IDatabase _databaseRedis;

        public RedisCacheService(IConnectionMultiplexer redisCache, int dataBaseNumber) 
        {
            _databaseRedis = redisCache.GetDatabase(dataBaseNumber);
        }

        public TModel Get(string key)
        {
            if (_databaseRedis.KeyExists(key))
            {
                var obj = JsonSerializer.Deserialize<TModel>(_databaseRedis.StringGet(key));
                if(obj != null)
                    return obj;
            }

            return null;
        }

        public IEnumerable<TModel> GetCollection(string key)
        {
            if (_databaseRedis.KeyExists(key))
            {
                var obj = JsonSerializer.Deserialize<IEnumerable<TModel>>(_databaseRedis.StringGet(key));
                if (obj != null)
                    return obj;
            }

            return null;
        }

        public TModel Set(string key, TModel value)
        {
            string stringResult = string.Empty;
            if (_databaseRedis.KeyExists(key))
                _databaseRedis.KeyDelete(key);

            _databaseRedis.SetAdd(key, JsonSerializer.Serialize(value));
            return value;
        }

        public IEnumerable<TModel> SetCollection(string key, IEnumerable<TModel> collection)
        {
            if (_databaseRedis.KeyExists(key))
                _databaseRedis.KeyDelete(key);

            _databaseRedis.SetAdd(key, JsonSerializer.Serialize(collection));
            return collection;
        }
    }
}
