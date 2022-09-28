using Rest.Api.Models.Response;
using System.Text.Json.Serialization;

namespace Rest.Api.Models.Request
{
    public class UserRequestModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public RoleModel Role { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
