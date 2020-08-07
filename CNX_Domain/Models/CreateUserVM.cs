using System.ComponentModel.DataAnnotations;

namespace CNX_Domain.Models
{
    public class CreateUserVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Locale { get; set; }
        [Required]
        public string UserPassword { get; set; }
    }

    public class AuthenticateUserVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserPassword { get; set; }
    }
}
