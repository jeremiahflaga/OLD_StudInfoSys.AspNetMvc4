using System.Collections.Generic;

namespace StudInfoSys.Models
{
    public class Student : Person, IDeletableEntity
    {

        public virtual StudentStatus StudentStatus { get; set; }

        public virtual ICollection<Registration> Registrations { get; set; }

        public virtual bool IsDeleted { get; set; }
    }
}