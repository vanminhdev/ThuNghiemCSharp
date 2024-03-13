﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Performance.DbContexts;

#nullable disable

namespace Performance.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240122065742_UpdateDeletedClassroom2")]
    partial class UpdateDeletedClassroom2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Performance.Entities.Classroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("MaxStudent")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaxStudent", "Status")
                        .HasDatabaseName("IX_Classroom");

                    SqlServerIndexBuilderExtensions.IncludeProperties(b.HasIndex("MaxStudent", "Status"), new[] { "Name" });

                    b.ToTable("Classroom");
                });

            modelBuilder.Entity("Performance.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("IndustryCode")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("MajorCode")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Phone")
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)");

                    b.Property<string>("StudentCode")
                        .HasMaxLength(128)
                        .IsUnicode(false)
                        .HasColumnType("varchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("Phone", "StudentCode", "Email", "DateOfBirth", "Name", "IndustryCode", "MajorCode", "Deleted")
                        .IsDescending(false, false, false, true, false, false, false, false)
                        .HasDatabaseName("IX_Student");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Performance.Entities.StudentClassroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClassroomId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClassroomId");

                    b.HasIndex(new[] { "StudentId", "ClassroomId" }, "IX_StudentClassroom");

                    b.ToTable("StudentClassroom");
                });

            modelBuilder.Entity("Performance.Entities.StudentClassroom", b =>
                {
                    b.HasOne("Performance.Entities.Classroom", "Classroom")
                        .WithMany()
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Performance.Entities.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("Student");
                });
#pragma warning restore 612, 618
        }
    }
}
