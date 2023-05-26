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
        public virtual DbSet<EntityPrinciple> EntityPrinciples { get; set; }
        public virtual DbSet<EntityDependent> EntityDependents { get; set; }
        public virtual DbSet<EntityDependent2> EntityDependent2s { get; set; }
        public virtual DbSet<EntityDependentLevel2> EntityDependentLevel2s { get; set; }

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
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EntityDependent>()
                .HasOne(e => e.EntityPrinciple)
                .WithMany(e => e.EntityDependents)
                .HasForeignKey(e => e.EntityPrincipleId)
                .OnDelete(DeleteBehavior.Restrict);
            //delete behavior Restrict, NoAction, SetNull hoạt động giống hệt nhau đều set null cho khoá ngoại khi xoá principle entity (trên docs microsoft cũng mô tả giống hệt nhau)
            //khi cài là restrict thì trong db là no action (tuy nhiên khi xoá bằng entity framework thì nó vẫn hoạt động như set null)
            //trường hợp DeleteBehavior là set null thì trong db là set null
            //mặc định không nói gì trong ef tạo migrations sẽ là cascade, mặc định trong db thì lại là no action

            //tóm lại chỉ cần phân thành 3 loại Restrict, NoAction coi là NoAction, loại SetNull và loại Cascade
            //loại 1 thì báo lỗi khi xoá priciple entity
            //loại 2 thì set null vào trường khoá ngoại khi xoá principle entity
            //loại 3 thì xoá tất cả dependent entity khi xoá principle entity

            modelBuilder.Entity<EntityDependent2>()
                .HasOne(e => e.EntityPrinciple)
                .WithMany(e => e.EntityDependent2s)
                .HasForeignKey(e => e.EntityPrincipleId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EntityDependentLevel2>()
                .HasOne(e => e.EntityDependent)
                .WithMany()
                .HasForeignKey(e => e.EntityDependentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
