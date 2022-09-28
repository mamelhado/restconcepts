using Rest.Domain.App;
using Rest.Domain.App.Interfaces.Repository;
using Rest.Infra.Data.Context;

namespace Rest.Infra.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        
        public UserRepository(BaseContext context) : base(context)
        {
            _context = context;
        }
    }
}
