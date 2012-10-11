using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StudInfoSys.Models;

namespace StudInfoSys.Repository
{
    public class StudentRepository : DeletableRepositoryBase<Student>, IStudentRepository
    {
        public StudentRepository(DbContext dataContext)
            : base(dataContext)
        {
        }
    }
}