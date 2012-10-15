using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudInfoSys.Models
{
    public class Student : Person, IDeletableEntity
    {

        public virtual StudentStatus StudentStatus { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }

        [ScaffoldColumn(false)]
        public virtual bool IsDeleted { get; set; }
    }
}