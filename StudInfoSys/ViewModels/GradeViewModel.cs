using StudInfoSys.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudInfoSys.ViewModels
{
    public class GradeViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public string PeriodName { get; set; }
        [Required]
        public int PeriodId { get; set; }

        [DisplayFormat(DataFormatString = "{0:f}", NullDisplayText = "No Grade")]
        [Range(0, 100)]
        [Display(Name = "Grade")]
        public virtual decimal? GradeValue { get; set; }

        public int SubjectGradesRecordId { get; set; }
    }
}