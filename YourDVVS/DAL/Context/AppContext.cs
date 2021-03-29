using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Context
{
    public class AppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlite("Data Source=YourDVVS.db");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(x => x.Lecturer).WithOne(x => x.User).HasForeignKey<Lecturer>(x => x.UserId);
            modelBuilder.Entity<User>().HasOne(x => x.Student).WithOne(x => x.User).HasForeignKey<Student>(x => x.UserId);
        }

        public virtual DbSet<StudentsChoice> StudentsChoice { get; set; }
        public virtual DbSet<Lecturer> Lecturer { get; set; }
        public virtual DbSet<Period> Period { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}