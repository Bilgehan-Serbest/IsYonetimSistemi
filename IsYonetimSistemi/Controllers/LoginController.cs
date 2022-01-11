using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsYonetimSistemi.Models;
using System.Web.Helpers;
using System.Data.Entity;

namespace IsYonetimSistemi.Controllers
{
    public class LoginController : Controller
    {
        // GET: ManagerLogin
        public ActionResult ManagerLogin()
        {
            return View();
        }
        public ActionResult PersonnelLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ManagerLogin(IsYonetimSistemi.Models.Manager managerModel)
        {
            using (IsYonetimDBEntities db = new IsYonetimDBEntities())
            {
                if(managerModel.password==null || managerModel.username==null){
                    ModelState.AddModelError("", "Hatalı Kullanıcı Adı ve/veya Parola");
                    managerModel.password = "";
                    return View("ManagerLogin", managerModel);
                }

                

                managerModel.password = Crypto.Hash(managerModel.password);
                var managerDetail = db.Managers.Where(x => x.username == managerModel.username && x.password == managerModel.password).FirstOrDefault();

                if (managerDetail == null){
                    ModelState.AddModelError("", "Hatalı Kullanıcı Adı ve/veya Parola");
                    managerModel.password = "";
                    return View("ManagerLogin", managerModel);
                }
                else{
                    Session["managerID"] = managerDetail.user_id;
                    Session["managerFName"] = managerDetail.first_name;
                    Session["managerLName"] = managerDetail.last_name;
                    return RedirectToAction("ManagerHomePage", "Home");
                }
            }
        }

        public ActionResult ManagerLogout()
        {
            int kullaniciID = (int)Session["ManagerID"];
            Session.Abandon();
            return RedirectToAction("ManagerLogin", "Login");
        }

        [HttpPost]
        //[MultipleButton(Name = "action", Argument = "PersonelLogin")]
        public ActionResult PersonnelLogin(IsYonetimSistemi.Models.Personnel personnelModel)
        {
            using (IsYonetimDBEntities db = new IsYonetimDBEntities())
            {
                if (personnelModel.password == null || personnelModel.username == null)
                {
                    ModelState.AddModelError("", "Hatalı Kullanıcı Adı ve/veya Parola");
                    personnelModel.password = "";
                    return View("PersonnelLogin", personnelModel);
                }

                personnelModel.password = Crypto.Hash(personnelModel.password);
                var personnelDetail = db.Personnels.Where(x => x.username == personnelModel.username && x.password == personnelModel.password).FirstOrDefault();
                if (personnelDetail == null)
                {
                    ModelState.AddModelError("", "Hatalı Kullanıcı Adı ve/veya password");
                    personnelModel.password = "";
                    return View("PersonnelLogin", personnelModel);
                }
                else
                {
                    Session["personnelID"] = personnelDetail.user_id;
                    Session["personnelFName"] = personnelDetail.first_name;
                    Session["PersonnelLName"] = personnelDetail.last_name;
                    return RedirectToAction("PersonnelHomePage", "Home");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult ResetPersonnelPassword(Personnel personnelModel)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                var personnelDetail = dbModel.Personnels.AsNoTracking().Where(x => x.username == personnelModel.username && x.email == personnelModel.email).FirstOrDefault();
                if (personnelDetail == null)
                {
                    //ModelState.AddModelError(string.Empty, "Hatalı Kullanıcı Adı ve/veya E-mail");
                    ViewBag.DuplicateMessage = "Hatalı Kullanıcı Adı ve/veya E-mail";
                }
                else
                {
                    personnelModel.user_id = Convert.ToInt32(personnelDetail.user_id);
                    personnelModel.username = personnelDetail.username.ToString();                        
                    personnelModel.first_name = personnelDetail.first_name.ToString();
                    personnelModel.last_name = personnelDetail.last_name.ToString();
                    personnelModel.email = personnelDetail.email.ToString();
                    personnelModel.salary = Convert.ToInt32(personnelDetail.salary);
                    personnelModel.password = Crypto.Hash(personnelModel.password);
                    personnelModel.confirm_password = Crypto.Hash(personnelModel.confirm_password);
                    dbModel.Entry(personnelModel).State = EntityState.Modified;
                    dbModel.SaveChanges();
                    personnelModel.password="";
                    ViewBag.SuccessMessage = "Parola Yenilendi.";
                    return View("PersonnelLogin", personnelModel);
            }
                //ModelState.AddModelError(string.Empty, "Hatalı Kullanıcı Adı ve/veya E-mail");
                ViewBag.DuplicateMessage = "Hatalı Kullanıcı Adı ve/veya E-mail";
                return View("PersonnelLogin", personnelModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetManagerPassword( Manager managerModel)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                var managerDetail = dbModel.Managers.AsNoTracking().Where(x => x.username == managerModel.username && x.email == managerModel.email).FirstOrDefault();
                if (managerDetail == null)
                {
                    ModelState.AddModelError(string.Empty, "Hatalı Kullanıcı Adı ve/veya E-mail");
                    //ViewBag.DuplicateMessage = "Hatalı Kullanıcı Adı ve/veya E-mail";
                }
                else
                {
                    managerModel.user_id = Convert.ToInt32(managerDetail.user_id);
                    managerModel.username = managerDetail.username.ToString();
                    managerModel.first_name = managerDetail.first_name.ToString();
                    managerModel.last_name = managerDetail.last_name.ToString();
                    managerModel.email = managerDetail.email.ToString();
                    managerModel.salary = Convert.ToInt32(managerDetail.salary);
                    managerModel.password = Crypto.Hash(managerModel.password);
                    managerModel.confirm_password = Crypto.Hash(managerModel.confirm_password);
                    dbModel.Entry(managerModel).State = EntityState.Modified;
                    dbModel.SaveChanges();
                    managerModel.password = "";
                    ViewBag.SuccessMessage = "Parola Yenilendi.";
                    return View("ManagerLogin", managerModel);
                }
                ModelState.AddModelError(string.Empty, "Hatalı Kullanıcı Adı ve/veya E-mail");
                //ViewBag.DuplicateMessage = "Hatalı Kullanıcı Adı ve/veya E-mail";
                return View("ManagerLogin", managerModel);
            }
        }

        public ActionResult PersonnelLogout()
        {
            int kullaniciID = (int)Session["personnelID"];
            Session.Abandon();
            return RedirectToAction("PersonnelLogin", "Login");
        }
    }
}