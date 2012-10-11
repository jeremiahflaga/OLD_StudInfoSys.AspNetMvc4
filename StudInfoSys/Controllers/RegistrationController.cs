using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudInfoSys.Models;

namespace StudInfoSys.Controllers
{
    public class RegistrationController : Controller
    {
        private StudInfoSysContext db = new StudInfoSysContext();

        //
        // GET: /Registration/

        //public ActionResult Index()
        //{
        //    var registrations = db.Registrations.Include(r => r.Semester).Include(r => r.Degree);
        //    return View(registrations.ToList());
        //}

        public ActionResult RegistrationsByStudentId(int studentId)
        {
            var registrations = db.Registrations.Where(r => r.Student.Id == studentId).Include(r => r.Semester).Include(r => r.Degree);
            
            //if (!registrations.Any())
            //{
            //    return null; // TODO: return a page that informs user that no registration record for current student exists
            //}
            return View("Index", registrations);
        }

        //
        // GET: /Registration/Details/5

        public ActionResult Details(int id = 0)
        {
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        //
        // GET: /Registration/Create

        public ActionResult Create()
        {
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name");
            ViewBag.DegreeId = new SelectList(db.Degrees, "Id", "Title");
            return View();
        }

        //
        // POST: /Registration/Create

        [HttpPost]
        public ActionResult Create(Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Registrations.Add(registration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", registration.SemesterId);
            ViewBag.DegreeId = new SelectList(db.Degrees, "Id", "Title", registration.DegreeId);
            return View(registration);
        }

        //
        // GET: /Registration/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", registration.SemesterId);
            ViewBag.DegreeId = new SelectList(db.Degrees, "Id", "Title", registration.DegreeId);
            return View(registration);
        }

        //
        // POST: /Registration/Edit/5

        [HttpPost]
        public ActionResult Edit(Registration registration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", registration.SemesterId);
            ViewBag.DegreeId = new SelectList(db.Degrees, "Id", "Title", registration.DegreeId);
            return View(registration);
        }

        //
        // GET: /Registration/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Registration registration = db.Registrations.Find(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        //
        // POST: /Registration/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = db.Registrations.Find(id);
            db.Registrations.Remove(registration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}