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
using System.Text;
using StudInfoSys.Helpers;
using PagedList;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace StudInfoSys.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public ViewResult Index(string searchString = "", string sortOrder = "", int? page = null)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.CurrentSearchString = searchString;

            var students = _studentRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.ToLower().Contains(searchString.ToLower()));
            }


            switch (sortOrder)
            {
                case "Name desc":
                    students = students.OrderByDescending(s=> s.LastName).ThenByDescending(s2=>s2.FirstName);
                    break;
                default:
                    students = students.OrderBy(s=> s.LastName).ThenBy(s2=>s2.FirstName);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(students.ToPagedList(pageNumber, pageSize));

        }


        public ViewResult SearchByLastName(string searchString)
        {
            return View("Index", _studentRepository.SearchFor(s => s.LastName.ToLower().Contains(searchString.ToLower()), false).OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ToPagedList(1, 3));
        }

        public ActionResult Details(int id = 0, string searchString = "", string sortOrder = "", int? page = null)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurrentPage = page;

            return View(student);
        }

        public ActionResult Create(string searchString = "", string sortOrder = "", int? page = null)
        {
            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurrentPage = page;

            return View(new StudentViewModel());
        }

        [HttpPost]
        public ActionResult Create(StudentViewModel studentViewModel, string searchString = "", string sortOrder = "", int? page = null)
        {
            if (ModelState.IsValid)
            {
                var student = MapStudentViewModelToStudent(studentViewModel);
                _studentRepository.Insert(student);
                _studentRepository.Save();
                return RedirectToAction("Index", new { searchString = student.LastName, page = page, sortOrder = sortOrder });
            }

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurrentPage = page;

            return View(studentViewModel);
        }

        public ActionResult Edit(int id = 0, string searchString = "", string sortOrder = "", int? page = null)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurrentPage = page;

            var studViewModel = MapStudentToStudentViewModel(student);
            return View(studViewModel);
        }

        [HttpPost]
        public ActionResult Edit(StudentViewModel studentViewModel, HttpPostedFileBase studentPhoto, string searchString = "", string sortOrder = "", int? page = null)
        {
            if (ModelState.IsValid)
            {
                var student = MapStudentViewModelToStudent(studentViewModel);

                if (studentPhoto != null && studentPhoto.ContentLength > 0)
                {
                    student.Photo = new byte[studentPhoto.ContentLength];
                    studentPhoto.InputStream.Read(student.Photo, 0, studentPhoto.ContentLength);
                    student.PhotoContentType = studentPhoto.ContentType;
                }

                _studentRepository.Update(student);
                _studentRepository.Save();
                
                return RedirectToAction("Index", new { page = page, sortOrder = sortOrder, searchString = searchString });
            }

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurrentPage = page;

            return View(studentViewModel);
        }

        public ActionResult Delete(int id = 0, string searchString = "", string sortOrder = "", int? page = null)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.CurrentSearchString = searchString;
            ViewBag.CurrentPage = page;

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, string searchString = "", string sortOrder = "", int? page = null)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            // If student has at least one registration record, deletion is prohibited
            if (student.Registrations.Any(r => r.IsDeleted == false))
            {
                throw new HttpException("You are not allowed to delete this student because he has registration records");
            }
            _studentRepository.Delete(student);
            _studentRepository.Save();
            return RedirectToAction("Index", new { page = page, sortOrder = sortOrder, searchString = searchString });
        }

        public ActionResult GetImage(int id)
        {
            Student student = _studentRepository.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            var photo = student.Photo;
            if (photo == null)
            {
                Image img = Properties.Resources.NoPhoto;

                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Png);
                    photo = ms.ToArray();
                }
            }

            return File(photo, student.PhotoContentType?? "image/*");
        }

        #region Private methods

        private static StudentViewModel MapStudentToStudentViewModel(Student student)
        {
            var studViewModel = new StudentViewModel
            {
                Address = student.Address,
                DateOfBirth = student.DateOfBirth,
                FirstName = student.FirstName,
                Gender = student.Gender,
                Id = student.Id,
                LastName = student.LastName,
                Photo = student.Photo,
                StudentStatus = student.StudentStatus,
                Email = student.Email
            };
            return studViewModel;
        }

        private static Student MapStudentViewModelToStudent(StudentViewModel studentViewModel)
        {
            var studViewModel = new Student
            {
                Address = studentViewModel.Address,
                DateOfBirth = studentViewModel.DateOfBirth,
                FirstName = studentViewModel.FirstName,
                Gender = studentViewModel.Gender,
                Id = studentViewModel.Id,
                LastName = studentViewModel.LastName,
                Photo = studentViewModel.Photo,
                StudentStatus = studentViewModel.StudentStatus,
                Email = studentViewModel.Email
            };
            return studViewModel;
        }
        
        #endregion


        protected override void OnException(ExceptionContext filterContext)
        {
            //Log error
            Log.WriteLog(Properties.Settings.Default.LogErrorFile, filterContext.Exception.ToString());
        }

    }
}