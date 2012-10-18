using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudInfoSys.Controllers;
using StudInfoSys.Models;
using StudInfoSys.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudInfoSys.Tests.Controllers
{
    [TestClass]
    public class StudentControllerTest
    {
        [TestClass]
        public class When_trying_to_load_index_page
        {
            private Mock<IStudentRepository> _mockStudentRepository;
            public When_trying_to_load_index_page ()
	        {
                _mockStudentRepository = new Mock<IStudentRepository>() 
                                            { DefaultValue = DefaultValue.Mock };
	        }

            private StudentController GetStudentController()
            {
                var studentController = new StudentController(_mockStudentRepository.Object);
                return studentController;
            }

            [TestMethod]
            public void a_ViewResult_should_be_returned()
            {
                // Arrange
                var studentController = GetStudentController();

                // Act
                var result = studentController.Index();

                // Assert
                Assert.IsInstanceOfType(result, typeof(ViewResult));
            }
            
            [TestMethod]
            public void a_list_of_Students_should_be_returned_as_the_Model()
            {
                // Arrange                
                var studentController = GetStudentController();

                // Act
                var result = studentController.Index();

                // Assert
                Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<Student>));
            }


            [TestMethod]
            public void the_Model_of_the_ViewResult_should_not_be_null()
            {
                // Arrange
                var studentController = GetStudentController();

                // Act
                var result = studentController.Index();

                // Assert
                Assert.IsNotNull(result.Model);
            }

            [TestMethod]
            public void the_number_of_Students_in_the_model_should_be_N()
            {
                // Arrange
                var listOfStudents = new List<Student>{
                    new Student(),
                    new Student(),
                    new Student()
                };

                _mockStudentRepository
                    .Setup(x => x.GetAll())
                    .Returns(listOfStudents.AsQueryable());
                var studentController = GetStudentController();

                //var students = _mockStudentRepository.Object.GetAll();
                //students.OrderBy(

                // Act
                var result = studentController.Index();
                var model = result.Model as IEnumerable<Student>;

                // Assert
                Assert.AreEqual(model.Count(), listOfStudents.Count());
            }

            [TestMethod]
            public void the_list_of_Students_should_be_sorted_by_Lastname_and_Firstname()
            {
                // Arrange
                var listOfStudents = new List<Student>{
                    new Student{FirstName="2-Second", LastName="Flaga"},
                    new Student{FirstName="1-First", LastName="Flaga"},
                    new Student{FirstName="3-Third", LastName="Floro"}
                };
                                
                _mockStudentRepository
                    .Setup(x => x.GetAll())
                    .Returns(listOfStudents.AsQueryable());
                var studentController = GetStudentController();

                var students = _mockStudentRepository.Object.GetAll();
                var studentsOrderedByLastName = students.OrderBy(s => s.LastName).ToList();

                // Act
                var result = studentController.Index();
                var model = (result.Model as IEnumerable<Student>).ToList();

                // Assert
                var sortedListOfStudents = listOfStudents.OrderBy(s=>s.LastName).ThenBy(s2=>s2.FirstName).ToList();
                Assert.AreEqual(model[0], sortedListOfStudents[0]);
                Assert.AreEqual(model[1], sortedListOfStudents[1]);
                Assert.AreEqual(model[2], sortedListOfStudents[2]);
            }
        }
    }
}
