using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsYonetimSistemi.Models;
using System.Web.Helpers;


namespace IsYonetimSistemi.Controllers
{
    public class PersonnelRegisterController : Controller
    {
        // GET: PersonnelRegister
        public ActionResult PersonnelRegister(int id = 0)
        {
            Personnel personnel = new Models.Personnel();
            return View(personnel);
        }
        [HttpPost]
        public ActionResult PersonnelRegister(Personnel personnelModel)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                if (dbModel.Personnels.Any(x => x.username == personnelModel.username))
                {
                    ViewBag.DuplicateMessage = "Kullanıcı adı bir başka kullanıcı tarafından kullanılıyor.";
                    return View("PersonnelRegister", personnelModel);
                }

                personnelModel.password = Crypto.Hash(personnelModel.password);
                personnelModel.confirm_password = Crypto.Hash(personnelModel.confirm_password);
                dbModel.Personnels.Add(personnelModel);
                dbModel.SaveChanges();

                ModelState.Clear();
                ViewBag.SuccessMessage = "Personel Kaydedildi.";
                return View("PersonnelRegister", new Personnel());
            }
        }
    }
}