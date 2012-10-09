using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudInfoSys.Models
{
    public class DeletableEntity : IDeletableEntity
    {
        public virtual bool IsDeleted { get; set; }
    }
}