using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudInfoSys.Models;

namespace StudInfoSys.Repository
{
    public class RegistrationRepository : DeletableRepositoryBase<Registration>, IRegistrationRepository
    {
        public RegistrationRepository(DbContext dataContext) : base(dataContext)
        {
        }
    }
}