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

namespace project_new.Controllers
{
    public class FacultyController : Controller
    {
       
        public ActionResult FacultyAddStudent(Courses cour)
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
                    @Session["error"] = "Student Allready Exist in the System";
                    return View("FacultyAddStudent");

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
                    @Session["error"] = "Student Allready Sign to this course";
                    return View("FacultyAddStudent");
                }
                SqlDataAdapter sda1 = new SqlDataAdapter("select Studentid from [Student] where StudentId ='" + Studentid +"'", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count != 1)
                {
                    @Session["error"] = "Student not exist";
                    return View("FacultyAddStudent");
                }
                SqlDataAdapter sda2 = new SqlDataAdapter("select CourseId from [Course] where CourseId ='" + CourseId + "'", con);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                if (dt2.Rows.Count != 1)
                {
                    @Session["error"] = "Course not exist";
                    return View("FacultyAddStudent");
                }
                
                    SqlCommand sda4 = new SqlCommand("select day,startHour,endHour from Course where CourseId ='" + CourseId + "'", con);
                    sda4.Connection = con;
                    con.Open();
                    SqlDataReader sdr = sda4.ExecuteReader();

                    List<Courses> objmodel = new List<Courses>();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            var details = new Courses();
                            details.Day = sdr["day"].ToString();
                            details.startHour = TimeSpan.Parse(sdr["startHour"].ToString());
                            details.endHour = TimeSpan.Parse(sdr["endHour"].ToString());
                            objmodel.Add(details);

                        }
                        cour.info = objmodel;
                        con.Close();
                        
                    }
                    if (!CheckCoursesStu(cour,Studentid, cour.CourseId, cour.info[0].Day, cour.info[0].startHour, cour.info[0].endHour))
                    {
                        @Session["error"] = "Cannot register for course due to overlapping schedule";
                        return View("FacultyAddStudent");
                    }
                    else
                    {
                        SqlCommand sda3 = new SqlCommand(@"INSERT INTO [StudentCourses] (sicStudentId, sicCourseId) VALUES ('" + Studentid + "', '" + CourseId + "')", con);
                        SqlDataAdapter da = new SqlDataAdapter(sda3);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                    }
                
            }
            return View("FacultyAddStudent");
        }

        public ActionResult FacultyAddCourse(Courses cour)
        {
            if (Request.Form["CourseId"] != null && Request.Form["CourseName"] != null && Request.Form["LecturerId"] != null && Request.Form["Day"] != null && Request.Form["Class"] != null && Request.Form["StartHour"] != null && Request.Form["EndHour"] != null 
                && Request.Form["MoedADay"] != null && Request.Form["MoedAMonth"] != null && Request.Form["MoedAYear"] != null && Request.Form["MoedBDay"] != null && Request.Form["MoedBMonth"] != null && Request.Form["MoedBYear"] != null && Request.Form["MoedAClass"] != null && Request.Form["MoedBClass"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseId"]);
                string CourseName = Request.Form["CourseName"].ToString();
                int LecturerId = Int32.Parse(Request.Form["LecturerId"]);
                string Day = Request.Form["Day"].ToString();
                string Class = Request.Form["Class"].ToString();
                TimeSpan StartHour = TimeSpan.Parse(Request.Form["StartHour"].ToString());
                TimeSpan EndHour = TimeSpan.Parse(Request.Form["EndHour"].ToString());
                int MoedADay = Int32.Parse(Request.Form["moedADay"]);
                int MoedAMonth = Int32.Parse(Request.Form["moedAMonth"]);
                int MoedAYear = Int32.Parse(Request.Form["moedAYear"]);
                int MoedBDay = Int32.Parse(Request.Form["moedBDay"]);
                int MoedBMonth = Int32.Parse(Request.Form["moedBMonth"]);
                int MoedBYear = Int32.Parse(Request.Form["moedBYear"]);
                string MoedAClass = Request.Form["MoedAClass"].ToString();
                string MoedBClass = Request.Form["MoedBClass"].ToString();
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                SqlDataAdapter sda = new SqlDataAdapter("select courseid from course where courseid='" + CourseId + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    @Session["error"] = "Course already exist";
                    return View("FacultyAddCourse");
                }
                else
                {
                    if (!CheckCoursesLec(cour, LecturerId, CourseId, Day, StartHour, EndHour))
                    {
                        @Session["error"] = "Lecturer Cannot register for this course due to overlapping schedule";
                        return View("FacultyAddCourse");
                    }
                    else
                    {
                         con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                        SqlCommand sda1 = new SqlCommand(@"INSERT INTO [Course] (CourseId, coursename ,LecturerId, Day,startHour,endHour,class,moedAYear,moedAMonth,moedADay,classA,moedBYear,moedBMonth,moedBDay,classb) VALUES ('" + CourseId + "', '" + CourseName + "', '" + LecturerId + "', '" + Day + "', '" + StartHour + "', '" + EndHour + "', '" + Class + "', '" + MoedAYear + "', '" + MoedAMonth + "', '" + MoedADay + "', '" + MoedAClass + "', '" + MoedBYear + "', '" + MoedBMonth + "', '" + MoedBDay + "', '" + MoedBClass + "')", con);
                        SqlDataAdapter da = new SqlDataAdapter(sda1);
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        con.Close();
                    }
                }

            }

            return View("FacultyAddCourse");
        }
        public bool CheckCoursesStu(Courses cour,int Studentid ,int courseId, string day, TimeSpan startHour, TimeSpan endHour)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
            SqlCommand sda1 = new SqlCommand("select f.starthour,f.endhour from course c,(select day, starthour, endhour from course, (select siccourseid from StudentCourses where sicStudentId = '" + Studentid + "')d where d.sicCourseId=CourseId ) f where f.day=c.day and CourseId= '" + courseId + "'", con);
            sda1.Connection = con;
            con.Open();
            SqlDataReader sdr = sda1.ExecuteReader();

            List<Courses> objmodel = new List<Courses>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var details = new Courses();
                    details.startHour = TimeSpan.Parse(sdr["startHour"].ToString());
                    details.endHour = TimeSpan.Parse(sdr["endHour"].ToString());
                    objmodel.Add(details);

                }
                cour.info = objmodel;
                con.Close();
            }
            else
            {
                cour.info= objmodel;
            }


            int i = 0;
            while (i < cour.info.Count())
            {

                bool a1 = (startHour >= cour.info[i].endHour) || (endHour <= cour.info[i].startHour);
                if (!a1)
                    return false;
                i++;
            }
            return true;
        }
        public bool CheckCoursesLec(Courses cour,int LecturerId, int courseId, string day, TimeSpan startHour, TimeSpan endHour)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
            SqlCommand sda1 = new SqlCommand("select DISTINCT f.starthour,f.endhour from course c,(select day,starthour,endhour from course where LecturerId= '" + LecturerId + "')f where f.Day = '" + day + "'", con);
            sda1.Connection = con;
            con.Open();
            SqlDataReader sdr = sda1.ExecuteReader();

            List<Courses> objmodel = new List<Courses>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var details = new Courses();
                    details.startHour = TimeSpan.Parse(sdr["startHour"].ToString());
                    details.endHour = TimeSpan.Parse(sdr["endHour"].ToString());
                    objmodel.Add(details);

                }
                cour.info = objmodel;
                con.Close();
            }
            else
            {
                cour.info = objmodel;
            }


            int i = 0;
            while (i < cour.info.Count())
            {

                bool a1 = (startHour >= cour.info[i].endHour) || (endHour <= cour.info[i].startHour);
                if (!a1)
                    return false;
                i++;
            }
            return true;
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

                SqlDataAdapter sda = new SqlDataAdapter("select courseid from course where courseid='" + CourseId + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count != 1)
                {
                    @Session["error"] = "Course Not exist";
                    return View("FacultyUpdateCourse");
                }
                else
                {

                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "update [Course] set Starthour = '" + StartHour + "', EndHour='" + EndHour + "', Day='" + Day + "' where CourseID = '" + CourseId + "'";
                    sqlcomm.Connection = con;
                    sqlcomm.ExecuteNonQuery();
                    con.Close();
                }
            }
            return View("FacultyUpdateCourse");

        }

        public ActionResult FacultyUpdateExamsSchedule()
        {

            if (Request.Form["CourseIdTB"] != null && Request.Form["moedADay"] != null && Request.Form["moedAMonth"] != null && Request.Form["moedAYear"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseIdTB"]);
                int MoedADay = Int32.Parse(Request.Form["moedADay"]);
                int MoedAMonth = Int32.Parse(Request.Form["moedAMonth"]);
                int MoedAYear = Int32.Parse(Request.Form["moedAYear"]);

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                con.Open();

                SqlDataAdapter sda = new SqlDataAdapter("select CourseId from [Course] where CourseId ='" + CourseId + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "update [Course] set moedAYear = '" + MoedAYear + "', moedAMonth='" + MoedAMonth + "', moedADay='" + MoedADay + "' where CourseId ='" + CourseId + "'";
                    sqlcomm.Connection = con;
                    sqlcomm.ExecuteNonQuery();

                    con.Close();
                }
                else
                {
                    @Session["error"] = "Course Not exist";
                    return View("FacultyUpdateExamsSchedule");
                }
            }
            else if (Request.Form["CourseIdTB"] != null && Request.Form["moedBDay"] != null && Request.Form["moedBMonth"] != null && Request.Form["moedBYear"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseIdTB"]);
                int MoedBDay = Int32.Parse(Request.Form["moedBDay"]);
                int MoedBMonth = Int32.Parse(Request.Form["moedBMonth"]);
                int MoedBYear = Int32.Parse(Request.Form["moedBYear"]);

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");
                SqlDataAdapter sda = new SqlDataAdapter("select CourseId from [Course] where CourseId ='" + CourseId + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    con.Open();
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "update [Course] set moedBYear = '" + MoedBYear + "', moedBMonth='" + MoedBMonth + "', moedBDay='" + MoedBDay + "' where CourseId ='" + CourseId + "'";
                    sqlcomm.Connection = con;
                    sqlcomm.ExecuteNonQuery();

                    con.Close();
                }
                else
                {
                    @Session["error"] = "Course Not exist";
                    return View("FacultyUpdateExamsSchedule");
                }
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


                SqlDataAdapter sda1 = new SqlDataAdapter("select Studentid from [Student] where StudentId ='" + StudentId + "'", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count != 1)
                {
                    @Session["error"] = "Student not exist";
                    return View("FacultyUpdateExamsGrades");
                }
                SqlDataAdapter sda2 = new SqlDataAdapter("select CourseId from [Course] where CourseId ='" + CourseId + "'", con);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                if (dt2.Rows.Count != 1)
                {
                    @Session["error"] = "Course not exist";
                    return View("FacultyUpdateExamsGrades");
                }

                SqlDataAdapter sda = new SqlDataAdapter("select sicStudentId,sicCourseId from [StudentCourses] where sicStudentId ='" + StudentId + "' and sicCourseId ='" + CourseId + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count == 1)
                {
                    con.Open();
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.CommandText = "update StudentCourses set GradeB= '" + NewGradeB + "'where sicStudentId =  '" + StudentId + "'and sicCourseId = (select CourseId from Course where ((moedBYear = '"+ DateTime.Now.Year + "' and moedBMonth = '" + DateTime.Now.Month + "' and moedBDay <= '" + DateTime.Now.Day + "') or (moedBYear = '" + DateTime.Now.Year + "' and moedBMonth < '" + DateTime.Now.Month+ "') or ( moedBYear < '" + DateTime.Now.Year + "')) and CourseId =  '" + CourseId + "')";
                    sqlcomm.Connection = con;
                    sqlcomm.ExecuteNonQuery();

                    con.Close();
                }
                else
                {
                    @Session["error"] = "The student not sign to this course";
                    return View("FacultyUpdateExamsGrades");
                }
            }
            else if (Request.Form["CourseIdTB"] != null && Request.Form["StudentIdTB"] != null && Request.Form["NewGradeATB"] != null)
            {
                int CourseId = Int32.Parse(Request.Form["CourseIdTB"]);
                int StudentId = Int32.Parse(Request.Form["StudentIdTB"]);
                int NewGradeA = Int32.Parse(Request.Form["NewGradeATB"]);
                var dateString = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-23GVLKN;database=WPF;Integrated Security=SSPI");


                SqlDataAdapter sda1 = new SqlDataAdapter("select Studentid from [Student] where StudentId ='" + StudentId + "'", con);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);
                if (dt1.Rows.Count != 1)
                {
                    @Session["error"] = "Student not exist";
                    return View("FacultyUpdateExamsGrades");
                }
                SqlDataAdapter sda2 = new SqlDataAdapter("select CourseId from [Course] where CourseId ='" + CourseId + "'", con);
                DataTable dt2 = new DataTable();
                sda2.Fill(dt2);
                if (dt2.Rows.Count != 1)
                {
                    @Session["error"] = "Course not exist";
                    return View("FacultyUpdateExamsGrades");
                }


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

                    @Session["error"] = "The student not sign to this course";
                    return View("FacultyUpdateExamsGrades");
                }
            }

            return View("FacultyUpdateExamsGrades");
        }
    }

}