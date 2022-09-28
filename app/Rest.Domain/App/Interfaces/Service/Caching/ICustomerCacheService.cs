using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Domain.App.Interfaces.Service
{
    public interface ICustomerCacheService : IRedisCacheService<Customer>
    {

    }
}
