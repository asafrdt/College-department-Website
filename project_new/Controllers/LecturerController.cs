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

namespace Project.Controllers
{
    public class LecturerController : Controller
    {

        public ActionResult LecturerCourse()
        {
            return View("LecturerCourse");
        }
        public ActionResult MyCourses(MyCourses cour)
        {

            int id = (int)Session["ID"];
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI";
            SqlCommand sqlcomm = new SqlCommand(@"select DISTINCT coursename,day,startHour,endHour,class from cou_lec_stu where LecturerId= '" + id + "'", con); ;
            sqlcomm.Connection = con;
            con.Open();
            SqlDataReader sdr = sqlcomm.ExecuteReader();

            List<MyCourses> objmodel = new List<MyCourses>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var details = new MyCourses();
                    details.coursename = sdr["coursename"].ToString();
                    details.day = sdr["day"].ToString();
                    details.startHour = TimeSpan.Parse(sdr["startHour"].ToString());
                    details.endHour = TimeSpan.Parse(sdr["endHour"].ToString());

                    details.Class = sdr["Class"].ToString();
                    objmodel.Add(details);

                }
                cour.info = objmodel;
                con.Close();

            }

            return View("LecturerMyCourses",cour);
        }
        public ActionResult FindCourse(MyCourses cour)
        {
            string coursename = Request.Form["CourseNameTB"].ToString();
            int id = (int)Session["ID"];
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI";
            SqlCommand sqlcomm = new SqlCommand(@"select DISTINCT coursename,day,startHour,endHour,class from cou_lec_stu where LecturerId= '" + id + "' and coursename= '"+ coursename+"'", con); ;
            sqlcomm.Connection = con;
            con.Open();
            SqlDataReader sdr = sqlcomm.ExecuteReader();

            List<MyCourses> objmodel = new List<MyCourses>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var details = new MyCourses();
                    details.coursename = sdr["coursename"].ToString();
                    details.day = sdr["day"].ToString();
                    details.startHour = TimeSpan.Parse(sdr["startHour"].ToString());
                    details.endHour = TimeSpan.Parse(sdr["endHour"].ToString());

                    details.Class = sdr["Class"].ToString();
                    objmodel.Add(details);

                }
                cour.info = objmodel;
                con.Close();

            }

            return View("LecturerMyCourses", cour);
        }

        public ActionResult DisplayStudentInCourse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult MyStudent(Cou_Lec_Stu cour)
        {
            var id = (int)Session["ID"];
            
            string coursename = Request.Form["CourseNameTb"].ToString();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI";
            SqlCommand sqlcomm = new SqlCommand(@"select DISTINCT coursename,StudentId,FirstNameStu,LastNameStu,Grade from [dbo].[cou_lec_stu] where coursename= '" + coursename+ "'", con); ;
            sqlcomm.Connection = con;
            con.Open();
            SqlDataReader sdr = sqlcomm.ExecuteReader();

            List<Cou_Lec_Stu> objmodel = new List<Cou_Lec_Stu>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var details = new Cou_Lec_Stu();
                    details.coursename = sdr["coursename"].ToString();
                    details.StudentId = sdr["StudentId"].ToString();
                    details.FirstNameStu = sdr["FirstNameStu"].ToString();
                    details.LastNameStu = sdr["LastNameStu"].ToString();
                    details.Grade = Convert.ToInt32(sdr["Grade"]);
                    objmodel.Add(details);

                }
                cour.info = objmodel;
                con.Close();

            }
            return View("MyStudent",cour);
        }

        public ActionResult LecturerChat()
        {
            return View();
        }

        public ActionResult LecturerSchedule(Courses cour)
        {
            
            var id = (int)Session["ID"];

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI";
            SqlCommand sqlcomm = new SqlCommand(@"select coursename,Day,startHour,endHour,class from Course where LecturerId= '" + id + "'", con); ;
            sqlcomm.Connection = con;
            con.Open();
            SqlDataReader sdr = sqlcomm.ExecuteReader();
            List<Courses> objmodel = new List<Courses>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var details = new Courses();
                    details.CourseName = sdr["coursename"].ToString();
                    details.Day = sdr["Day"].ToString();
                    details.startHour = TimeSpan.Parse(sdr["startHour"].ToString());
                    details.endHour = TimeSpan.Parse(sdr["endHour"].ToString());
                    details.Class = sdr["Class"].ToString();
                    objmodel.Add(details);

                }
                cour.info = objmodel;
                con.Close();

            }
            return View("LecturerSchedule", cour);
        }
        public ActionResult LecturerExams()
        {
            return View();
        }
    }

}