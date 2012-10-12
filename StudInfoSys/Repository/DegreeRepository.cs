using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudInfoSys.Models;

namespace StudInfoSys.Repository
{
    public class DegreeRepository : RepositoryBase<Degree>, IDegreeRepository
    {
        public DegreeRepository(DbContext dataContext)
            : base(dataContext)
        {
        }
    }
}