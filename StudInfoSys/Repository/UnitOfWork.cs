using System.Data.Entity;
using StudInfoSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudInfoSys.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }

        private IRepository<Student> _studentRepository;

        private IRepository<Registration> _registrationRepository;

        private IRepository<SubjectGradesRecord> _subjectGradesRecordRepository;

        private IRepository<Grade> _gradeRepository;

        private IRepository<Degree> _degreeRepository;

        private IRepository<Level> _levelRepository;

        private IRepository<Period> _periodRepository;

        private IRepository<Semester> _semesterRepository;

        private IRepository<Subject> _subjectRepository;

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public IRepository<Student> StudentRepository
        {
            //get
            //{
            //    if (_studentRepository == null)
            //    {
            //        _studentRepository = new StudentRepository(_context);
            //    }
            //    return _studentRepository;
            //}
            get { return _studentRepository ?? (_studentRepository = new StudentRepository(Context)); }
        }

        public IRepository<Registration> RegistrationRepository
        {
            get { return _registrationRepository ?? (_registrationRepository = new RegistrationRepository(Context)); }
        }

        public IRepository<SubjectGradesRecord> SubjectGradesRecordRepository
        {
            get {
                return _subjectGradesRecordRepository ?? (_subjectGradesRecordRepository = new SubjectGradesRecordRepository(Context));
            }
        }

        public IRepository<Grade> GradeRepository
        {
            get { return _gradeRepository ?? (_gradeRepository = new GradeRepository(Context)); }
        }

        public IRepository<Degree> DegreeRepository
        {
            get { return _degreeRepository ?? (_degreeRepository = new DegreeRepository(Context)); }
        }

        public IRepository<Level> LevelRepository
        {
            get { return _levelRepository ?? (_levelRepository = new LevelRepository(Context)); }
        }

        public IRepository<Period> PeriodRepository
        {
            get { return _periodRepository ?? (_periodRepository = new PeriodRepository(Context)); }
        }

        public IRepository<Semester> SemesterRepository
        {
            get { return _semesterRepository ?? (_semesterRepository = new SemesterRepository(Context)); }
        }

        public IRepository<Subject> SubjectRepository
        {
            get { return _subjectRepository ?? (_subjectRepository = new SubjectRepository(Context)); }
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}