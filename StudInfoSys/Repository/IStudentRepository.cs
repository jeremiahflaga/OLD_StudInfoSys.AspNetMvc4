//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using StudInfoSys.Models;

//namespace StudInfoSys.Repository
//{
//    public interface IStudentRepository : IDeletableRepository<Student>
//    {
//        IEnumerable<string> GetListOfGenders();
//        IEnumerable<string> GetListOfStudentStatus();
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudInfoSys.Models;

namespace StudInfoSys.Repository
{
    public interface IStudentRepository : IDeletableRepository<Student>
    {
        IDictionary<int, string> GetListOfGenders();
        IDictionary<int, string> GetListOfStudentStatus();
    }
}