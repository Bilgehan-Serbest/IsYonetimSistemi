using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult YoneticiAnaSayfa()
        {
            return View();
        }
        public ActionResult PersonelAnaSayfa()
        {
            return View();
        }
        public ActionResult GirisSecim()
        {
            return View();
        }
    }
}