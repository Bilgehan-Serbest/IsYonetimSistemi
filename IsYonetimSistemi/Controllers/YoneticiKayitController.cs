using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsYonetimSistemi.Models;
using System.Web.Helpers;

namespace IsYonetimSistemi.Controllers
{
    public class YoneticiKayitController : Controller
    {
        // GET: YoneticiKayit
        public ActionResult YoneticiKayit(int id = 0)
        {
            Yonetici yonetici = new Models.Yonetici();
            return View(yonetici);
        }
        [HttpPost]
        public ActionResult YoneticiKayit(Yonetici yoneticiModel)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                if (dbModel.Yoneticis.Any(x => x.kullanici_adi == yoneticiModel.kullanici_adi))
                {
                    ViewBag.DuplicateMessage = "Kullanıcı adı bir başka kullanıcı tarafından kullanılıyor.";
                    return View("YoneticiKayit", yoneticiModel);
                }
                yoneticiModel.parola_dogrula = Crypto.Hash(yoneticiModel.parola_dogrula);
                yoneticiModel.parola = Crypto.Hash(yoneticiModel.parola);
                dbModel.Yoneticis.Add(yoneticiModel);
                dbModel.SaveChanges();

                ModelState.Clear();
                ViewBag.SuccessMessage = "Yonetici Kaydedildi.";
                return View("YoneticiKayit", new Yonetici());
            }
        }
    }
}