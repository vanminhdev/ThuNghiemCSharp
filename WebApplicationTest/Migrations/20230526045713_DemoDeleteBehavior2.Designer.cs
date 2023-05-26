﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplicationTest.DbContexts;

#nullable disable

namespace WebApplicationTest.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230526045713_DemoDeleteBehavior2")]
    partial class DemoDeleteBehavior2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplicationTest.Entities.Classroom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("Classrooms");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.EntityDependent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EntityPrincipleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EntityPrincipleId");

                    b.ToTable("EntityDependent");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.EntityDependent2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EntityPrincipleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EntityPrincipleId");

                    b.ToTable("EntityDependent2");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.EntityDependentLevel2", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EntityDependentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("EntityDependentId");

                    b.ToTable("EntityDependentLevel2");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.EntityPrinciple", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("EntityPrinciple");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.Hobby", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("Hobby");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.StudentClassroom", b =>
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

                    b.HasIndex("StudentId");

                    b.ToTable("StudentsClassrooms");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.EntityDependent", b =>
                {
                    b.HasOne("WebApplicationTest.Entities.EntityPrinciple", "EntityPrinciple")
                        .WithMany("EntityDependents")
                        .HasForeignKey("EntityPrincipleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EntityPrinciple");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.EntityDependent2", b =>
                {
                    b.HasOne("WebApplicationTest.Entities.EntityPrinciple", "EntityPrinciple")
                        .WithMany("EntityDependent2s")
                        .HasForeignKey("EntityPrincipleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("EntityPrinciple");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.EntityDependentLevel2", b =>
                {
                    b.HasOne("WebApplicationTest.Entities.EntityDependent", "EntityDependent")
                        .WithMany()
                        .HasForeignKey("EntityDependentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("EntityDependent");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.Hobby", b =>
                {
                    b.HasOne("WebApplicationTest.Entities.Student", "Student")
                        .WithMany("Hobbies")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.StudentClassroom", b =>
                {
                    b.HasOne("WebApplicationTest.Entities.Classroom", "Classroom")
                        .WithMany("StudentClasses")
                        .HasForeignKey("ClassroomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplicationTest.Entities.Student", "Student")
                        .WithMany("StudentClassrooms")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Classroom");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.Classroom", b =>
                {
                    b.Navigation("StudentClasses");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.EntityPrinciple", b =>
                {
                    b.Navigation("EntityDependent2s");

                    b.Navigation("EntityDependents");
                });

            modelBuilder.Entity("WebApplicationTest.Entities.Student", b =>
                {
                    b.Navigation("Hobbies");

                    b.Navigation("StudentClassrooms");
                });
#pragma warning restore 612, 618
        }
    }
}