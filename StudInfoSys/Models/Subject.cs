using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace StudInfoSys.Models
{
    public class Subject
    {
        [HiddenInput]
        public virtual int Id { get; set; }


        public string SubjectCode { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public virtual string Name { get; set; }

        [Range(0, 10)]
        public virtual int NumberOfUnits { get; set; }

        
        public virtual Level Level { get; set; }
        [Required]
        public virtual int LevelId { get; set; }
    }
}