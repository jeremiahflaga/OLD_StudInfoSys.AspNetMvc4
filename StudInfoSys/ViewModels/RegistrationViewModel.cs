using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using StudInfoSys.Models;
using System.Web.Mvc;

namespace StudInfoSys.ViewModels
{
    public class RegistrationViewModel : IValidatableObject
    {
        public virtual int Id { get; set; }

        [Required]
        [Display(Name = "From School Year")]
        [Range(1800, 9999)]
        public int SchoolYearFrom { get; set; }

        [Required]
        [Display(Name = "To School Year")]
        [Range(1800, 9999)]
        public int SchoolYearTo { get; set; }

        [Required]
        [Display(Name="Semester")]
        //public Semester Semester { get; set; }
        public int SemesterId { get; set; }

        [Required]
        [Display(Name="Degree")]
        //public Degree Degree { get; set; }
        public int DegreeId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date of Registration")]
        [DisplayFormat(NullDisplayText = "", DataFormatString = "{0:MMMM dd,yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfRegistration { get; set; }

        //[Required]
        [Display(Name="Student")]
        public int StudentId { get; set; }

        [Display(Name = "Name")]
        public string StudentFullName { get; set; }

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


        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var schoolYearFields = new string[] { "SchoolYearFrom", "SchoolYearTo" };
            if (String.IsNullOrEmpty(SchoolYearFrom.ToString()) || String.IsNullOrEmpty(SchoolYearTo.ToString()))
            {
                yield return new ValidationResult("School Year is required");
            }
        }
    }
}