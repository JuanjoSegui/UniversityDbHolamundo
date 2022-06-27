using Holamundo.Models.DataModels;

namespace Holamundo.Services
{
    public interface IStudientService
    {

        IEnumerable<Studient> GetStientsWithCourses();

        IEnumerable<Studient> GetStientsWithNoCourses();


    }
}
