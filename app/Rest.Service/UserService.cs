using Rest.Domain.App;
using Rest.Domain.App.Interfaces.Repository;
using Rest.Domain.App.Interfaces.Service;
using Rest.Domain.App.Interfaces.Service.Authentication;
using Rest.Infra.CrossCutting.Extensions.Exceptions;
using Rest.Infra.CrossCutting.Utils.Authentication;

namespace Rest.Service
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        public UserService(IUserRepository userRepository, IJwtService jwtService) : base(userRepository)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user = await _userRepository.SingleOrDefaultAsync(x => x.Username == model.Username);

            /*var user = new User 
            {
                Id = 1,
                FirstName = "GenericUser",
                LastName = "GenericUser",
                Role = Infra.CrossCutting.Utils.Enum.Role.User,
                Username = "GenericUser",
                PaswordHash = BCrypt.Net.BCrypt.HashPassword("GenericU")
            };*/

            // validate
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PaswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt token
            var jwtToken = _jwtService.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken);
        }
    }
}
