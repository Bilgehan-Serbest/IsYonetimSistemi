using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsYonetimSistemi.Models;
using System.Web.Helpers;

namespace IsYonetimSistemi.Controllers
{
    public class ManagerRegisterController : Controller
    {
        // GET: ManagerRegister
        public ActionResult ManagerRegister(int id = 0)
        {
            Manager manager = new Models.Manager();
            return View(manager);
        }
        [HttpPost]
        public ActionResult ManagerRegister(Manager managerModel)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                if (dbModel.Managers.Any(x => x.username == managerModel.username))
                {
                    ViewBag.DuplicateMessage = "Kullanıcı adı bir başka kullanıcı tarafından kullanılıyor.";
                    return View("ManagerRegister", managerModel);
                }
                managerModel.password = Crypto.Hash(managerModel.password);
                managerModel.confirm_password = Crypto.Hash(managerModel.confirm_password);
                dbModel.Managers.Add(managerModel);
                dbModel.SaveChanges();

                ModelState.Clear();
                ViewBag.SuccessMessage = "Yonetici Kaydedildi.";
                return View("ManagerRegister", new Manager());
            }
        }
    }
}