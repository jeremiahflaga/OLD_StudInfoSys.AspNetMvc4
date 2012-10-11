//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Web;
//using StudInfoSys.Models;

//namespace StudInfoSys.Repository
//{
//    public class StudentRepository : DeletableRepositoryBase<Student>, IStudentRepository
//    {
//        public StudentRepository(DbContext dataContext) : base(dataContext)
//        {
//        }

//        public IEnumerable<string> GetListOfGenders()
//        {
//            return new string[] {"Male", "Female"};
//        }

//        public IEnumerable<string> GetListOfStudentStatus()
//        {
//            return new string[] 
//            { 
//                "Preparatory - On Going",
//                "Preparatory - Finished",
//                "Elementary - On Going",
//                "Elementary - Finished",
//                "High School - On Going",
//                "High School - Finished",
//                "College/Undergraduate Studies - On Going",
//                "College/Undergraduate Studies - Finished",
//                "Graduate Studies - On Going",
//                "Graduate Studies - Finished",
//                "Not A Student Anymore" 
//            };
//        }
//    }
//}




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

        public IDictionary<int, string> GetListOfGenders()
        {
            return new Dictionary<int, string> { { 0, "Male" }, { 1, "Female" } };
        }

        public IDictionary<int, string> GetListOfStudentStatus()
        {
            return new Dictionary<int, string>
            { 
                {0,"Preparatory - On Going"},
                {1,"Preparatory - Finished"},
                {2,"Elementary - On Going"},
                {3,"Elementary - Finished"},
                {4,"High School - On Going"},
                {5,"High School - Finished"},
                {6,"College/Undergraduate Studies - On Going"},
                {7,"College/Undergraduate Studies - Finished"},
                {8,"Graduate Studies - On Going"},
                {9,"Graduate Studies - Finished"},
                {10,"Not A Student Anymore"}
            };
        }
    }
}