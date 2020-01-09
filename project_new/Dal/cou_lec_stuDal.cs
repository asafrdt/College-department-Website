using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using project_new.Models;


namespace project_new.Dal
{
    public class cou_lec_stuDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cou_Lec_Stu>().ToTable("cou_lec_stu");

        }
        public DbSet<Cou_Lec_Stu> LecturesInfo { get; set; }

    }
}