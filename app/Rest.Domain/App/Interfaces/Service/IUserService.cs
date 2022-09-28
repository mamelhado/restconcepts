using Rest.Infra.CrossCutting.Utils.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Domain.App.Interfaces.Service
{
    public interface IUserService : IBaseService<User>
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
    }
}
