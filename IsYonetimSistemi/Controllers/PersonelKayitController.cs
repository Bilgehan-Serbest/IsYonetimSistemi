using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsYonetimSistemi.Models;

namespace IsYonetimSistemi.Controllers
{
    public class PersonelKayitController : Controller
    {
        // GET: PersonelKayit
        public ActionResult PersonelKayit(int id = 0)
        {
            Personel personel = new Models.Personel();
            return View(personel);
        }
        [HttpPost]
        public ActionResult PersonelKayit(Personel personelModel)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                if (dbModel.Personels.Any(x => x.kullanici_adi == personelModel.kullanici_adi))
                {
                    ViewBag.DuplicateMessage = "Kullanıcı adı bir başka kullanıcı tarafından kullanılıyor.";
                    return View("PersonelKayit", personelModel);
                }

                dbModel.Personels.Add(personelModel);
                dbModel.SaveChanges();

                ModelState.Clear();
                ViewBag.SuccessMessage = "Personel Kaydedildi.";
                return View("PersonelKayit", new Personel());
            }
        }
    }
}