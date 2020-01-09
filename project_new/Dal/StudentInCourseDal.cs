using project_new.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project_new.Dal
{
    public class StudentInCourseDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentInCourse>().ToTable("StudentCourses");
        }
        public DbSet<StudentInCourse> StudentInCourses { get; set; }
    }
}