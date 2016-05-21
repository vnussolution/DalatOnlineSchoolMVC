using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration.Conventions;
using DalatOnlineSchool.Models;

namespace DalatOnlineSchool.DAL
{
    public class SchoolContext:DbContext
    {
        // connection string is passed in to the constructor  "SchoolContext" - web.config
        public SchoolContext() : base("SchoolContext")
        {

            //Disable lazy loading causing overhead queries to DB
            //this.Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        //public DbSet<Person> People { get; set; } // to be used for inheritance

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // disable pluralizing, in this application we use singular form
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();

            // fluent API on relationship between Course and Instructor
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Instructors)
                .WithMany(i => i.Courses)
                .Map(t => t.MapLeftKey("CourseID").MapRightKey("InstructorID").ToTable("CourseInstructor"));
            modelBuilder.Entity<Department>().MapToStoredProcedures();

            // fluent API on relationship between Instructor and officeassigment
            // modelBuilder.Entity<Instructor>().HasOptional(p => p.OfficeAssignment).WithRequired(p => p.Instructor);

        }
    }
}