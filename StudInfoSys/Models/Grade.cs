using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace StudInfoSys.Models
{
    public class Grade : IDeletableEntity
    {
        [HiddenInput]
        public virtual int Id { get; set; }

        [Required]
        public virtual Period Period { get; set; }
        public virtual int PeriodId { get; set; }

        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "No Grade")]
        [Range(0, 100)]
        public virtual decimal? GradeValue { get; set; }

        //for navigation only
        public virtual SubjectGradesRecord SubjectGradesRecord { get; set; }       
        //public virtual int SubjectGradesRecordId { get; set; } //for easy access to SubjectGradesRecord's Id

        public virtual bool IsDeleted { get; set; }
    }
}