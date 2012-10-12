using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudInfoSys.Models;
using System.Web.Mvc;

namespace StudInfoSys.ViewModels
{
    public class RegistrationViewModel
    {
        public virtual int Id { get; set; }

        [Required]
        [Display(Name = "From School Year")]
        public int SchoolYearFrom { get; set; }

        [Required]
        [Display(Name = "To School Year")]
        public int SchoolYearTo { get; set; }

        [Required]
        public Semester Semester { get; set; }
        //public int SemesterId { get; set; }

        [Required]
        public Degree Degree { get; set; }
        //public int DegreeId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date of Registration")]
        public DateTime DateOfRegistration { get; set; }

        public int StudentId { get; set; }

        [Display(Name = "School Year", Order = 500)]
        public string SchoolYear
        {
            get
            {
                return SchoolYearFrom + "-" + SchoolYearTo;
            }
        }

        public IEnumerable<SelectListItem> SemestersList { get; set; }
        public IEnumerable<SelectListItem> DegreesList { get; set; }
    }
}