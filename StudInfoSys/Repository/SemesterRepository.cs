using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudInfoSys.Models;

namespace StudInfoSys.Repository
{
    public class SemesterRepository : RepositoryBase<Semester>, ISemesterRepository
    {
        public SemesterRepository(DbContext dataContext)
            : base(dataContext)
        {
        }
    }
}