using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;

namespace StudInfoSys.Models
{
    /// <summary>
    /// Holds the subject and the grades for all periods
    /// (Ex.
    ///     for Subject->Mathematics
    ///             Period->Prelim      Grade->89
    ///             Period->Midterm     Grade->890
    ///             Period->Finals      Grade->891
    /// )
    /// </summary>
    public class SubjectGradesRecord : IDeletableEntity
    {
        [HiddenInput]
        public virtual int Id { get; set; }

        [Required]
        public virtual Subject Subject { get; set; }
        
        public virtual int SubjectId { get; set; }

        public virtual ICollection<Grade> Grades { get; set; }

        //for navigation only
        public virtual Registration Registration { get; set; }
        //public virtual int RegistrationId { get; set; } //for easy access to Registration's Id

        [ScaffoldColumn(false)]
        public virtual bool IsDeleted { get; set; }


        [DisplayFormat(NullDisplayText = "")]
        public decimal? FinalGrade
        {
            get
            {
                if (!Grades.Any(g => g.GradeValue == null))
                {
                    return Grades.Average(g => g.GradeValue);
                }
                return null;
            }
        }


    }
}