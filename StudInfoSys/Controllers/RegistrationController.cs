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
using StudInfoSys.Helpers;

namespace StudInfoSys.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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

        public ActionResult Details(int id = 0)
        {
            Registration registration = _unitOfWork.RegistrationRepository.GetById(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        /// <summary>
        /// Creates the registration for the student with the specified student ID.
        /// </summary>
        /// <param name="id">The student ID.</param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            var registrationViewModel = new RegistrationViewModel
                                            {
                                                StudentId = id,
                                                SemestersList =
                                                    new SelectList(_unitOfWork.SemesterRepository.GetAll(), "Id", "Name"),
                                                DegreesList =
                                                    new SelectList(_unitOfWork.DegreeRepository.GetAll(), "Id", "Title"),
                                                DateOfRegistration = DateTime.Now
                                            };
            
            return View(registrationViewModel);
        }

        [HttpPost]
        public ActionResult Create(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var registration = MapRegistrationViewModelToRegistration(registrationViewModel);
                _unitOfWork.RegistrationRepository.Insert(registration);
                _unitOfWork.RegistrationRepository.Save();
                
                return RedirectToAction("Index", new { id = registrationViewModel.StudentId });
            }

            registrationViewModel.SemestersList = new SelectList(_unitOfWork.SemesterRepository.GetAll(), "Id", "Name", registrationViewModel.SemesterId);
            registrationViewModel.DegreesList = new SelectList(_unitOfWork.DegreeRepository.GetAll(), "Id", "Title", registrationViewModel.DegreeId);
            return View(registrationViewModel);
        }

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

        [HttpPost]
        public ActionResult Edit(RegistrationViewModel registrationViewModel)
        {
            if (ModelState.IsValid)
            {
                var registration = MapRegistrationViewModelToRegistration(registrationViewModel);
                _unitOfWork.RegistrationRepository.Update(registration);
                _unitOfWork.RegistrationRepository.Save();
                return RedirectToAction("Index", new { id = registrationViewModel.StudentId });
            }

            registrationViewModel.SemestersList = new SelectList(_unitOfWork.SemesterRepository.GetAll(), "Id", "Name", registrationViewModel.SemesterId);
            registrationViewModel.DegreesList = new SelectList(_unitOfWork.SemesterRepository.GetAll(), "Id", "Title", registrationViewModel.DegreeId);
            return View(registrationViewModel);
        }

        public ActionResult Delete(int id = 0)
        {
            Registration registration = _unitOfWork.RegistrationRepository.GetById(id);
            if (registration == null)
            {
                return HttpNotFound();
            }
            return View(registration);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Registration registration = _unitOfWork.RegistrationRepository.GetById(id);
            if (registration == null)
            {
                return HttpNotFound();
            }

            // If registration record has at least one related grade record, deletion is prohibited
            if (registration.SubjectGradesRecords.Any(sgr => sgr.IsDeleted == false))
            {
                throw new HttpException("You are not allowed to delete this registration record because it has related grade records");
            }

            var studentId = registration.Student.Id;
            _unitOfWork.RegistrationRepository.Delete(registration);
            _unitOfWork.RegistrationRepository.Save();

            return RedirectToAction("Index", new { id = studentId });
        }



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
        
        protected override void OnException(ExceptionContext filterContext)
        {
            //Log error
            Log.WriteLog(Properties.Settings.Default.LogErrorFile, filterContext.Exception.ToString());
        }

    }
}