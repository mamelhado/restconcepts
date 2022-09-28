using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.Domain.App.Interfaces.Service.Authentication
{
     public interface IJwtService
     {
        public string GenerateJwtToken(User user);
        public int? ValidateJwtToken(string token);
     }
}
