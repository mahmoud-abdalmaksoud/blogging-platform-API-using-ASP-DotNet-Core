using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Data.Models
{
    public partial class UserSalary
    {

        [Key]
        public int UserId { get; set; }
        public decimal Salary { get; set; }


    }
}
