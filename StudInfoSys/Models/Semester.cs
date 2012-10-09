using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StudInfoSys.Models
{
    public class Semester
    {
        [HiddenInput]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public virtual string Name { get; set; }
    }
}