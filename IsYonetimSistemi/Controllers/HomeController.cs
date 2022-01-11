using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult ManagerHomePage()
        {
            return View();
        }
        public ActionResult PersonnelHomePage()
        {
            return View();
        }
        public ActionResult SelectLogin()
        {
            return View();
        }
    }
}