using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StudInfoSys.Models;
using StudInfoSys.Repository;
using StudInfoSys.ViewModels;

namespace StudInfoSys.Controllers
{
    public class SubjectGradesRecordController : Controller
    {
        //private StudInfoSysContext db = new StudInfoSysContext();
        IUnitOfWork _unitOfWork;

        public SubjectGradesRecordController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Grade/

        /// <summary>
        /// Indexes the specified registration id.
        /// </summary>
        /// <param name="id">The registration id.</param>
        /// <returns></returns>
        public ViewResult Index(int id)
        {
            return SubjectGradesRecordByRegistrationId(id);
        }

        /// <summary>
        /// Grades record by registration id.
        /// </summary>
        /// <param name="registrationId">The registration id.</param>
        /// <returns></returns>
        /// [ChildActionOnly]
        public ViewResult SubjectGradesRecordByRegistrationId(int registrationId)
        {
            //var subjectGradesRecords = _unitOfWork.SubjectGradesRecordRepository.SearchFor(sgr => sgr.Registration.Id == id, false).Include(sgr => sgr.Subject);
            //var subjectGradesRecordViewModel = MapListOfSubjectGradesRecordToListOfSubjectGradesRecordViewModel(subjectGradesRecords);
            ViewBag.RegistrationId = registrationId;
            return View("Index", _unitOfWork.SubjectGradesRecordRepository.SearchFor(sgr => sgr.Registration.Id == registrationId, false)
                .Include(sgr => sgr.Subject)
                .Include(sgr => sgr.Registration)
                .Include(sgr => sgr.Grades)
                );
        }

        //
        // GET: /Grade/Details/5

        public ActionResult Details(int id = 0)
        {
            SubjectGradesRecord subjectgradesrecord = _unitOfWork.SubjectGradesRecordRepository.GetById(id);
            if (subjectgradesrecord == null)
            {
                return HttpNotFound();
            }
            return View(subjectgradesrecord);
        }

        //
        // GET: /Grade/Create

        /// <summary>
        /// Creates a SubjectGradesRecord for the specified registration id.
        /// </summary>
        /// <param name="id">The registration id.</param>
        /// <returns></returns>
        public ActionResult Create(int id)
        {
            var levelIdOfCurrentRegistration = _unitOfWork.RegistrationRepository.GetById(id).Degree.LevelId;
            var subjects = _unitOfWork.SubjectRepository.GetAll();
            var registrations = _unitOfWork.RegistrationRepository.GetAll();
            var periods = _unitOfWork.PeriodRepository.GetAll();
            var subjectGradesRecordViewModel = new SubjectGradesRecordViewModel
            {
                RegistrationId = id,
                SubjectsList = new SelectList(_unitOfWork.SubjectRepository.GetAll(), "Id", "Name"),
                PeriodsList = periods.Where(p => p.LevelId == levelIdOfCurrentRegistration),
                Grades = new List<Grade>()
            };
            
            return View(subjectGradesRecordViewModel);
        }

        //
        // POST: /Grade/Create

        [HttpPost]
        public ActionResult Create(SubjectGradesRecord subjectgradesrecord)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.SubjectGradesRecordRepository.Insert(subjectgradesrecord);
                _unitOfWork.SubjectGradesRecordRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.SubjectId = new SelectList(_unitOfWork.SubjectRepository.GetAll().Distinct(), "Id", "SubjectCode", subjectgradesrecord.SubjectId);
            return View(subjectgradesrecord);
        }

        //
        // GET: /Grade/Edit/5

        public ActionResult Edit(int id = 0)
        {
            SubjectGradesRecord subjectgradesrecord = _unitOfWork.SubjectGradesRecordRepository.GetById(id);
            if (subjectgradesrecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubjectId = new SelectList(_unitOfWork.SubjectRepository.GetAll().Distinct(), "Id", "SubjectCode", subjectgradesrecord.SubjectId);
            return View(subjectgradesrecord);
        }

        //
        // POST: /Grade/Edit/5

        [HttpPost]
        public ActionResult Edit(SubjectGradesRecord subjectgradesrecord)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.SubjectGradesRecordRepository.Update(subjectgradesrecord);
                _unitOfWork.SubjectGradesRecordRepository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.SubjectId = new SelectList(_unitOfWork.SubjectRepository.GetAll().Distinct(), "Id", "SubjectCode", subjectgradesrecord.SubjectId);
            return View(subjectgradesrecord);
        }

        //
        // GET: /Grade/Delete/5

        public ActionResult Delete(int id = 0)
        {
            SubjectGradesRecord subjectgradesrecord = _unitOfWork.SubjectGradesRecordRepository.GetById(id);
            if (subjectgradesrecord == null)
            {
                return HttpNotFound();
            }
            return View(subjectgradesrecord);
        }

        //
        // POST: /Grade/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SubjectGradesRecord subjectgradesrecord = _unitOfWork.SubjectGradesRecordRepository.GetById(id);
            _unitOfWork.SubjectGradesRecordRepository.Delete(subjectgradesrecord);
            _unitOfWork.SubjectGradesRecordRepository.Save();
            return RedirectToAction("Index");
        }


        private SubjectGradesRecord MapSubjectGradesRecordViewModelToSubjectGradesRecord(SubjectGradesRecordViewModel subjectGradesRecordViewModel)
        {
            return new SubjectGradesRecord
            {
                Id = subjectGradesRecordViewModel.Id, 
                Registration = _unitOfWork.RegistrationRepository.GetById(subjectGradesRecordViewModel.RegistrationId),
                SubjectId = subjectGradesRecordViewModel.Id,
                //Grades = subjectGradesRecordViewModel.Grades
            };
        }

        private SubjectGradesRecordViewModel MapSubjectGradesRecordToSubjectGradesRecordViewModel(SubjectGradesRecord subjectGradesRecord)
        {
            return new SubjectGradesRecordViewModel()
            {
                //Id = subjectGradesRecord.Id,
                RegistrationId = subjectGradesRecord.Registration.Id,
                SubjectId = subjectGradesRecord.Subject.Id,
                LevelId = subjectGradesRecord.Registration.Degree.LevelId
                //SubjectCode = subjectGradesRecord.Subject.SubjectCode,
                //SubjectName = subjectGradesRecord.Subject.Name
                //Grades = subjectGradesRecord.Grades
            };
        }

        private IEnumerable<SubjectGradesRecordViewModel> MapListOfSubjectGradesRecordToListOfSubjectGradesRecordViewModel(IEnumerable<SubjectGradesRecord> listOfSubjectGradesRecord)
        {
            List<SubjectGradesRecordViewModel> listOfSubjectGradesRecordViewModel = new List<SubjectGradesRecordViewModel>();

            foreach (var subjectGradesRecord in listOfSubjectGradesRecord)
            {
                SubjectGradesRecordViewModel vm = MapSubjectGradesRecordToSubjectGradesRecordViewModel(subjectGradesRecord);
                listOfSubjectGradesRecordViewModel.Add(vm);
            }

            return listOfSubjectGradesRecordViewModel;
        }

    }
}