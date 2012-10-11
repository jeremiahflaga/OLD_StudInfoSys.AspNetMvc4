using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using StudInfoSys.Models;
using System.Data.Entity.Migrations;

namespace StudInfoSys.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<StudInfoSysContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
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
                                Registrations = CreateListOfRegistrations()
                            },

                            new Student { FirstName = "Yves Donald", LastName = "Maquilan", DateOfBirth = new DateTime(1990, 01, 01), Address = "Address", Gender = Gender.Male, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing },
                            new Student { FirstName = "Loreto", LastName = "Yubat", DateOfBirth = new DateTime(1990, 01, 01), Address = "Address", Gender = Gender.Male, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing },
                            new Student { FirstName = "Dawn", LastName = "Alcazar", DateOfBirth = new DateTime(1990, 01, 01), Address = "Address", Gender = Gender.Male, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing },
                            new Student { FirstName = "Clyde", LastName = "Goboy", DateOfBirth = new DateTime(1990, 01, 01), Address = "Address", Gender = Gender.Male, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing },
                            new Student { FirstName = "Orland", LastName = "Orland", DateOfBirth = new DateTime(1990, 01, 01), Address = "Address", Gender = Gender.Male, StudentStatus = StudentStatus.UndergraduateStudiesOnGoing }
                        };
        }


        private Registration[] CreateListOfRegistrations()
        {
            return new Registration[]
                       {
                           //First Year
                           new Registration
                               {
                                   DateOfRegistration = new DateTime(2012, 06, 01),
                                   SchoolYearFrom = 2012, SchoolYearTo = 2013,
                                   SemesterId = 1, DegreeId= 1, //IsDeleted = false, 
                                   SubjectGradesRecords = CreateListOfSubjectGradesRecordsForFirstYear()
                               },
                           //Second Year
                           new Registration
                               {
                                   DateOfRegistration = new DateTime(2012, 11, 01),
                                   SchoolYearFrom = 2012, SchoolYearTo = 2013,
                                   SemesterId = 2, DegreeId= 1, //IsDeleted = false, 
                                   SubjectGradesRecords = CreateListOfSubjectGradesRecordsForSecondYear()
                               }
                       };
        }

        private SubjectGradesRecord[] CreateListOfSubjectGradesRecordsForFirstYear()
        {
            return new SubjectGradesRecord[]
                       {
                           new SubjectGradesRecord
                               {
                                   SubjectId = 1, Grades = new Grade[]
                                                               {
                                                                   new Grade{ PeriodId = 1, GradeValue = 89},
                                                                   new Grade{ PeriodId = 2, GradeValue = 90},
                                                                   new Grade{ PeriodId = 3, GradeValue = 91}
                                                               }
                               },
                            new SubjectGradesRecord
                               {
                                   SubjectId = 2, Grades = new Grade[]
                                                               {
                                                                   new Grade{ PeriodId = 1, GradeValue = 89},
                                                                   new Grade{ PeriodId = 2, GradeValue = 90},
                                                                   new Grade{ PeriodId = 3, GradeValue = 91}
                                                               }
                               }

                       };
        }

        private SubjectGradesRecord[] CreateListOfSubjectGradesRecordsForSecondYear()
        {
            return new SubjectGradesRecord[]
                       {
                           new SubjectGradesRecord
                               {
                                   SubjectId = 3, Grades = new Grade[]
                                                               {
                                                                   new Grade{ PeriodId = 1, GradeValue = 89},
                                                                   new Grade{ PeriodId = 2, GradeValue = 90},
                                                                   new Grade{ PeriodId = 3, GradeValue = 91}
                                                               }
                               },
                            new SubjectGradesRecord
                               {
                                   SubjectId = 4, Grades = new Grade[]
                                                               {
                                                                   new Grade{ PeriodId = 1, GradeValue = 89},
                                                                   new Grade{ PeriodId = 2, GradeValue = 90},
                                                                   new Grade{ PeriodId = 3, GradeValue = 91}
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
                           new Degree{ Acronym = "BS-GD", LevelId = 4, Title = "Bachelor of Science in Graphics Design"}
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
                new Subject{ Name = "Web Develoopment", LevelId = 4, NumberOfUnits = 3, SubjectCode = "CS202"},

                new Subject{ Name = "Mathematics", LevelId = 3, NumberOfUnits = 1, SubjectCode = "Mathematics"},
                new Subject{ Name = "English", LevelId = 3, NumberOfUnits = 1, SubjectCode = "English"},
                new Subject{ Name = "Biology", LevelId = 3, NumberOfUnits = 1, SubjectCode = "Biology"},
                new Subject{ Name = "Chemistry", LevelId = 3, NumberOfUnits = 1, SubjectCode = "Chemistry"},
                new Subject{ Name = "Physics", LevelId = 3, NumberOfUnits = 1, SubjectCode = "Physics"},
                new Subject{ Name = "Filipino", LevelId = 3, NumberOfUnits = 1, SubjectCode = "Filipino"},
                new Subject{ Name = "Araling Panlipunan", LevelId = 3, NumberOfUnits = 1, SubjectCode = "Araling Panlipunan"},
                new Subject{ Name = "MAPEH", LevelId = 3, NumberOfUnits = 1, SubjectCode = "MAPEH"}
            };
        }
    }
}
