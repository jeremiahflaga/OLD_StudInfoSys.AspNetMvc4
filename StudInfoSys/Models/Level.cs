using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace StudInfoSys.Models
{
    /// <summary>
    /// Ex. Preparatory, Elementary, High School, College/Undergraduate, Graduate
    /// </summary>
    /// <example>Preparatory, Elementary, High School, College/Undergraduate, Graduate</example>
    public class Level
    {
        [HiddenInput]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength=1)]
        public virtual string Name { get; set; }
    }
}
