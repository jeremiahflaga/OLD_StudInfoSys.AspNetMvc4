using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudInfoSys.Models;
using StudInfoSys.Repository;
using StudInfoSys.ViewModels;

namespace StudInfoSys.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //private readonly IRegistrationRepository _registrationRepository;

        public RegistrationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Student/

        /// <summary>
        /// Indexes the registration records of the student with the specified student ID.
        /// </summary>
        /// <param name="id">The student ID.</param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            return RegistrationsByStudentId(id);
        }

        [ChildActionOnly]
        public ActionResult RegistrationsByStudentId(int studentId)
        {
            ViewBag.StudentId = studentId;
            var registrations = _unitOfWork.RegistrationRepository.SearchFor(r => r.Student.Id == studentId, false)
                .Include(r => r.Semester)
                .Include(r => r.Degree)
                .OrderBy(r => r.SchoolYearFrom).ThenBy(r => r.SchoolYearTo);
            return View("Index", registrations);
        }

        //
        // GET: /Registration/Details/5

        public ActionResult Details(int id = 0)
        {
            Registration registration = _unitOfWork.RegistrationRepository.GetById(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        //
        // GET: /Registration/Create

        /// <summary>
        /// Creates the registration for the student with the specified student ID.
        /// </summary>
        /// <param name="id">The student ID.</param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            //var periods = _unitOfWork.PeriodRepository.GetAll();
            var registrationViewModel = new RegistrationViewModel
                                            {
                                                StudentId = id,
                                                SemestersList =
                                                    new SelectList(_unitOfWork.SemesterRepository.GetAll(), "Id", "Name"),
                                                DegreesList =
                                                    new SelectList(_unitOfWork.DegreeRepository.GetAll(), "Id", "Title")
                                            };
            
            return View(registrationViewModel);
        }

        //
        // POST: /Registration/Create

        [HttpPost]
        public ActionResult Create(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var registration = MapRegistrationViewModelToRegistration(registrationViewModel);
                _unitOfWork.RegistrationRepository.Insert(registration);
                _unitOfWork.RegistrationRepository.Save();
                //try
                //{
                //    _unitOfWork.RegistrationRepository.Save();
                //}
                //catch (DbEntityValidationException ex)
                //{
                //    Debug.WriteLine("========================================\nDEBUG: " + ex.ToString());
                //}
                
                return RedirectToAction("Index", new { id = registrationViewModel.StudentId });
            }

            registrationViewModel.SemestersList = new SelectList(_unitOfWork.SemesterRepository.GetAll(), "Id", "Name", registrationViewModel.SemesterId);
            registrationViewModel.DegreesList = new SelectList(_unitOfWork.DegreeRepository.GetAll(), "Id", "Title", registrationViewModel.DegreeId);
            return View(registrationViewModel);
        }

        //
        // GET: /Registration/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Registration registration = _unitOfWork.RegistrationRepository.GetById(id);
            if (registration == null)
            {
                return HttpNotFound();
            }

            var registrationViewModel = MapRegistrationToRegistrationViewModel(registration);
            registrationViewModel.SemestersList = new SelectList(_unitOfWork.SemesterRepository.GetAll(), "Id", "Name", registrationViewModel.SemesterId);
            registrationViewModel.DegreesList = new SelectList(_unitOfWork.DegreeRepository.GetAll(), "Id", "Title", registrationViewModel.DegreeId);
            return View(registrationViewModel);
        }

        //
        // POST: /Registration/Edit/5

        [HttpPost]
        public ActionResult Edit(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var registration = MapRegistrationViewModelToRegistration(registrationViewModel);
                //_unitOfWork.RegistrationRepository.GetAll();
                _unitOfWork.RegistrationRepository.Update(registration);
                _unitOfWork.RegistrationRepository.Save();
                return RedirectToAction("Index", new { id = registrationViewModel.StudentId });
            }

            registrationViewModel.SemestersList = new SelectList(_unitOfWork.SemesterRepository.GetAll(), "Id", "Name", registrationViewModel.SemesterId);
            registrationViewModel.DegreesList = new SelectList(_unitOfWork.SemesterRepository.GetAll(), "Id", "Title", registrationViewModel.DegreeId);
            return View(registrationViewModel);
        }

        //
        // GET: /Registration/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Registration registration = _unitOfWork.RegistrationRepository.GetById(id);
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
            Registration registration = _unitOfWork.RegistrationRepository.GetById(id);
            var studentId = registration.Student.Id;
            _unitOfWork.RegistrationRepository.Delete(registration);
            _unitOfWork.RegistrationRepository.Save();

            //TODO: create a RegistrationViewModel that includes StudentId as one of its properties
            return RedirectToAction("Index", new { id = studentId });
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}


        private Registration MapRegistrationViewModelToRegistration(RegistrationViewModel registrationsViewModel)
        {
            return new Registration
                       {
                           Id = registrationsViewModel.Id,
                           DateOfRegistration = registrationsViewModel.DateOfRegistration,
                           //Degree = _unitOfWork.DegreeRepository.GetById(registrationsViewModel.DegreeId),
                           DegreeId = registrationsViewModel.DegreeId,
                           SchoolYearFrom = registrationsViewModel.SchoolYearFrom,
                           SchoolYearTo = registrationsViewModel.SchoolYearTo,
                           //Semester = _unitOfWork.SemesterRepository.GetById(registrationsViewModel.SemesterId),
                           SemesterId = registrationsViewModel.SemesterId,
                           Student = _unitOfWork.StudentRepository.GetById(registrationsViewModel.StudentId),
                       };
        }

        private RegistrationViewModel MapRegistrationToRegistrationViewModel(Registration registrations)
        {
            return new RegistrationViewModel
                       {
                           Id = registrations.Id,
                           DateOfRegistration = registrations.DateOfRegistration,
                           DegreeId = registrations.Degree.Id,
                           SchoolYearFrom = registrations.SchoolYearFrom,
                           SchoolYearTo = registrations.SchoolYearTo,
                           SemesterId = registrations.Semester.Id,
                           StudentId = registrations.Student.Id
                       };
        }
    }
}

//Id = registrationsViewModel.Id,
//                           DateOfRegistration = registrationsViewModel.DateOfRegistration,
//                           Degree = _unitOfWork.DegreeRepository.GetById(registrationsViewModel.DegreeId),
//                           SchoolYearFrom = registrationsViewModel.SchoolYearFrom,
//                           SchoolYearTo = registrationsViewModel.SchoolYearTo,
//                           Semester = _unitOfWork.SemesterRepository.GetById(registrationsViewModel.SemesterId)
