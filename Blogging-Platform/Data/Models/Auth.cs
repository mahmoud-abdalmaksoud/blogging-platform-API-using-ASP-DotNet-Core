using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Data.Models
{
    public class Auth
    {
        [Key]
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
