using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace StudInfoSys.Models
{
    /// <summary>
    /// Represents the periods in a semester (ex. Prelim, Midterm, Finals)
    /// </summary>
    public class Period
    {
        [HiddenInput]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public virtual string Name { get; set; }

        [Required]
        public virtual Level Level { get; set; }
        public virtual int LevelId { get; set; }

    }
}