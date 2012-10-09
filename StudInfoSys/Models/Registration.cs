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
        [HiddenInput]
        public virtual int Id { get; set; }
        
        [Required]
        public virtual int SchoolYearFrom { get; set; }

        [Required]
        public virtual int SchoolYearTo { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public virtual DateTime DateOfRegistration { get; set; }

        [Required]
        public virtual Semester Semester { get; set; }
        public virtual int SemesterId { get; set; }

        [Required]
        public virtual Degree Degree { get; set; }
        public virtual int DegreeId { get; set; }

        public virtual ICollection<SubjectGradesRecord> SubjectGradesRecords { get; set; }

        //for navigation
        public virtual Student Student { get; set; }
        //public virtual int StudentId { get; set; } //for easy access to Student's Id

        public virtual bool IsDeleted { get; set; }

    }
}