using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsYonetimSistemi.Models;
using System.Net;
using System.Data.Entity;

namespace IsYonetimSistemi.Controllers
{
    public class PersonelController : Controller
    {
        private IsYonetimDBEntities db = new IsYonetimDBEntities();
        // GET: Personel
        public ActionResult Index()
        {
            return View(db.Personels.ToList());
        }
        public ActionResult PersonelListesi_p()
        {
            return View(db.Personels.ToList());
        }

        public ActionResult PersonelGorevleri()
        {
            int personelID = (int)System.Web.HttpContext.Current.Session["personelID"];
            List<Gorevlendirme> tumGorevler = db.Gorevlendirmes.ToList();
            List<Gorevlendirme> personelGorevleri = new List<Gorevlendirme>();
            foreach (Gorevlendirme gorev in tumGorevler){
                if (gorev.personel_id == personelID)
                    personelGorevleri.Add(gorev);
            }
            return View(personelGorevleri);
        }

        public ActionResult PersonelIzinleri()
        {
            int personelID = (int)System.Web.HttpContext.Current.Session["personelID"];
            List<Izin> tumIzinler = db.Izins.ToList();
            List<Izin> personelIzinleri = new List<Izin>();
            foreach (Izin izin in tumIzinler)
            {
                if (izin.personel_id == personelID)
                    personelIzinleri.Add(izin);
            }
            return View(personelIzinleri);
        }
        public ActionResult Detay(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personel personel= db.Personels.Find(id);
            if (personel == null)
            {
                return HttpNotFound();
            }
            return View(personel);
        }

        // GET: Personels/Düzenle/5
        public ActionResult Düzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personel personel = db.Personels.Find(id);
            if (personel == null)
            {
                return HttpNotFound();
            }
            return View(personel);
        }

        // POST: Personels/Düzenle/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Düzenle([Bind(Include = "kullanici_id,ad,soyad,email,maas")] Personel personel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personel);
        }

        // GET: Personels/Sil/5
        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personel personel = db.Personels.Find(id);
            if (personel == null)
            {
                return HttpNotFound();
            }
            return View(personel);
        }

        // POST: Personels/Sil/5
        [HttpPost, ActionName("Sil")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personel personel = db.Personels.Find(id);
            db.Personels.Remove(personel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}