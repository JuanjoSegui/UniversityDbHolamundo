using System.ComponentModel.DataAnnotations;
namespace Holamundo.Models.DataModels
{

    public enum Level
    {
        Basic,
        Medium,
        Advance,
        Expert
            
    }


    public class Course : BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(280)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public Level Level { get; set; } = Level.Basic;


        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public Chapter Chapter { get; set; } = new Chapter();

        [Required]
        public ICollection<Studient> Studients { get; set; } = new List<Studient>();

        
    }
}
