using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using project_new.Models;
using System.Configuration;

namespace project_new.Controllers
{
    public class FacultyController : Controller
    {
       
        public ActionResult FacultyAddStudent()
        {
            
            if (Request.Form["UserId"] != null && Request.Form["UserName"] != null && Request.Form["Password"] != null)
            {
                int UserId = Int32.Parse(Request.Form["UserId"]);
                string UserName = Request.Form["UserName"].ToString();
                string Password = Request.Form["Password"].ToString();
                string UserType = "Student";
                string FirstName = Request.Form["FirstName"].ToString();
                string LastName = Request.Form["LastName"].ToString();

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                SqlDataAdapter sda = new SqlDataAdapter("select UserId from [User] where UserId ='" + UserId + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    //message box "Student Allready Exist!

                }
                else
                {
                    //insert Student into User Table
                    SqlCommand sda1 = new SqlCommand(@"INSERT INTO [User] (UserId, UserName, Password, type) VALUES ('" + UserId + "', '" + UserName + "', '" + Password + "', '" + UserType + "')", con);
                    SqlDataAdapter da = new SqlDataAdapter(sda1);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    //insert Student into Student Table
                    SqlCommand sda2 = new SqlCommand(@"INSERT INTO [Student] (StudentId, FirstNameStu, LastNameStu) VALUES ('" + UserId + "', '" + FirstName + "', '" + LastName + "')", con);
                    SqlDataAdapter da1 = new SqlDataAdapter(sda2);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1);


                    con.Close();

                }
            }
            if (Request.Form["Studentid"] != null && Request.Form["CourseId"] != null)
            {
                int Studentid = Int32.Parse(Request.Form["Studentid"]);
                string CourseId = Request.Form["CourseId"].ToString();
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
     
                SqlDataAdapter sda = new SqlDataAdapter("select sicStudentId,sicCourseId from [StudentCourses] where sicStudentId ='" + Studentid + "' and sicCourseId ='" + CourseId + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    //message box "Student Allready Sign to this course!
                }
                else
                {
                    SqlCommand sda1 = new SqlCommand(@"INSERT INTO [StudentCourses] (sicStudentId, sicCourseId) VALUES ('" + Studentid + "', '" + CourseId + "')", con);
                    SqlDataAdapter da = new SqlDataAdapter(sda1);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                }
            }
            return View("FacultyAddStudent");
        }

        public ActionResult FacultyAddCourse()
        {
            if (Request.Form["CourseId"] != null && Request.Form["CourseName"] != null && Request.Form["LecturerId"] != null && Request.Form["Day"] != null && Request.Form["Class"] != null && Request.Form["StartHour"] != null && Request.Form["EndHour"] != null && Request.Form["MoedADate"] != null && Request.Form["MoedAClass"] != null && Request.Form["MoedBClass"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseId"]);
                string CourseName = Request.Form["CourseName"].ToString();
                int LecturerId = Int32.Parse(Request.Form["LecturerId"]);
                string Day = Request.Form["Day"].ToString();
                string Class = Request.Form["Class"].ToString();
                TimeSpan StartHour = TimeSpan.Parse(Request.Form["StartHour"].ToString());
                TimeSpan EndHour = TimeSpan.Parse(Request.Form["EndHour"].ToString());
                DateTime MoedADate = DateTime.Parse(Request.Form["MoedADate"]);
                string MoedAClass = Request.Form["MoedAClass"].ToString();
                DateTime MoedBDate = DateTime.Parse(Request.Form["MoedBDate"]);
                string MoedBClass = Request.Form["MoedBClass"].ToString();

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                SqlCommand sda1 = new SqlCommand(@"INSERT INTO [Course] (CourseId, coursename ,LecturerId, Day,startHour,endHour,class,moedA,classA,moedb,classb) VALUES ('" + CourseId + "', '" + CourseName + "', '" + LecturerId + "', '" + Day + "', '" + StartHour + "', '" + EndHour + "', '" + Class + "', '" + MoedADate + "', '" + MoedAClass + "', '" + MoedBDate + "', '" + MoedBClass + "')", con);
                SqlDataAdapter da = new SqlDataAdapter(sda1);
                DataSet ds = new DataSet();
                da.Fill(ds);
                con.Close();

            }

            return View("FacultyAddCourse");
        }

        public ActionResult FacultyUpdateCourse()
        {
            if (Request.Form["CourseIdTB"] != null && Request.Form["StartHourTB"] != null && Request.Form["EndHourTB"] != null && Request.Form["DayTB"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseIdTB"]);
                string StartHour = Request.Form["StartHourTB"].ToString();
                string EndHour = Request.Form["EndHourTB"].ToString();

                string Day = Request.Form["DayTB"].ToString();
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                con.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.CommandText = "update [Course] set Starthour = '" + StartHour + "', EndHour='" + EndHour + "', Day='" +Day+"' where CourseID = '" + CourseId + "'";
                sqlcomm.Connection = con;
                sqlcomm.ExecuteNonQuery();

                con.Close();
            }
            return View("FacultyUpdateCourse");

        }



        public ActionResult FacultyUpdateExamsSchedule()
        {

            if (Request.Form["CourseIdTB"] != null && Request.Form["MoedADateTB"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseIdTB"]);
                DateTime MoedADate = DateTime.Parse(Request.Form["MoedADateTB"]);

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter("select CourseId from [Course] where CourseId ='" + CourseId + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "update [Course] set moedA = '" + MoedADate + "' where CourseId ='" + CourseId + "'";
                    sqlcomm.Connection = con;
                    sqlcomm.ExecuteNonQuery();

                    con.Close();
                }
            }
            else if (Request.Form["CourseIdTB"] != null && Request.Form["MoedBDateTB"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseIdTB"]);
                DateTime MoedBDate = DateTime.Parse(Request.Form["MoedBDateTB"]);

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                SqlDataAdapter sda = new SqlDataAdapter("select CourseId from [Course] where CourseId ='" + CourseId + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    con.Open();
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "update [Course] set moedB = '" + MoedBDate + "' where CourseId ='" + CourseId + "'";
                    sqlcomm.Connection = con;
                    sqlcomm.ExecuteNonQuery();

                    con.Close();
                }
            }
            else
            {
     
                //message box "The course not exict!

            }
            

            return View("FacultyUpdateExamsSchedule");
        }



        public ActionResult FacultyUpdateExamsGrades()
        {
            if (Request.Form["CourseIdTB"] != null && Request.Form["StudentIdTB"] != null && Request.Form["NewGradeBTB"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseIdTB"]);
                int StudentId = Int32.Parse(Request.Form["StudentIdTB"]);
                int NewGradeB = Int32.Parse(Request.Form["NewGradeBTB"]);

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                SqlDataAdapter sda = new SqlDataAdapter("select sicStudentId,sicCourseId from [StudentCourses] where sicStudentId ='" + StudentId + "' and sicCourseId ='" + CourseId + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    con.Open();
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "update StudentCourses set GradeB= '" + NewGradeB + "'where sicStudentId =  '" + StudentId + "'and sicCourseId = (select courseid from Course where moedB < 'TO_DATE(' '" + DateTime.Parse(@DateTime.Now.ToShortDateString()) + "','dd/mm/yyyy') and CourseId = '" + CourseId + "')";
                    sqlcomm.Connection = con;
                    sqlcomm.ExecuteNonQuery();

                    con.Close();
                }
                else
                { 
                    //message box "Student Allready Sign to this course!

                }
            }
            else if (Request.Form["CourseIdTB"] != null && Request.Form["StudentIdTB"] != null && Request.Form["NewGradeATB"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseIdTB"]);
                int StudentId = Int32.Parse(Request.Form["StudentIdTB"]);
                int NewGradeA = Int32.Parse(Request.Form["NewGradeATB"]);

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                SqlDataAdapter sda = new SqlDataAdapter("select sicStudentId,sicCourseId from [StudentCourses] where sicStudentId ='" + StudentId + "' and sicCourseId ='" + CourseId + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    con.Open();
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "update StudentCourses set GradeA= '" + NewGradeA + "'where sicStudentId =  '" + StudentId + "'and sicCourseId = (select courseid from Course where moedA < TO_DATE('" + @DateTime.Now.ToShortDateString() + "','dd/mm/yyyy') and CourseId = '" + CourseId + "')" ;
                    sqlcomm.Connection = con;
                    sqlcomm.ExecuteNonQuery();

                    con.Close();
                }
                else
                {

                    //message box "Student Allready Sign to this course!

                }
            }

            return View("FacultyUpdateExamsGrades");
        }
    }

}