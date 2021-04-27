using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Context
{
    public partial class AplicationContext : DbContext
    {
        public AplicationContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("FileName=yourDVVS.db");
        /*public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }*/
        public AplicationContext(DbContextOptions<AplicationContext> options)
    : base(options)
        {
            Database.EnsureCreated();
        }
        public virtual DbSet<StudentsChoice> StudentsChoice { get; set; }
        public virtual DbSet<Lecturer> Lecturer { get; set; }
        public virtual DbSet<Period> Period { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentsChoice>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.SubjectId })
                    .HasName("choises_pkey");

                entity.ToTable("choices");

                entity.HasIndex(e => e.SubjectId).HasName("fki_choises_subject_subject_id");

                entity.HasIndex(e => e.UserId)
                    .HasName("fki_choises_student_user_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.StudentsChoices)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("choises_subject_subject_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StudentsChoices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("choises_student_user_id");
            });



            modelBuilder.Entity<Lecturer>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("lecturer_pkey");

                entity.ToTable("lecturers");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Lecturer)
                    .HasForeignKey<Lecturer>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("lecturer_user_user_id_fkey");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.ToTable("period");

                entity.Property(e => e.PeriodId).HasColumnName("period_id");

                entity.Property(e => e.PeriodBegining).HasColumnName("period_begining");

                entity.Property(e => e.PeriodEnd).HasColumnName("period_end");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("student_pkey");

                entity.ToTable("students");

                entity.HasIndex(e => e.UserId)
                    .HasName("fki_user_id_fkey");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Course).HasColumnName("course");


                entity.HasOne(d => d.User)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("student_user_user_id_fkey");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.SubjectId)
                    .HasName("subjects_pkey");

                entity.ToTable("subjects");


                entity.HasIndex(e => e.LecturerId)
                    .HasName("fki_subject_lecturer_user_id");

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description");

                entity.Property(e => e.LecturerId).HasColumnName("lecturer_id");

                entity.Property(e => e.MaxNumberOfStudents).HasColumnName("max_number_of_students");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(70);

                entity.Property(e => e.NumberOfStudents).HasColumnName("number_of_students");

                entity.Property(e => e.Semester).HasColumnName("semester");

                
                entity.HasOne(d => d.Lecturer)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.LecturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("subject_lecturer_user_id");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.HasIndex(e => e.Login)
                    .HasName("users_login_key")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("first_name")
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("last_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(255);

                entity.Property(e => e.MiddleName)
                    .IsRequired()
                    .HasColumnName("middle_name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(50);

                entity.Property(e => e.Role).HasColumnName("role");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}