using StudInfoSys.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudInfoSys.ViewModels
{
    public class SubjectGradesRecordViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        
        //public Subject Subject { get; set; }

        //[Display(Name = "Subject")]
        public int SubjectId { get; set; }

        public List<GradeViewModel> GradeViewModels { get; set; }

        //public Grade CurrentGrade { get; set; }

        //private ICollection<Grade> _grades;
        //public ICollection<Grade> Grades
        //{
        //    get
        //    {
        //        return _grades;
        //    }
        //    set
        //    {
        //        _grades.Clear();
        //        var grades = value;
        //        foreach (var grade in grades)
        //        {
        //            _grades.Add(new Grade
        //            {
        //                GradeValue = grade.GradeValue,
        //                IsDeleted = grade.IsDeleted,
        //                Period = grade.Period
        //            });
        //        }
        //    }
        //}

        [Display(Name = "Name")]
        public string StudentFullName { get; set; }

        //for navigation only
        //public virtual Registration Registration { get; set; }
        public int RegistrationId { get; set; } //for easy access to Registration's Id
        public int LevelId { get; set; }

        [DisplayFormat(NullDisplayText = "")]
        [Display(Name = "Final Grade")]
        public decimal? FinalGrade
        {
            get
            {
                if (!GradeViewModels.Any(g => g.GradeValue == null))
                {
                    return GradeViewModels.Average(g => g.GradeValue);
                }
                return null;
            }
        }


        public IEnumerable<SelectListItem> SubjectsList { get; set; }
        public IEnumerable<Period> PeriodsList { get; set; }

    }
}