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
        private readonly DbContext _context;

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
            _context = context;
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
            get { return _studentRepository ?? (_studentRepository = new StudentRepository(_context)); }
        }

        public IRepository<Registration> RegistrationRepository
        {
            get { return _registrationRepository ?? (_registrationRepository = new RegistrationRepository(_context)); }
        }

        public IRepository<SubjectGradesRecord> SubjectGradesRecordRepository
        {
            get {
                return _subjectGradesRecordRepository ?? (_subjectGradesRecordRepository = new SubjectGradesRecordRepository(_context));
            }
        }

        public IRepository<Grade> GradeRepository
        {
            get { return _gradeRepository ?? (_gradeRepository = new GradeRepository(_context)); }
        }

        public IRepository<Degree> DegreeRepository
        {
            get { return _degreeRepository ?? (_degreeRepository = new DegreeRepository(_context)); }
        }

        public IRepository<Level> LevelRepository
        {
            get { return _levelRepository ?? (_levelRepository = new LevelRepository(_context)); }
        }

        public IRepository<Period> PeriodRepository
        {
            get { return _periodRepository ?? (_periodRepository = new PeriodRepository(_context)); }
        }

        public IRepository<Semester> SemesterRepository
        {
            get { return _semesterRepository ?? (_semesterRepository = new SemesterRepository(_context)); }
        }

        public IRepository<Subject> SubjectRepository
        {
            get { return _subjectRepository ?? (_subjectRepository = new SubjectRepository(_context)); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}