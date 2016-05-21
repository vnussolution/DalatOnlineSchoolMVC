﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DalatOnlineSchool.DAL;
using DalatOnlineSchool.ViewModels;

namespace DalatOnlineSchool.Controllers
{
    public class HomeController : Controller
    {
        private SchoolContext db = new SchoolContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            IQueryable<EnrollmentDateGroup> data = from student in db.Students
                                                    group student by student.EnrollmentDate into dataGroup
                                                    select new EnrollmentDateGroup()
                                                    {
                                                        EnrollmentDate = dataGroup.Key,
                                                        StudentCount = dataGroup.Count()
                                                    };

            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}