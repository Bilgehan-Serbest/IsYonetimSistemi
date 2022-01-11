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
    public class TaskController : Controller
    {
        private IsYonetimDBEntities db = new IsYonetimDBEntities(); 
        // GET: Task
        public ActionResult GiveTask()
        {
            IsYonetim isYonetim = new Models.IsYonetim();
            ViewBag.personnelList = db.Personnels.ToList();
            return View(isYonetim);
        }

        public ActionResult ListTasks()
        {
            List<Task> currentTasks = db.Tasks.ToList();
            return View(currentTasks);
        }

        [HttpPost]
        public ActionResult GiveTask(int[] PersonnelIDs, IsYonetim isYonetim)
        {
            using (IsYonetimDBEntities dbModel = new IsYonetimDBEntities())
            {
                if (PersonnelIDs == null)
                {
                    ViewBag.DuplicateMessage = "Görev atanacak personnel seçilmedi.";
                    ViewBag.personnelList = db.Personnels.ToList();
                    return View("GiveTask", isYonetim);              
                }
                else
                {              
                    foreach (int personnelID in PersonnelIDs)
                    {
                        ModelState.Clear();
                        Task newTask = new Task() { manager_id = isYonetim.taskViewModel.manager_id, personnel_id = personnelID,
                            task_name = isYonetim.taskViewModel.task_name, task_detail = isYonetim.taskViewModel.task_detail };                        
                        dbModel.Tasks.Attach(newTask);
                        dbModel.Tasks.Add(newTask);
                        dbModel.SaveChanges();                        
                    }                    
                    ViewBag.SuccessMessage = "Gorev Atandi.";
                    ViewBag.personnelList = db.Personnels.ToList();
                    return View("GiveTask", isYonetim);
                }
            }
        }

        // GET: Tasks/Edit/5
        public ActionResult EditTask(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            Task task = db.Tasks.Find(id);            
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.personnelList = db.Personnels.ToList();            
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTask(int PersonnelID, [Bind(Include = "task_id,manager_id,task_name,task_detail")] Task task)
        {
            if (ModelState.IsValid)
            {
                task.personnel_id = PersonnelID;
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListTasks");
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult DeleteTask(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("DeleteTask")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("ListTasks");
        }

    }
        
}