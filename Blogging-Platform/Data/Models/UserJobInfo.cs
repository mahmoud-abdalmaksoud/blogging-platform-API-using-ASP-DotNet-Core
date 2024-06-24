using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Data.Models
{
    public partial class UserJobInfo
    {
        [Key]
        public int UserId { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
    }

}
