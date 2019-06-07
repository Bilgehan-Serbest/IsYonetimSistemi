using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsYonetimSistemi.Models;

namespace IsYonetimSistemi.Controllers
{
    public class GirisController : Controller
    {
        // GET: YoneticiGiris
        public ActionResult YoneticiGiris()
        {
            return View();
        }
        public ActionResult PersonelGiris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YoneticiGiris(IsYonetimSistemi.Models.Yonetici yoneticiModel)
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
                    Session["yoneticiID"] = yoneticiDetay.kullanici_id;
                    Session["yoneticiAd"] = yoneticiDetay.ad;
                    Session["yoneticiSoyad"] = yoneticiDetay.soyad;
                    return RedirectToAction("YoneticiAnaSayfa", "Home");
                }
            }
        }

        public ActionResult YoneticiCikis()
        {
            int kullaniciID = (int)Session["yoneticiID"];
            Session.Abandon();
            return RedirectToAction("YoneticiGiris", "Giris");
        }

        [HttpPost]
        public ActionResult PersonelGiris(IsYonetimSistemi.Models.Personel personelModel)
        {
            using (IsYonetimDBEntities db = new IsYonetimDBEntities())
            {
                var personelDetay = db.Personels.Where(x => x.kullanici_adi == personelModel.kullanici_adi && x.parola == personelModel.parola).FirstOrDefault();
                if (personelDetay == null)
                {
                    ModelState.AddModelError("", "Hatalı Kullanıcı Adı ve/veya Parola");
                    return View("PersonelGiris", personelModel);
                }
                else
                {
                    Session["personelID"] = personelDetay.kullanici_id;
                    Session["personelAd"] = personelDetay.ad;
                    Session["personelSoyad"] = personelDetay.soyad;
                    return RedirectToAction("PersonelAnaSayfa", "Home");
                }
            }
        }

        public ActionResult PersonelCikis()
        {
            int kullaniciID = (int)Session["personelID"];
            Session.Abandon();
            return RedirectToAction("PersonelGiris", "Giris");
        }
    }
}