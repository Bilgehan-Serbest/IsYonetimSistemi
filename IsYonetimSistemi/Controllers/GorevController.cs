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
    public class GorevController : Controller
    {
        private IsYonetimDBEntities db = new IsYonetimDBEntities(); 
        // GET: Gorev
        public ActionResult GorevAtama()
        {
            IsYonetim isYonetim = new Models.IsYonetim();
            ViewBag.personelListesi = db.Personels.ToList();
            return View(isYonetim);
        }

        public ActionResult GorevListeleme()
        {
            List<Gorevlendirme> mevcutGorevler = db.Gorevlendirmes.ToList();
            return View(mevcutGorevler);
        }

        [HttpPost]
        public ActionResult GorevAtama(int[] PersonelIDs, IsYonetim isYonetim)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                if (PersonelIDs == null)
                {
                    ViewBag.DuplicateMessage = "Görev atanacak personel seçilmedi.";
                    ViewBag.personelListesi = db.Personels.ToList();
                    return View("GorevAtama", isYonetim);              
                }
                else
                {              
                    foreach (int personelID in PersonelIDs)
                    {
                        ModelState.Clear();
                        Gorevlendirme yeniGorev = new Gorevlendirme() { yonetici_id = isYonetim.gorevlendirmeViewModel.yonetici_id, personel_id = personelID, gorev_adi = isYonetim.gorevlendirmeViewModel.gorev_adi, gorev_tanimi = isYonetim.gorevlendirmeViewModel.gorev_tanimi };                        
                        dbModel.Gorevlendirmes.Attach(yeniGorev);
                        dbModel.Gorevlendirmes.Add(yeniGorev);
                        dbModel.SaveChanges();                        
                    }                    
                    ViewBag.SuccessMessage = "Gorev Atandi.";
                    ViewBag.personelListesi = db.Personels.ToList();
                    return View("GorevAtama", isYonetim);
                }
            }
        }

        // GET: Gorevs/Düzenle/5
        public ActionResult GorevDuzenleme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            Gorevlendirme gorev = db.Gorevlendirmes.Find(id);            
            if (gorev == null)
            {
                return HttpNotFound();
            }
            ViewBag.personelListesi = db.Personels.ToList();            
            return View(gorev);
        }

        // POST: Gorevs/Düzenle/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GorevDuzenleme(int PersonelID, [Bind(Include = "gorev_id,yonetici_id,gorev_adi,gorev_tanimi")] Gorevlendirme gorev)
        {
            if (ModelState.IsValid)
            {
                gorev.personel_id = PersonelID;
                db.Entry(gorev).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GorevListeleme");
            }
            return View(gorev);
        }

        // GET: Gorevs/Sil/5
        public ActionResult GorevSilme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gorevlendirme gorev = db.Gorevlendirmes.Find(id);
            if (gorev == null)
            {
                return HttpNotFound();
            }
            return View(gorev);
        }

        // POST: Gorevs/Sil/5
        [HttpPost, ActionName("GorevSilme")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gorevlendirme gorev = db.Gorevlendirmes.Find(id);
            db.Gorevlendirmes.Remove(gorev);
            db.SaveChanges();
            return RedirectToAction("GorevListeleme");
        }

    }
        
}