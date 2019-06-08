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
                yoneticiModel.parola = Crypto.Hash(yoneticiModel.parola);
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
        //[MultipleButton(Name = "action", Argument = "PersonelGiris")]
        public ActionResult PersonelGiris(IsYonetimSistemi.Models.Personel personelModel)
        {
            using (IsYonetimDBEntities db = new IsYonetimDBEntities())
            {
                personelModel.parola = Crypto.Hash(personelModel.parola);
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

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult PersonelParolaYenile([Bind(Include = "kullanici_adi,parola,parola_dogrula,email")] Personel personelModel)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                var personelDetay = dbModel.Personels.AsNoTracking().Where(x => x.kullanici_adi == personelModel.kullanici_adi && x.email == personelModel.email).FirstOrDefault();
                if (personelDetay == null)
                {
                    //ModelState.AddModelError(string.Empty, "Hatalı Kullanıcı Adı ve/veya E-mail");
                    ViewBag.DuplicateMessage = "Hatalı Kullanıcı Adı ve/veya E-mail";
                }
                else
                {
                    personelModel.kullanici_id = Convert.ToInt32(personelDetay.kullanici_id);
                    personelModel.kullanici_adi = personelDetay.kullanici_adi.ToString();                        
                    personelModel.ad = personelDetay.ad.ToString();
                    personelModel.soyad = personelDetay.soyad.ToString();
                    personelModel.email = personelDetay.email.ToString();
                    personelModel.maas = Convert.ToInt32(personelDetay.maas);
                    personelModel.parola = Crypto.Hash(personelModel.parola);
                    personelModel.parola_dogrula = Crypto.Hash(personelModel.parola_dogrula);
                    dbModel.Entry(personelModel).State = EntityState.Modified;
                    dbModel.SaveChanges();
                    ViewBag.SuccessMessage = "Parola Yenilendi.";
                    return View("PersonelGiris", personelModel);
            }
                //ModelState.AddModelError(string.Empty, "Hatalı Kullanıcı Adı ve/veya E-mail");
                ViewBag.DuplicateMessage = "Hatalı Kullanıcı Adı ve/veya E-mail";
                return View("PersonelGiris", personelModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YoneticiParolaYenile([Bind(Include = "kullanici_adi,parola,parola_dogrula,email")] Yonetici yoneticiModel)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                var yoneticiDetay = dbModel.Yoneticis.AsNoTracking().Where(x => x.kullanici_adi == yoneticiModel.kullanici_adi && x.email == yoneticiModel.email).FirstOrDefault();
                if (yoneticiDetay == null)
                {
                    ModelState.AddModelError(string.Empty, "Hatalı Kullanıcı Adı ve/veya E-mail");
                    //ViewBag.DuplicateMessage = "Hatalı Kullanıcı Adı ve/veya E-mail";
                }
                else
                {
                    yoneticiModel.kullanici_id = Convert.ToInt32(yoneticiDetay.kullanici_id);
                    yoneticiModel.kullanici_adi = yoneticiDetay.kullanici_adi.ToString();
                    yoneticiModel.ad = yoneticiDetay.ad.ToString();
                    yoneticiModel.soyad = yoneticiDetay.soyad.ToString();
                    yoneticiModel.email = yoneticiDetay.email.ToString();
                    yoneticiModel.maas = Convert.ToInt32(yoneticiDetay.maas);
                    yoneticiModel.parola = Crypto.Hash(yoneticiModel.parola);
                    yoneticiModel.parola_dogrula = Crypto.Hash(yoneticiModel.parola_dogrula);
                    dbModel.Entry(yoneticiModel).State = EntityState.Modified;
                    dbModel.SaveChanges();
                    
                    ViewBag.SuccessMessage = "Parola Yenilendi.";
                    return View("YoneticiGiris", yoneticiModel);
                }
                ModelState.AddModelError(string.Empty, "Hatalı Kullanıcı Adı ve/veya E-mail");
                //ViewBag.DuplicateMessage = "Hatalı Kullanıcı Adı ve/veya E-mail";
                return View("YoneticiGiris", yoneticiModel);
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