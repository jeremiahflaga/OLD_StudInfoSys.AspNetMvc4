using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StudInfoSys.Controllers;
using StudInfoSys.Models;
using StudInfoSys.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
                _mockStudentRepository = new Mock<IStudentRepository>();
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
            
            //JBOY: I think this should be inside the test for the Irepository
            [TestMethod]
            public void a_list_of_Students_should_be_returned_as_the_model()
            {
                // Arrange
                var listOfStudents = new List<Student>();

                _mockStudentRepository
                    .Setup(x => x.GetAll())
                    .Returns(() => listOfStudents.AsQueryable())
                    .Verifiable();

                var studentController = GetStudentController();

                // Act
                var result = studentController.Index();

                // Assert
                _mockStudentRepository.Verify();
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


            //[TestMethod]
            //public void the_list_of_Students_should_be_sorted_by_Lastname_and_Firstname()
            //{
            //    //// Arrange
            //    //var listOfStudents = new List<Student>();

            //    //_mockStudentRepository
            //    //    .Setup(x => x.GetAll())
            //    //    .Returns(() => listOfStudents.AsQueryable())
            //    //    .Verifiable();

            //    //var studentController = GetStudentController();

            //    //// Act
            //    //var result = studentController.Index();

            //    //// Assert
            //    //_mockStudentRepository.Verify
            //}
        }
    }
}
