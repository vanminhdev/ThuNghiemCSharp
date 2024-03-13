using Microsoft.EntityFrameworkCore;
using Performance.Entities;

namespace Performance.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.\\;Initial Catalog=Performance;Integrated Security=True;Pooling=False;TrustServerCertificate=True;");
            }
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
                .HasIndex(s => new { s.Phone, s.StudentCode, s.Email, s.DateOfBirth, s.Name, s.IndustryCode, s.MajorCode, s.Deleted })
                .IsDescending(false, false, false, true, false, false, false, false)
                .HasDatabaseName($"IX_{nameof(Student)}");

            modelBuilder.Entity<Classroom>()
                .HasIndex(c => new { c.MaxStudent, c.Status })
                .IncludeProperties(c => new { c.Name })
                .HasDatabaseName($"IX_{nameof(Classroom)}");

            //modelBuilder.Entity<Classroom>()
            //    .HasIndex(c => new { c.MaxStudent })
            //    .HasDatabaseName($"IX_{nameof(Classroom)}1");

            //modelBuilder.Entity<Classroom>()
            //    .HasIndex(c => new { c.Status })
            //    .HasDatabaseName($"IX_{nameof(Classroom)}2");
        }
    }
}
