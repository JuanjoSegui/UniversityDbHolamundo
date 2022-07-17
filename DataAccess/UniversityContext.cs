using UniversityDbHolaMundo.Models.DataModels;
using Microsoft.EntityFrameworkCore;



namespace UniversityDbHolaMundo.DataAccess
{
    public class UniversityContext: DbContext
    {



        private readonly ILoggerFactory _loggerFactory;


        public UniversityContext(DbContextOptions<UniversityContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }
        // add DbSets (Tables of our Database)

        public DbSet<User>? Users { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Studient>? Studients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var logger = _loggerFactory.CreateLogger<UniversityDbContext>();
            //optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new []{DbLoggerCategor.Database.Name }));
            //optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategor.Database.Name }), LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

        }

    }
}
