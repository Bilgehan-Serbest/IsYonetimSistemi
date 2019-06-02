using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsYonetimSistemi.Models;

namespace IsYonetimSistemi.Controllers
{
    public class YoneticiGirisController : Controller
    {
        // GET: YoneticiGiris
        public ActionResult YoneticiGiris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GirisYap(IsYonetimSistemi.Models.Yonetici yoneticiModel)
        {
            using (IsYonetimDBEntities db = new IsYonetimDBEntities())
            {
                var yoneticiDetay = db.Yoneticis.Where(x => x.kullanici_adi == yoneticiModel.kullanici_adi && x.parola == yoneticiModel.parola).FirstOrDefault();
                if (yoneticiDetay == null)
                {
                    ModelState.AddModelError("", "Hatalı Kullanıcı Adı ve/veya Parola");
                    return View("YoneticiGiris", yoneticiModel);
                }
                else
                {
                    Session["kullaniciID"] = yoneticiDetay.kullanici_id;
                    Session["yoneticiAd"] = yoneticiDetay.ad;
                    Session["yoneticiSoyad"] = yoneticiDetay.soyad;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult CikisYap()
        {
            int kullaniciID = (int)Session["kullaniciID"];
            Session.Abandon();
            return RedirectToAction("YoneticiGiris", "YoneticiGiris");
        }
    }
}