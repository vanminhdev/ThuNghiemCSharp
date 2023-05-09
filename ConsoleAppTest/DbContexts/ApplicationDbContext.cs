using ConsoleAppTest.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppTest.DbContexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected ApplicationDbContext()
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Classroom> Classrooms { get; set; }
        public virtual DbSet<StudentClassroom> StudentsClassrooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>(entity =>
            //{
            //    entity.HasMany(e => e.StudentClassrooms)
            //        .WithOne()
            //        .HasForeignKey(e => e.StudentId);
            //});

            modelBuilder.Entity<StudentClassroom>(entity =>
            {
                //entity.HasOne<Student>()
                //    .WithMany()
                //    .HasForeignKey(e => e.StudentId);

                //entity.HasOne<Classroom>()
                //    .WithMany()
                //    .HasForeignKey(e => e.ClassroomId);
            });
        }
    }
}
