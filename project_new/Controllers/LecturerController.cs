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
            SqlCommand sqlcomm = new SqlCommand(@"select DISTINCT coursename,day,startHour,endHour,class,moedAYear,moedAMonth,moedADay,classA,moedBYear,moedBMonth,moedBDay,classB from cou_lec_stu where LecturerId= '" + id + "'", con); ;
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
                    details.moedADay = Convert.ToInt32(sdr["moedADay"]);
                    details.moedAMonth = Convert.ToInt32(sdr["moedAMonth"]);
                    details.moedAYear = Convert.ToInt32(sdr["moedAYear"]);
                    details.classA = sdr["classA"].ToString();
                    details.moedBDay = Convert.ToInt32(sdr["moedBDay"]);
                    details.moedBMonth = Convert.ToInt32(sdr["moedBMonth"]);
                    details.moedBYear = Convert.ToInt32(sdr["moedBYear"]);
                    details.classB = sdr["classB"].ToString();
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
            SqlCommand sqlcomm = new SqlCommand(@"select DISTINCT coursename,day,startHour,endHour,class,moedAYear,moedAMonth,moedADay,classA,moedBYear,moedBMonth,moedBDay,classB from cou_lec_stu where LecturerId= '" + id + "' and coursename= '" + coursename+"'", con); ;
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
                    details.moedADay = Convert.ToInt32(sdr["moedADay"]);
                    details.moedAMonth = Convert.ToInt32(sdr["moedAMonth"]);
                    details.moedAYear = Convert.ToInt32(sdr["moedAYear"]);
                    details.classA = sdr["classA"].ToString();
                    details.moedBDay = Convert.ToInt32(sdr["moedBDay"]);
                    details.moedBMonth = Convert.ToInt32(sdr["moedBMonth"]);
                    details.moedBYear = Convert.ToInt32(sdr["moedBYear"]);
                    details.classB = sdr["classB"].ToString();
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
            SqlCommand sqlcomm = new SqlCommand(@"select DISTINCT coursename,StudentId,FirstNameStu,LastNameStu,GradeA,GradeB from [dbo].[cou_lec_stu] where coursename= '" + coursename+ "'", con); ;
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
                    details.GradeA = Convert.ToInt32(sdr["GradeA"]);
                    if (sdr["GradeB"].ToString() !="" )
                    {
                        details.GradeB = Convert.ToInt32(sdr["GradeB"]);
                    }
                    objmodel.Add(details);

                }
                cour.info = objmodel;
                con.Close();

            }
            return View("MyStudent",cour);
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



        public ActionResult LecturerUpdateExamsGrades()
        {
            var id = (int)Session["ID"];

            if (Request.Form["CourseIdTB"] != null && Request.Form["StudentIdTB"] != null && Request.Form["NewGradeBTB"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseIdTB"]);
                int StudentId = Int32.Parse(Request.Form["StudentIdTB"]);
                int NewGradeB = Int32.Parse(Request.Form["NewGradeBTB"]);

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                SqlDataAdapter sda1 = new SqlDataAdapter("select LecturerId from [Course] where CourseId ='" + CourseId + "' and LecturerId= '"+ id + "'", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count == 1)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("select sicStudentId,sicCourseId from [StudentCourses] where sicStudentId ='" + StudentId + "' and sicCourseId ='" + CourseId + "'", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        con.Open();
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.CommandText = "update StudentCourses set GradeB= '" + NewGradeB + "'where sicStudentId =  '" + StudentId + "'and sicCourseId = (select CourseId from Course where ((moedBYear = '" + DateTime.Now.Year + "' and moedBMonth = '" + DateTime.Now.Month + "' and moedBDay <= '" + DateTime.Now.Day + "') or (moedBYear = '" + DateTime.Now.Year + "' and moedBMonth < '" + DateTime.Now.Month + "') or ( moedBYear < '" + DateTime.Now.Year + "')) and CourseId =  '" + CourseId + "')";
                        sqlcomm.Connection = con;
                        sqlcomm.ExecuteNonQuery();

                        con.Close();
                    }
                    else
                    {
                        //message box

                    }
                }
            }


            else if (Request.Form["CourseIdTB"] != null && Request.Form["StudentIdTB"] != null && Request.Form["NewGradeATB"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseIdTB"]);
                int StudentId = Int32.Parse(Request.Form["StudentIdTB"]);
                int NewGradeA = Int32.Parse(Request.Form["NewGradeATB"]);

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                SqlDataAdapter sda1 = new SqlDataAdapter("select LecturerId from [Course] where CourseId ='" + CourseId + "' and LecturerId= '" + id + "'", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count == 1)
                {
                    SqlDataAdapter sda = new SqlDataAdapter("select sicStudentId,sicCourseId from [StudentCourses] where sicStudentId ='" + StudentId + "' and sicCourseId ='" + CourseId + "'", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count == 1)
                    {
                        con.Open();
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.CommandText = "update StudentCourses set GradeA= '" + NewGradeA + "'where sicStudentId =  '" + StudentId + "'and sicCourseId = (select CourseId from Course where ((moedAYear = '" + DateTime.Now.Year + "' and moedAMonth = '" + DateTime.Now.Month + "' and moedADay <= '" + DateTime.Now.Day + "') or (moedAYear = '" + DateTime.Now.Year + "' and moedAMonth < '" + DateTime.Now.Month + "') or ( moedAYear < '" + DateTime.Now.Year + "')) and CourseId =  '" + CourseId + "')";
                        sqlcomm.Connection = con;
                        sqlcomm.ExecuteNonQuery();

                        con.Close();
                    }
                    else
                    {

                        //message box

                    }
                }
            }

            return View("LecturerUpdateExamsGrades");
        }
    }

}