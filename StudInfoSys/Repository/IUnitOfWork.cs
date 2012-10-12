using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudInfoSys.Models;

namespace StudInfoSys.Repository
{
    public interface IUnitOfWork
    {
        IRepository<Student> StudentRepository { get; }
        IRepository<Registration> RegistrationRepository { get; }
        IRepository<SubjectGradesRecord> SubjectGradesRecordRepository { get; }

        IRepository<Grade> GradeRepository { get; }
        IRepository<Degree> DegreeRepository { get; }
        IRepository<Level> LevelRepository { get; }

        IRepository<Period> PeriodRepository { get; }
        IRepository<Semester> SemesterRepository { get; }
        IRepository<Subject> SubjectRepository { get; }


        void Save();
    }
}