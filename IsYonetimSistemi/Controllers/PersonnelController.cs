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
    public class PersonnelController : Controller
    {
        private IsYonetimDBEntities db = new IsYonetimDBEntities();
        // GET: Personnel
        public ActionResult Index()
        {
            return View(db.Personnels.ToList());
        }
        public ActionResult PersonnelList_p()
        {
            return View(db.Personnels.ToList());
        }

        public ActionResult PersonnelTasks()
        {
            int personnelID = (int)System.Web.HttpContext.Current.Session["personnelID"];
            List<Task> allTasks = db.Tasks.ToList();
            List<Task> personnelTasks = new List<Task>();
            foreach (Task task in allTasks){
                if (task.personnel_id == personnelID)
                    personnelTasks.Add(task);
            }
            return View(personnelTasks);
        }

        public ActionResult PersonnelLeaves()
        {
            int personnelID = (int)System.Web.HttpContext.Current.Session["personnelID"];
            List<Leave> allLeaves = db.Leaves.ToList();
            List<Leave> personnelLeaves = new List<Leave>();
            foreach (Leave leave in allLeaves)
            {
                if (leave.personnel_id== personnelID)
                    personnelLeaves.Add(leave);
            }
            return View(personnelLeaves);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel= db.Personnels.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // GET: Personnels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // POST: Personnels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,first_name,last_name,email,salary")] Personnel personnel)
        {
            var personnelToUpdate = db.Personnels.FirstOrDefault(x => x.user_id == personnel.user_id);
            if (personnelToUpdate != null)
            {
                personnelToUpdate.first_name = personnel.first_name;
                personnelToUpdate.last_name = personnel.last_name;
                personnelToUpdate.email = personnel.email;
                personnelToUpdate.salary = personnel.salary;
                personnelToUpdate.confirm_password = personnelToUpdate.password;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personnel);
        }

        // GET: Personnels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnel personnel = db.Personnels.Find(id);
            if (personnel == null)
            {
                return HttpNotFound();
            }
            return View(personnel);
        }

        // POST: Personnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Personnel personnel = db.Personnels.Find(id);
            db.Personnels.Remove(personnel);
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