using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudInfoSys.Models;
using StudInfoSys.Repository;

namespace StudInfoSys.Controllers
{
    public class RegistrationController : Controller
    {
        private StudInfoSysContext db = new StudInfoSysContext();

        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationController(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }


        public ActionResult RegistrationsByStudentId(int studentId)
        {
            var registrations = _registrationRepository.SearchFor(r => r.Student.Id == studentId).Include(r => r.Semester).Include(r => r.Degree);
            
            return View("Index", registrations);
        }

        //
        // GET: /Registration/Details/5

        public ActionResult Details(int id = 0)
        {
            Registration registration = _registrationRepository.GetById(id);
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
                _registrationRepository.Insert(registration);
                _registrationRepository.Save();
                return RedirectToAction("RegistrationsByStudentId", new {studentId = registration.Student.Id});
            }

            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", registration.SemesterId);
            ViewBag.DegreeId = new SelectList(db.Degrees, "Id", "Title", registration.DegreeId);
            return View(registration);
        }

        //
        // GET: /Registration/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Registration registration = _registrationRepository.GetById(id);
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
                _registrationRepository.Update(registration);
                _registrationRepository.Save();
                return RedirectToAction("RegistrationsByStudentId", new { studentId = registration.Student.Id });
            }
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Name", registration.SemesterId);
            ViewBag.DegreeId = new SelectList(db.Degrees, "Id", "Title", registration.DegreeId);
            return View(registration);
        }

        //
        // GET: /Registration/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Registration registration = _registrationRepository.GetById(id);
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
            Registration registration = _registrationRepository.GetById(id);
            _registrationRepository.Delete(registration);
            _registrationRepository.Save();

            //TODO: create a RegistrationViewModel that includes StudentId as one of its properties
            return RedirectToAction("RegistrationsByStudentId", new { studentId = registration.Student.Id });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}