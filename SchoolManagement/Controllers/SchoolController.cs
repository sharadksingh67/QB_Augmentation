using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    public class SchoolController : Controller
    {
        public SchoolManagementDbContext db = new SchoolManagementDbContext();

        // GET: School
        public ActionResult Index()
        {
            ViewBag.Message = "This is Index Action";
            return View(db.schools.ToList());
        }

        // GET: School/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            school school = db.schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // GET: School/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: School/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SchoolId,Name,SchoolType,OnlineClasses,LaunchDate")] school school)
        {
            if (ModelState.IsValid)
            {
                db.schools.Add(school);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(school);
        }

        // GET: School/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            school school = db.schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: School/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SchoolId,Name,SchoolType,OnlineClasses,LaunchDate")] school school)
        {
            if (ModelState.IsValid)
            {
                db.Entry(school).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(school);
        }

        // GET: School/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            school school = db.schools.Find(id);
            if (school == null)
            {
                return HttpNotFound();
            }
            return View(school);
        }

        // POST: School/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            school school = db.schools.Find(id);
            db.schools.Remove(school);
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
