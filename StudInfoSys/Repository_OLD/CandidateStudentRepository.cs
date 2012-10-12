using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudInfoSys.Models;

namespace StudInfoSys.Repository
{
    public class CandidateStudentRepository : CandidateDeletableRepository<Student>
    {
        public CandidateStudentRepository(DbContext dataContext)
            : base(dataContext)
        {
        }
    }
}