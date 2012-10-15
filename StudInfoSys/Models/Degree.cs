using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace StudInfoSys.Models
{
    public class Degree
    {
        [HiddenInput]
        public virtual int Id { get; set; }


        /// <summary>
        /// Gets or sets the title. 
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        /// <example>Kinder 1, Grade 4, First Year Hogh School, BSCS</example>
        [Required]
        [StringLength(100, MinimumLength=1)]
        public virtual string Title { get; set; }

        [Required]
        [StringLength(20)]
        public virtual string Acronym { get; set; }

        
        public virtual Level Level { get; set; }
        [Required]
        public virtual int LevelId { get; set; }
        
        public string LevelAndTitle { 
            get
            {
                return Level + " - " + Title;
            }
        }


    }
}