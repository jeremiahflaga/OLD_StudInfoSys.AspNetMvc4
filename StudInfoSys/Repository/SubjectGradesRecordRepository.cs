using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudInfoSys.Models;

namespace StudInfoSys.Repository
{
    public class SubjectGradesRecordRepository : RepositoryBase<SubjectGradesRecord>, ISubjectGradesRecordRepository
    {
        public SubjectGradesRecordRepository(DbContext dataContext)
            : base(dataContext)
        {
        }
    }
}