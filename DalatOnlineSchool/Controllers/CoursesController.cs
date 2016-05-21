using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DalatOnlineSchool.DAL;
using DalatOnlineSchool.Models;

namespace DalatOnlineSchool.Controllers
{
    public class CoursesController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Courses
        public ActionResult Index(int? SelectedDepartment)
        {
            // eager loading - include
            //var courses = db.Courses.Include(c => c.Department);
            //return View(courses.ToList());


            var departments = db.Departments.OrderBy(q => q.Name).ToList();
            ViewBag.SelectedDepartment = new SelectList(departments, "DepartmentID","Name", SelectedDepartment);
            int departmentID = SelectedDepartment.GetValueOrDefault();
            IQueryable<Course> courses = db.Courses.Where(c => !SelectedDepartment.HasValue || c.DepartmentID ==departmentID)
            .OrderBy(d => d.CourseID)
            .Include(d => d.Department);
            var sql = courses.ToString();
            return View(courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        //// GET: Courses/Create
        //public ActionResult Create()
        //{
        //    ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
        //    return View();
        //}

        //// POST: Courses/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Courses.Add(course);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);
        //    return View(course);
        //}

        //// GET: Courses/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Course course = db.Courses.Find(id);
        //    if (course == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);
        //    return View(course);
        //}

        //// POST: Courses/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(course).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name", course.DepartmentID);
        //    return View(course);
        //}

        
        
        
        
        // GET: Courses/Delete/5


        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Courses.Add(course);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException dex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            PopulateDepartmentsDropDownList(course.DepartmentID); ;
            
            return View(course);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseID, Title, Credits,DepartmentID")] Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(course).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                //Log the error (uncomment dex variable name and add a line here towrite a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and ifthe problem persists, see your system administrator.");
            }
            PopulateDepartmentsDropDownList(course.DepartmentID);
            return View(course);
        }

        
        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in db.Departments
                                   orderby d.Name
                                   select d;

            ViewBag.DepartmentID = new SelectList(departmentsQuery, "DepartmentID","Name", selectedDepartment);
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
