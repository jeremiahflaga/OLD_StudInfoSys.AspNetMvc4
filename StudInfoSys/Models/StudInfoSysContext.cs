using System.Data.Entity;

namespace StudInfoSys.Models
{
    public class StudInfoSysContext : DbContext
    {
        public StudInfoSysContext()
            : base("DefaultConnection")
        { }

        //public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<SubjectGradesRecord> SubjectGradesRecords { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Level> Levels { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //create Table per Type 
            modelBuilder.Entity<Student>().ToTable("Students");


            base.OnModelCreating(modelBuilder);
        }
    }
}