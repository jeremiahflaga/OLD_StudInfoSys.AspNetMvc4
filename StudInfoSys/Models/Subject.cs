using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace StudInfoSys.Models
{
    public class Subject
    {
        [HiddenInput]
        public virtual int Id { get; set; }

        [Display(Name="Subject Code")]
        public string SubjectCode { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public virtual string Name { get; set; }

        [Range(0, 10)]
        [Display(Name = "Units")]
        public virtual int NumberOfUnits { get; set; }

        
        public virtual Level Level { get; set; }
        [Required]
        public virtual int LevelId { get; set; }
    }
}