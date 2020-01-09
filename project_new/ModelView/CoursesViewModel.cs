using project_new.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project_new.ViewModel
{
    public class CoursesViewModel
    {
        public Courses courseName { get; set; }
        public List<Courses> coursesName { get; set; }
    }
}