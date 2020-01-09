using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using project_new.Models;


namespace project_new.Dal
{
    public class stu_lec_courDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course_StuCourse_Lec>().ToTable("stu_lec_cour");

        }
        public DbSet<Course_StuCourse_Lec> CourseInfo { get; set; }
    }
}