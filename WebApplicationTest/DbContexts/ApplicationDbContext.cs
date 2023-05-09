using WebApplicationTest.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace WebApplicationTest.DbContexts
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
        public virtual DbSet<Hobby> Hobbies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>(entity =>
            //{
            //    entity.HasMany(e => e.StudentClassrooms)
            //        .WithOne(e => e.Student)
            //        .HasForeignKey(e => e.StudentId);
            //});

            //modelBuilder.Entity<Classroom>(entity =>
            //{
            //    entity.HasMany<StudentClassroom>()
            //        .WithOne(e => e.Classroom)
            //        .HasForeignKey(e => e.ClassroomId);
            //});

            //Phải khai báo đầy đủ các navigation property
            modelBuilder.Entity<Student>() //lấy 1 trong 2 entity quan hệ n - n, ở đây chọn Student
                .HasMany(student => student.Classrooms) //Student có nhiều Classroom
                .WithMany(classroom => classroom.Students) //với Classroom có nhiều Student
                .UsingEntity<StudentClassroom>( //cấu hình bảng quan hệ, viết khoá ngoại bảng Classroom trước do bên trên khai báo HasMany là Student có nhiều Classroom
                    studentClassroom => studentClassroom
                        .HasOne(sc => sc.Classroom)
                        .WithMany(classroom => classroom.StudentClasses)
                        .HasForeignKey(sc => sc.ClassroomId), //khoá ngoại
                    studentClassroom => studentClassroom
                        .HasOne(sc => sc.Student)
                        .WithMany(student => student.StudentClassrooms)
                        .HasForeignKey(sc => sc.StudentId)); //khoá ngoại

            modelBuilder.Entity<Hobby>()
                .HasOne(e => e.Student)
                .WithMany(e => e.Hobbies)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.SetNull); //dành cho trường hợp trường khoá ngoại có thể null
        }
    }
}
