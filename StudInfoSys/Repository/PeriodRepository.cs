using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudInfoSys.Models;

namespace StudInfoSys.Repository
{
    public class PeriodRepository : RepositoryBase<Period>, IPeriodRepository
    {
        public PeriodRepository(DbContext dataContext)
            : base(dataContext)
        {
        }
    }
}