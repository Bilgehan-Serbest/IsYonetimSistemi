using IsYonetimSistemi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace IsYonetimSistemi.Controllers
{
    public class LeaveController : Controller
    {
        private IsYonetimDBEntities db = new IsYonetimDBEntities();
        // GET: Leave
        public ActionResult GiveLeave()
        {
            IsYonetim isYonetim = new Models.IsYonetim();
            isYonetim.leaveViewModel.leave_start_date = DateTime.Today;
            isYonetim.leaveViewModel.leave_end_date= DateTime.Today;
            ViewBag.personnelList = db.Personnels.ToList();
            return View(isYonetim);
        }

        public ActionResult ListLeaves()
        {
            List<Leave> currentLeaves = db.Leaves.ToList();
            return View(currentLeaves);
        }

        [HttpPost]
        public ActionResult GiveLeave(int[] PersonnelIDs, IsYonetim isYonetim)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                if (PersonnelIDs == null)
                {
                    ViewBag.DuplicateMessage = "Izin verilecek personel seçilmedi.";
                    ViewBag.personnelList = db.Personnels.ToList();
                    return View("GiveLeave", isYonetim);
                }
                else
                {
                    foreach (int personnelID in PersonnelIDs)
                    {
                        ModelState.Clear();
                        Leave newLeave = new Leave() { manager_id = isYonetim.leaveViewModel.manager_id, personnel_id= personnelID,
                            leave_reason = isYonetim.leaveViewModel.leave_reason, leave_start_date= isYonetim.leaveViewModel.leave_start_date,
                            leave_end_date= isYonetim.leaveViewModel.leave_end_date};
                        dbModel.Leaves.Attach(newLeave);
                        dbModel.Leaves.Add(newLeave);
                        dbModel.SaveChanges();
                    }
                    ViewBag.SuccessMessage = "İzin verildi.";
                    ViewBag.personnelList = db.Personnels.ToList();
                    return View("GiveLeave", isYonetim);
                }
            }
        }
        // GET: Leaves/Edit/5
        public ActionResult EditLeave(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            ViewBag.personnelList = db.Personnels.ToList();
            return View(leave);
        }

        // POST: Leaves/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLeave(int PersonnelID, [Bind(Include = "leave_id,manager_id,leave_reason,leave_start_date,leave_end_date")] Leave leave)
        {            
            if (ModelState.IsValid)
            {
                leave.personnel_id = PersonnelID;
                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListLeaves");
            }
            return View(leave);
        }
        // GET: Leaves/Delete/5
        public ActionResult DeleteLeave(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Leave leave = db.Leaves.Find(id);
            if (leave == null)
            {
                return HttpNotFound();
            }
            return View(leave);
        }

        // POST: Leaves/Delete/5
        [HttpPost, ActionName("DeleteLeave")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Leave leave = db.Leaves.Find(id);
            db.Leaves.Remove(leave);
            db.SaveChanges();
            return RedirectToAction("ListLeaves");
        }
    }
}