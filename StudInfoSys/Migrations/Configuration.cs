using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using StudInfoSys.Models;
using System.Data.Entity.Migrations;
using StudInfoSys.Repository;

namespace StudInfoSys.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<StudInfoSysContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

            _unitOfWork = new UnitOfWork(new StudInfoSysContext());
        }

        protected override void Seed(StudInfoSysContext context)
        {
            context.Levels.AddOrUpdate(l => l.Name, CreateListOfLevels());
            context.SaveChanges();

            context.Degrees.AddOrUpdate(d => d.Acronym, CreateListOfDegrees());
            context.SaveChanges();

            context.Periods.AddOrUpdate(p => new { p.Name, p.LevelId }, CreateListOfPeriod());
            context.SaveChanges();

            context.Semesters.AddOrUpdate(s => s.Name, CreateListOfSemesters());
            context.SaveChanges();

            context.Subjects.AddOrUpdate(s => s.SubjectCode, CreateListOfSubjects());
            context.SaveChanges();

            context.Students.AddOrUpdate(s => new { s.FirstName, s.LastName }, CreateListOfStudents());


            //context.Database.ExecuteSqlCommand("ALTER TABLE Subjects ADD UNIQUE (SubjectCode);");
            //context.Database.ExecuteSqlCommand("ALTER TABLE Subjects ADD CONSTRAINT con_first UNIQUE (SubjectCode);");
        }

        private IUnitOfWork _unitOfWork;

        private Student[] CreateListOfStudents()
        {
            return new Student[]
                       {
                            new Student
                            {
                                FirstName = "Jboy",
                                LastName = "Flaga",
                                DateOfBirth = new DateTime(1990, 01, 01),
                                Address = "Kidapawan City, North Cotabato, Philippines, 9400",
                                Gender = Gender.Male,
                                StudentStatus = StudentStatus.UndergraduateStudiesOnGoing,
                                Email = "jeremiahflaga@gmail.com",
                                Registrations = CreateListOfRegistrations()
                            },

                            new Student { FirstName = "Rimmy Joy", LastName = "Flaga", DateOfBirth = new DateTime(1990, 01, 01), Address = "Address", Gender = Gender.Female, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing, Email = "mail@mail.com" },
                            new Student { FirstName = "Debbie", LastName = "Flaga", DateOfBirth = new DateTime(1990, 01, 01), Address = "Address", Gender = Gender.Male, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing, Email = "mail@mail.com" },
                            new Student { FirstName = "Jonathan", LastName = "Flaga", DateOfBirth = new DateTime(1990, 01, 01), Address = "Address", Gender = Gender.Male, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing, Email = "mail@mail.com" },                            
                            new Student { FirstName = "Yves Donald", LastName = "Maquilan", DateOfBirth = new DateTime(1990, 01, 01), Address = "Address", Gender = Gender.Male, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing, Email = "mail@mail.com" },
                            new Student { FirstName = "Donald", LastName = "Magallena", DateOfBirth = new DateTime(1990, 01, 01), Address = "Address", Gender = Gender.Male, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing, Email = "mail@mail.com" },
                            new Student { FirstName = "Averyveryveryverylongfirstname", LastName = "Averyveryveryverylonglastname", DateOfBirth = new DateTime(1990, 01, 01), Address = "Veryveryveryveryveryveryveryveryveryveryveryveryveryveryvery long address", Gender = Gender.Male, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing, Email = "mail@mail.com" },
                            new Student { FirstName = "Zeryveryveryverylongfirstname", LastName = "Zeryveryveryverylonglastname", DateOfBirth = new DateTime(1990, 01, 01), Address = "Veryveryveryveryveryveryveryveryveryveryveryveryveryveryvery long Address", Gender = Gender.Male, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing, Email = "mail@mail.com" }
                           
                        };
        }


        private Registration[] CreateListOfRegistrations()
        {
            return new Registration[]
                       {
                           //First Year College
                           new Registration
                               {
                                   DateOfRegistration = new DateTime(2012, 06, 01),
                                   SchoolYearFrom = 2012, SchoolYearTo = 2013,
                                   SemesterId = 1, DegreeId= 1, IsDeleted = false,
                                   //Degree = new Degree{Id = 2},
                                   SubjectGradesRecords = CreateListOfSubjectGradesRecordsForFirstYearCollege()
                               },
                           //Second Year College
                           new Registration
                               {
                                   DateOfRegistration = new DateTime(2012, 11, 01),
                                   SchoolYearFrom = 2012, SchoolYearTo = 2013,
                                   SemesterId = 2, DegreeId= 1, IsDeleted = false, 
                                   SubjectGradesRecords = CreateListOfSubjectGradesRecordsForSecondYearCollege()
                               },
                           //Fourth Year High School
                           new Registration
                               {
                                   DateOfRegistration = new DateTime(2004, 06, 01),
                                   SchoolYearFrom = 2004, SchoolYearTo = 2005,
                                   SemesterId = 4, DegreeId= 7, IsDeleted = false, 
                                   SubjectGradesRecords = CreateListOfSubjectGradesRecordsForFourthYearHighSchool()
                               }
                       };

            //var semester1 = _unitOfWork.SemesterRepository.GetById(1);
            //var semester2 = _unitOfWork.SemesterRepository.GetById(2);

            //var degree1 = _unitOfWork.DegreeRepository.GetById(1);
            //var degree2 = _unitOfWork.DegreeRepository.GetById(2);


            //return new Registration[]
            //           {
            //               //First Year
            //               new Registration
            //                   {
            //                       DateOfRegistration = new DateTime(2012, 06, 01),
            //                       SchoolYearFrom = 2012, SchoolYearTo = 2013,
            //                       Semester = semester1, Degree = degree1, //IsDeleted = false,
            //                       //Degree = new Degree{Id = 2},
            //                       SubjectGradesRecords = CreateListOfSubjectGradesRecordsForFirstYear()
            //                   },
            //               //Second Year
            //               new Registration
            //                   {
            //                       DateOfRegistration = new DateTime(2012, 11, 01),
            //                       SchoolYearFrom = 2012, SchoolYearTo = 2013,
            //                       Semester = semester2, Degree = degree2, //IsDeleted = false, 
            //                       SubjectGradesRecords = CreateListOfSubjectGradesRecordsForSecondYear()
            //                   }
            //           };
        }

        private SubjectGradesRecord[] CreateListOfSubjectGradesRecordsForFirstYearCollege()
        {
            return new SubjectGradesRecord[]
                       {
                           new SubjectGradesRecord
                               {
                                   SubjectId = 1, Grades = new Grade[]
                                                               {
                                                                   new Grade{ PeriodId = 1, GradeValue = 84},
                                                                   new Grade{ PeriodId = 2, GradeValue = 85},
                                                                   new Grade{ PeriodId = 3, GradeValue = 86}
                                                               }
                               },
                            new SubjectGradesRecord
                               {
                                   SubjectId = 2, Grades = new Grade[]
                                                               {
                                                                   new Grade{ PeriodId = 1, GradeValue = 84},
                                                                   new Grade{ PeriodId = 2, GradeValue = 85},
                                                                   new Grade{ PeriodId = 3, GradeValue = 86}
                                                               }
                               }

                       };
        }

        private SubjectGradesRecord[] CreateListOfSubjectGradesRecordsForSecondYearCollege()
        {
            return new SubjectGradesRecord[]
                       {
                           new SubjectGradesRecord
                               {
                                   SubjectId = 3, Grades = new Grade[]
                                                               {
                                                                   new Grade{ PeriodId = 1, GradeValue = 84},
                                                                   new Grade{ PeriodId = 2, GradeValue = 85},
                                                                   new Grade{ PeriodId = 3, GradeValue = 86}
                                                               }
                               },
                            new SubjectGradesRecord
                               {
                                   SubjectId = 4, Grades = new Grade[]
                                                               {
                                                                   new Grade{ PeriodId = 1, GradeValue = 84},
                                                                   new Grade{ PeriodId = 2, GradeValue = 85},
                                                                   new Grade{ PeriodId = 3, GradeValue = 86}
                                                               }
                               }

                       };
        }

        private SubjectGradesRecord[] CreateListOfSubjectGradesRecordsForFourthYearHighSchool()
        {
            return new SubjectGradesRecord[]
                       {
                           new SubjectGradesRecord
                               {
                                   SubjectId = 5, Grades = new Grade[]
                                                               {
                                                                   new Grade{ PeriodId = 7, GradeValue = 84},
                                                                   new Grade{ PeriodId = 8, GradeValue = 85},
                                                                   new Grade{ PeriodId = 9, GradeValue = 86},
                                                                   new Grade{ PeriodId = 10, GradeValue = 87}
                                                               }
                               },
                            new SubjectGradesRecord
                               {
                                   SubjectId = 6, Grades = new Grade[]
                                                               {
                                                                   new Grade{ PeriodId = 7, GradeValue = 84},
                                                                   new Grade{ PeriodId = 8, GradeValue = 85},
                                                                   new Grade{ PeriodId = 9, GradeValue = 86},
                                                                   new Grade{ PeriodId = 10, GradeValue = 87}
                                                               }
                               }

                       };
        }

        private Degree[] CreateListOfDegrees()
        {
            return new Degree[]
                       {
                           new Degree{ Acronym = "BSCS", LevelId = 4, Title = "Bachelor of Science in Computer Science"},
                           new Degree{ Acronym = "BSME", LevelId = 4, Title = "Bachelor of Science in Mechanical Engineering"},
                           new Degree{ Acronym = "BS-GD", LevelId = 4, Title = "Bachelor of Science in Graphics Design"},

                           new Degree{ Acronym = "HS-1", LevelId = 3, Title = "1st Year High School"},
                           new Degree{ Acronym = "HS-2", LevelId = 3, Title = "2nd Year High School"},
                           new Degree{ Acronym = "HS-3", LevelId = 3, Title = "3rd Year High School"},
                           new Degree{ Acronym = "HS-4", LevelId = 3, Title = "4th Year High School"},

                           new Degree{ Acronym = "Elem-1", LevelId = 2, Title = "Grade 1"},
                           new Degree{ Acronym = "Elem-2", LevelId = 2, Title = "Grade 2"},
                           new Degree{ Acronym = "Elem-3", LevelId = 2, Title = "Grade 3"},
                           new Degree{ Acronym = "Elem-4", LevelId = 2, Title = "Grade 4"},
                           new Degree{ Acronym = "Elem-5", LevelId = 2, Title = "Grade 5"},
                           new Degree{ Acronym = "Elem-6", LevelId = 2, Title = "Grade 6"},

                           new Degree{ Acronym = "Prep-Nursery", LevelId = 1, Title = "Nursery"},
                           new Degree{ Acronym = "Prep-K1", LevelId = 1, Title = "Kinder 1"},
                           new Degree{ Acronym = "Prep-K2", LevelId = 1, Title = "Kinder 2"}
                       };
        }

        private Level[] CreateListOfLevels()
        {
            return new Level[]
            {
                new Level{ Name="Preparatory"},
                new Level{ Name="Elementary"},
                new Level{ Name="High School"},
                new Level{ Name="College"},
                new Level{ Name="Graduate"}
            };
        }

        private Period[] CreateListOfPeriod()
        {
            return new Period[]
            {
                
                new Period{Name = "Prelim", LevelId = 4},
                new Period{Name = "Midterm", LevelId = 4},
                new Period{Name = "Finals", LevelId = 4},

                new Period{Name = "Prelim", LevelId = 5},
                new Period{Name = "Midterm", LevelId = 5},
                new Period{Name = "Finals", LevelId = 5},

                new Period{Name = "1st Grading", LevelId = 1},
                new Period{Name = "2nd Grading", LevelId = 1},
                new Period{Name = "3rd Grading", LevelId = 1},
                new Period{Name = "4th Grading", LevelId = 1},

                new Period{Name = "1st Grading", LevelId = 2},
                new Period{Name = "2nd Grading", LevelId = 2},
                new Period{Name = "3rd Grading", LevelId = 2},
                new Period{Name = "4th Grading", LevelId = 2},

                new Period{Name = "1st Grading", LevelId = 3},
                new Period{Name = "2nd Grading", LevelId = 3},
                new Period{Name = "3rd Grading", LevelId = 3},
                new Period{Name = "4th Grading", LevelId = 3}
            };
        }

        private Semester[] CreateListOfSemesters()
        {
            return new Semester[]
            {
                new Semester{ Name = "1st Semester"},
                new Semester{ Name = "2nd Semester"},
                new Semester{ Name = "Summer"},
                new Semester{ Name = "Not Applicable"}
            };
        }

        private Subject[] CreateListOfSubjects()
        {
            return new Subject[]
            {
                new Subject{ Name = "Introduction to Computer Science", LevelId = 4, NumberOfUnits = 3, SubjectCode = "CS101"},
                new Subject{ Name = "Data Structures", LevelId = 4, NumberOfUnits = 3, SubjectCode = "CS102"},
                new Subject{ Name = "Introduction to Algorithms", LevelId = 4, NumberOfUnits = 3, SubjectCode = "CS201"},
                new Subject{ Name = "Web Development", LevelId = 4, NumberOfUnits = 3, SubjectCode = "CS202"},

                new Subject{ Name = "Mathematics", LevelId = 3, NumberOfUnits = 1, SubjectCode = "HS-Math"},
                new Subject{ Name = "English", LevelId = 3, NumberOfUnits = 1, SubjectCode = "HS-English"},
                new Subject{ Name = "Biology", LevelId = 3, NumberOfUnits = 1, SubjectCode = "HS-Biology"},
                new Subject{ Name = "Chemistry", LevelId = 3, NumberOfUnits = 1, SubjectCode = "HS-Chemistry"},
                new Subject{ Name = "Physics", LevelId = 3, NumberOfUnits = 1, SubjectCode = "HS-Physics"},
                new Subject{ Name = "Filipino", LevelId = 3, NumberOfUnits = 1, SubjectCode = "HS-Filipino"},
                new Subject{ Name = "Araling Panlipunan", LevelId = 3, NumberOfUnits = 1, SubjectCode = "HS-AP"},
                new Subject{ Name = "MAPEH", LevelId = 3, NumberOfUnits = 1, SubjectCode = "HS-MAPEH"},

                new Subject{ Name = "Mathematics", LevelId = 2, NumberOfUnits = 1, SubjectCode = "Elem-Math"},
                new Subject{ Name = "English", LevelId = 2, NumberOfUnits = 1, SubjectCode = "Elem-English"},
                new Subject{ Name = "General Science", LevelId = 2, NumberOfUnits = 1, SubjectCode = "Elem-Science"},
                new Subject{ Name = "Physical Education", LevelId = 2, NumberOfUnits = 1, SubjectCode = "Elem-PE"},
                new Subject{ Name = "Filipino", LevelId = 2, NumberOfUnits = 1, SubjectCode = "Elem-Filipino"},
                new Subject{ Name = "Social Studies", LevelId = 2, NumberOfUnits = 1, SubjectCode = "Elem-SS"},
                new Subject{ Name = "Home Economics", LevelId = 2, NumberOfUnits = 1, SubjectCode = "Elem-HE"},

                new Subject{ Name = "Mathematics", LevelId = 1, NumberOfUnits = 1, SubjectCode = "Prep-Math"},
                new Subject{ Name = "English", LevelId = 1, NumberOfUnits = 1, SubjectCode = "Prep-English"},
                new Subject{ Name = "General Science", LevelId = 1, NumberOfUnits = 1, SubjectCode = "Prep-Science"},
                new Subject{ Name = "Physical Education", LevelId = 1, NumberOfUnits = 1, SubjectCode = "Prep-PE"},
                new Subject{ Name = "Filipino", LevelId = 1, NumberOfUnits = 1, SubjectCode = "Prep-Filipino"},
                new Subject{ Name = "Social Studies", LevelId = 1, NumberOfUnits = 1, SubjectCode = "Prep-SS"}
            };
        }
    }
}
