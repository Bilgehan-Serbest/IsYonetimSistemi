using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IsYonetimSistemi.Models;
using System.Net;

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

        // GET: Personels/Sil/5
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

        // POST: Personels/Sil/5
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