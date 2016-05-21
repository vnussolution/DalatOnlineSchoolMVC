﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DalatOnlineSchool.DAL;
using DalatOnlineSchool.Models;
using System.Data.Entity.Infrastructure;

namespace DalatOnlineSchool.Controllers
{
    public class DepartmentsController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Departments
        public async Task<ActionResult> Index()
        {
            var departments = db.Departments.Include(d => d.Administrator);
            return View(await departments.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartmentID,Name,Budget,StartDate,InstructorID")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DepartmentID,Name,Budget,StartDate,RowVersion,InstructorID")] Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(department).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            // throw concurrency exception
            catch (DbUpdateConcurrencyException ex)
            {
                var entry = ex.Entries.Single();
                var clientValues = (Department) entry.Entity;
                var databaseEntry = entry.GetDatabaseValues();
                if (databaseEntry == null)
                {
                    ModelState.AddModelError(string.Empty,"Unable to save changes. The department was deleted by another user");
                }
                else
                {
                    var databaseValues = (Department) databaseEntry.ToObject();
                    if(databaseValues.Name!=clientValues.Name) ModelState.AddModelError("Name", "Current value: "+databaseValues.Name);
                    if(databaseValues.Budget!=clientValues.Budget) ModelState.AddModelError("Budget","Current value: "+String.Format("{0:c}",databaseValues.Budget));
                    if(databaseValues.StartDate != clientValues.StartDate) ModelState.AddModelError("StartDate","Current value:" + String.Format("{0:d}",databaseValues.StartDate));
                    if(databaseValues.InstructorID != clientValues.InstructorID) ModelState.AddModelError("Administrator","Current value: " + db.Instructors.Find(databaseValues.InstructorID).FullName);
                    ModelState.AddModelError(string.Empty, "The record you attemptedto edit "+ "was modified by another user after you got the original value.The "
                            + "edit operation was canceled and the current values in the database " + "have been displayed. If you still want to edit this record, click " + "the Save button again. Otherwise click the Back to List hyperlink.");
                    department.RowVersion = databaseValues.RowVersion; // this avoids the error message showing up again next time unless it actuallys happens again

                }
            }
  
            ViewBag.InstructorID = new SelectList(db.Instructors, "ID", "FullName", department.InstructorID);
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            if (concurrencyError.GetValueOrDefault())
            {
                if (department == null)
                {
                    ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete "+ "was deleted by another user after you got the original values. " + "Click the Back to List hyperlink.";
                }
                else
                {
                    ViewBag.ConcurrencyErrorMessage = "The record you attempted to delete " + "was modified by another user after you got the original values. "
                + "The delete operation was canceled and the current values in the "+ "database have been displayed. If you still want to delete this "
                + "record, click the Delete button again. Otherwise "
                + "click the Back to List hyperlink.";
                }
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Department department)
        {
            try
            {
                db.Entry(department).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new {concurrencyError = true});
            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "Unable to delete. Try again,and if the problem persists contact your system administrator.");
                return View(department);
            }     

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