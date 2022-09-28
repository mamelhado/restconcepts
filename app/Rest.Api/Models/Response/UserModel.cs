using System.Text.Json.Serialization;

namespace Rest.Api.Models.Response
{
    public class UserModel : ModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public RoleModel Role { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
