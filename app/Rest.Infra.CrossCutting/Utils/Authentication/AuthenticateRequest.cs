using System.ComponentModel.DataAnnotations;

namespace Rest.Infra.CrossCutting.Utils.Authentication
{
    
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [MaxLength(length: 8)]
        [MinLength(length: 8)]
        public string Password { get; set; }
    }
}
