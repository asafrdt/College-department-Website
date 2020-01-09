using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_new.Models;
using project_new.Controllers;
using project_new.ViewModel;
using project_new.Dal;

namespace project_new.Controllers
{
    public class AccountController : Controller
    {

        //[HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult StudentHomePage()
        {
            return View("StudentHomePage");
        }
        public ActionResult Verify()
        {

            AccountDal dal = new AccountDal();
            string username = Request.Form["Name"].ToString();
            string userpassword = Request.Form["Password"].ToString();
            List<Account> acc = (from x in dal.Users where x.UserName==username && x.Password == userpassword select x).ToList<Account>();
            AccountViewModel usr = new AccountViewModel();
            usr.usersName = acc;
            if (usr.usersName.Count == 0)
            {
                TempData["Message"] = "Login has been failed, username or password does not exist";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Account login_usr = new Account();
                login_usr = usr.usersName[0];
                Session["Username"] = login_usr.UserName;
                Session["Password"] = login_usr.Password;
                Session["Type"] = login_usr.type;
                Session["ID"] = login_usr.UserId;

                if (login_usr.type == "student")
                {
                    return View("StudentHomePage", login_usr);
                }
                if (login_usr.type == "facultyAdmin")
                {
                    return View("FacultyHomePage", login_usr);
                }
                if (login_usr.type == "lecturer")
                {
                    return View("LecturerHomePage", login_usr);
                }
                else
                {
                    return View("Error", login_usr);
                }

            }
        }
    }
}