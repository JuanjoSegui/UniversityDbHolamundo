using System.ComponentModel.DataAnnotations;



namespace Holamundo.Models.DataModels
{
    public class Studient: BaseEntity
    {

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime Dob { get; set; }
        [Required]
        public ICollection<Course> Courses { get; set; } = new List<Course>();
        


    }
}
