using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyProject.Data.Models
{
    public partial class User
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        public ICollection<Post> Posts { get; set; }
        public bool Active { get; set; }
    }
}
