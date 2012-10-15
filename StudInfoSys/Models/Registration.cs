using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StudInfoSys.Models
{
    /// <summary>
    /// Record of student's enrolment/registration
    /// </summary>
    public class Registration : IDeletableEntity
    {
        public virtual int Id { get; set; }
        
        [Required]
        [Display(Name = "From School Year")]
        public virtual int SchoolYearFrom { get; set; }

        [Required]
        [Display(Name = "To School Year")]
        public virtual int SchoolYearTo { get; set; }

        
        public virtual Semester Semester { get; set; }
        [Required]
        public virtual int SemesterId { get; set; }

        
        public virtual Degree Degree { get; set; }
        [Required]
        public virtual int DegreeId { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Date of Registration")]
        public virtual DateTime DateOfRegistration { get; set; }

        public virtual ICollection<SubjectGradesRecord> SubjectGradesRecords { get; set; }

        //for navigation
        public virtual Student Student { get; set; }
        //public virtual int StudentId { get; set; } //for easy access to Student's Id

        [ScaffoldColumn((false))]
        public virtual bool IsDeleted { get; set; }

        [Display(Name="School Year", Order=500)]
        public string SchoolYear { 
            get
            {
                return SchoolYearFrom + "-" + SchoolYearTo;
            }
        }

    }
}