using project_new.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project_new.ViewModel
{
    public class StudentInCourseViewModel
    {
        public StudentInCourse studentincourseName { get; set; }
        public List<StudentInCourse> studentincoursesName { get; set; }
    }
}