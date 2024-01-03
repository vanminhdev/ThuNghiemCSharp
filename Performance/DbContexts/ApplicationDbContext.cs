using Microsoft.EntityFrameworkCore;
using Performance.Entities;

namespace Performance.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentClassroom> StudentClassrooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(student => student.Classrooms)
                .WithMany(classroom => classroom.Students)
                .UsingEntity<StudentClassroom>(
                    studentClassroom => studentClassroom
                        .HasOne(sc => sc.Classroom)
                        .WithMany()
                        .HasForeignKey(sc => sc.ClassroomId),
                    studentClassroom => studentClassroom
                        .HasOne(sc => sc.Student)
                        .WithMany()
                        .HasForeignKey(sc => sc.StudentId));

            modelBuilder.Entity<Student>()
                .HasIndex(c => new { c.Name })
                .HasDatabaseName($"IX_{nameof(Student)}");

            modelBuilder.Entity<Classroom>()
                .HasIndex(c => new { c.MaxStudent, c.Status })
                .IncludeProperties(c => new { c.Name })
                .HasDatabaseName($"IX_{nameof(Classroom)}");
        }
    }
}
