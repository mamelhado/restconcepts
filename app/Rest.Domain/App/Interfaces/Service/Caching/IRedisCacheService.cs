using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Domain.App.Interfaces.Service
{
    public interface IRedisCacheService<TModel> where TModel : class
    {
        IEnumerable<TModel> GetCollection(string key);
        
        IEnumerable<TModel> SetCollection(string key, IEnumerable<TModel> collection);

        TModel Get(string key);

        TModel Set(string key, TModel value);
    }
}
