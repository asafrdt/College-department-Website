using project_new.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace project_new.Dal
{
    public class FacultysDal : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Facultys>().ToTable("FacultyAdmin");
        }
        public DbSet<Facultys> Facultys { get; set; }
    }
}