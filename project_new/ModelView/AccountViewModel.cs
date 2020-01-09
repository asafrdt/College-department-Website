using project_new.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace project_new.ViewModel
{
    public class AccountViewModel
    {
        public Account userName { get; set; }
        public List<Account> usersName { get; set; }
    }
}