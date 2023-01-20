using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SCHOOLProj.Models;

namespace SCHOOLProj.Data
{
    public partial class SCHOOLContext : DbContext
    {
        public SCHOOLContext()
        {
        }

        public SCHOOLContext(DbContextOptions<SCHOOLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Grade> Grades { get; set; } = null!;
        public virtual DbSet<Personell> Personells { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-B2K268C; Initial Catalog=SCHOOL;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.FkPersonellId).HasColumnName("FK_PersonellId");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Subject).HasMaxLength(15);

                entity.HasOne(d => d.FkPersonell)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.FkPersonellId)
                    .HasConstraintName("FK_Course_Personell");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FkCourseId).HasColumnName("FK_CourseId");

                entity.Property(e => e.FkStudentId).HasColumnName("FK_StudentId");

                entity.Property(e => e.Grade1).HasColumnName("Grade");

                entity.Property(e => e.SetDate).HasColumnType("date");

                entity.HasOne(d => d.FkCourse)
                    .WithMany()
                    .HasForeignKey(d => d.FkCourseId)
                    .HasConstraintName("FK_Grades_Course");

                entity.HasOne(d => d.FkStudent)
                    .WithMany()
                    .HasForeignKey(d => d.FkStudentId)
                    .HasConstraintName("FK_Grades_Students");
            });

            modelBuilder.Entity<Personell>(entity =>
            {
                entity.ToTable("Personell");

                entity.Property(e => e.PersonellFname)
                    .HasMaxLength(20)
                    .HasColumnName("PersonellFName");

                entity.Property(e => e.PersonellLname)
                    .HasMaxLength(20)
                    .HasColumnName("PersonellLName");

                entity.Property(e => e.PersonellRole)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Startdate).HasColumnType("date");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.StudentFname)
                    .HasMaxLength(25)
                    .HasColumnName("StudentFName");

                entity.Property(e => e.StudentLname)
                    .HasMaxLength(25)
                    .HasColumnName("StudentLName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
