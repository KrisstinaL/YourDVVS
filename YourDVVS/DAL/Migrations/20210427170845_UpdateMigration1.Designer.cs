﻿// <auto-generated />
using System;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(AplicationContext))]
    [Migration("20210427170845_UpdateMigration1")]
    partial class UpdateMigration1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.4");

            modelBuilder.Entity("DAL.Entities.Lecturer", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.HasKey("UserId")
                        .HasName("lecturer_pkey");

                    b.ToTable("lecturers");
                });

            modelBuilder.Entity("DAL.Entities.Period", b =>
                {
                    b.Property<int>("PeriodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("period_id");

                    b.Property<DateTime>("PeriodBegining")
                        .HasColumnType("TEXT")
                        .HasColumnName("period_begining");

                    b.Property<DateTime>("PeriodEnd")
                        .HasColumnType("TEXT")
                        .HasColumnName("period_end");

                    b.HasKey("PeriodId");

                    b.ToTable("period");
                });

            modelBuilder.Entity("DAL.Entities.Student", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.Property<int?>("Course")
                        .HasColumnType("INTEGER")
                        .HasColumnName("course");

                    b.HasKey("UserId")
                        .HasName("student_pkey");

                    b.HasIndex("UserId")
                        .HasDatabaseName("fki_user_id_fkey");

                    b.ToTable("students");
                });

            modelBuilder.Entity("DAL.Entities.StudentsChoice", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.Property<int>("SubjectId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("subject_id");

                    b.HasKey("UserId", "SubjectId")
                        .HasName("choises_pkey");

                    b.HasIndex("SubjectId")
                        .HasDatabaseName("fki_choises_subject_subject_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("fki_choises_student_user_id");

                    b.ToTable("choices");
                });

            modelBuilder.Entity("DAL.Entities.Subject", b =>
                {
                    b.Property<int>("SubjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("subject_id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<string>("Faculty")
                        .HasColumnType("TEXT");

                    b.Property<int>("LecturerId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("lecturer_id");

                    b.Property<int?>("MaxNumberOfStudents")
                        .HasColumnType("INTEGER")
                        .HasColumnName("max_number_of_students");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<int?>("NumberOfStudents")
                        .HasColumnType("INTEGER")
                        .HasColumnName("number_of_students");

                    b.Property<int?>("Semester")
                        .HasColumnType("INTEGER")
                        .HasColumnName("semester");

                    b.HasKey("SubjectId")
                        .HasName("subjects_pkey");

                    b.HasIndex("LecturerId")
                        .HasDatabaseName("fki_subject_lecturer_user_id");

                    b.ToTable("subjects");
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("user_id");

                    b.Property<string>("Faculty")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("last_name");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT")
                        .HasColumnName("login");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("middle_name");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT")
                        .HasColumnName("password");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER")
                        .HasColumnName("role");

                    b.HasKey("UserId")
                        .HasName("users_pkey");

                    b.HasIndex("Login")
                        .IsUnique()
                        .HasDatabaseName("users_login_key");

                    b.ToTable("users");
                });

            modelBuilder.Entity("DAL.Entities.Lecturer", b =>
                {
                    b.HasOne("DAL.Entities.User", "User")
                        .WithOne("Lecturer")
                        .HasForeignKey("DAL.Entities.Lecturer", "UserId")
                        .HasConstraintName("lecturer_user_user_id_fkey")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Entities.Student", b =>
                {
                    b.HasOne("DAL.Entities.User", "User")
                        .WithOne("Student")
                        .HasForeignKey("DAL.Entities.Student", "UserId")
                        .HasConstraintName("student_user_user_id_fkey")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Entities.StudentsChoice", b =>
                {
                    b.HasOne("DAL.Entities.Subject", "Subject")
                        .WithMany("StudentsChoices")
                        .HasForeignKey("SubjectId")
                        .HasConstraintName("choises_subject_subject_id")
                        .IsRequired();

                    b.HasOne("DAL.Entities.Student", "User")
                        .WithMany("StudentsChoices")
                        .HasForeignKey("UserId")
                        .HasConstraintName("choises_student_user_id")
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Entities.Subject", b =>
                {
                    b.HasOne("DAL.Entities.Lecturer", "Lecturer")
                        .WithMany("Subjects")
                        .HasForeignKey("LecturerId")
                        .HasConstraintName("subject_lecturer_user_id")
                        .IsRequired();

                    b.Navigation("Lecturer");
                });

            modelBuilder.Entity("DAL.Entities.Lecturer", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("DAL.Entities.Student", b =>
                {
                    b.Navigation("StudentsChoices");
                });

            modelBuilder.Entity("DAL.Entities.Subject", b =>
                {
                    b.Navigation("StudentsChoices");
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Navigation("Lecturer");

                    b.Navigation("Student");
                });
#pragma warning restore 612, 618
        }
    }
}
