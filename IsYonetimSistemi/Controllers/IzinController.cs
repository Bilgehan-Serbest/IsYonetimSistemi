﻿using IsYonetimSistemi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IsYonetimSistemi.Controllers
{
    public class IzinController : Controller
    {
        private IsYonetimDBEntities db = new IsYonetimDBEntities();
        // GET: Izin
        public ActionResult IzinVerme()
        {
            IsYonetim isYonetim = new Models.IsYonetim();
            isYonetim.izinViewModel.izin_baslangic_tarihi = DateTime.Today;
            isYonetim.izinViewModel.izin_bitis_tarihi= DateTime.Today;
            ViewBag.personelListesi = db.Personels.ToList();
            return View(isYonetim);
        }

        public ActionResult IzinListeleme()
        {
            List<Izin> mevcutIzinler = db.Izins.ToList();
            return View(mevcutIzinler);
        }

        [HttpPost]
        public ActionResult IzinVerme(int[] PersonelIDs, IsYonetim isYonetim)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                if (PersonelIDs == null)
                {
                    ViewBag.DuplicateMessage = "Izin verilecek personel seçilmedi.";
                    ViewBag.personelListesi = db.Personels.ToList();
                    return View("IzinVerme", isYonetim);
                }
                else
                {
                    foreach (int personelID in PersonelIDs)
                    {
                        ModelState.Clear();
                        Izin yeniIzin = new Izin() { yonetici_id = isYonetim.izinViewModel.yonetici_id, personel_id = personelID, izin_sebebi= isYonetim.izinViewModel.izin_sebebi, izin_baslangic_tarihi= isYonetim.izinViewModel.izin_baslangic_tarihi, izin_bitis_tarihi = isYonetim.izinViewModel.izin_bitis_tarihi };
                        dbModel.Izins.Attach(yeniIzin);
                        dbModel.Izins.Add(yeniIzin);
                        dbModel.SaveChanges();
                    }
                    ViewBag.SuccessMessage = "İzin verildi.";
                    ViewBag.personelListesi = db.Personels.ToList();
                    return View("IzinVerme", isYonetim);
                }
            }
        }
        // GET: Izins/Düzenle/5
        public ActionResult IzinDuzenleme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Izin izin= db.Izins.Find(id);
            if (izin == null)
            {
                return HttpNotFound();
            }
            ViewBag.personelListesi = db.Personels.ToList();
            return View(izin);
        }

        // POST: Izins/Düzenle/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IzinDuzenleme(int PersonelID, [Bind(Include = "izin_id,yonetici_id,izin_sebebi,izin_baslangic_tarihi,izin_bitis_tarihi")] Izin izin)
        {
            if (ModelState.IsValid)
            {
                izin.personel_id = PersonelID;
                db.Entry(izin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IzinListeleme");
            }
            return View(izin);
        }
        // GET: Izins/Sil/5
        public ActionResult IzinSilme(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Izin izin = db.Izins.Find(id);
            if (izin == null)
            {
                return HttpNotFound();
            }
            return View(izin);
        }

        // POST: Izinss/Sil/5
        [HttpPost, ActionName("IzinSilme")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Izin izin = db.Izins.Find(id);
            db.Izins.Remove(izin);
            db.SaveChanges();
            return RedirectToAction("IzinListeleme");
        }
    }
}