using Holamundo.Models.DataModels;
using Microsoft.EntityFrameworkCore;



namespace Holamundo.DataAccess
{
    public class UniversityContext: DbContext
    {
        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options) { 
        
        }
        // add DbSets (Tables of our Database)

        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Studient>? Studients { get; set; }
        

    }
}
