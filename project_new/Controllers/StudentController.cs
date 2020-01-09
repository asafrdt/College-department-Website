using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using project_new.Models;
using System.Configuration;
using project_new.Dal;
using project_new.ViewModel;

namespace project_new.Controllers
{
    public class StudentController : Controller
    {
     
        public ActionResult StudentCourse()
        {
            int id =(int) Session["ID"];
            stu_lec_courDal Cscl = new stu_lec_courDal();
            List<Course_StuCourse_Lec> ListCourseInfo =
                (from x in Cscl.CourseInfo
                 where (x.sicStudentId.Equals(id)) && (x.LecturerId.Equals(x.liLecturerId))
                 select x).ToList<Course_StuCourse_Lec>();

            ViewBag.Cour = ListCourseInfo;

            return View("StudentCourse");

        }
        public ActionResult StudentGrades()
        {
            int id = (int)Session["ID"];
            stu_lec_courDal Cscl = new stu_lec_courDal();
            List<Course_StuCourse_Lec> ListCourseInfo =
                (from x in Cscl.CourseInfo
                 where (x.sicStudentId.Equals(id)) && (x.LecturerId.Equals(x.liLecturerId))
                 select x).ToList<Course_StuCourse_Lec>();

            ViewBag.Cour = ListCourseInfo;

            return View("StudentGrades");

        }
        public ActionResult StudentExams()
        {
            int id = (int)Session["ID"];
            stu_lec_courDal Cscl = new stu_lec_courDal();
            List<Course_StuCourse_Lec> ListCourseInfo =
                (from x in Cscl.CourseInfo
                 where (x.sicStudentId.Equals(id)) && (x.LecturerId.Equals(x.liLecturerId))
                 select x).ToList<Course_StuCourse_Lec>();

            ViewBag.Cour = ListCourseInfo;

            return View("StudentExams");
        }
    }

}