using Rest.Domain.App;
using Rest.Domain.App.Interfaces.Repository;
using Rest.Infra.Data.Context;

namespace Rest.Infra.Data.Repository
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(BaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
