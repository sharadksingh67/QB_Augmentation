using Moq;
using NUnit.Framework;
using SchoolManagement.Controllers;
using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SchoolManagement.Tests.Controllers
{
    [TestFixture]
    public class StudentControllerTest
    {
        [Test]
        public void TestStudentIndex()
        {

            //var mock = new Mock<IDbContext>();

            //mock.Setup(x => x.Set<Student>())
            //.Returns(new FakeDbSet<Student>
            //{
            //    new Student{ID=567567, FirstMidName="Abdullah", LastName="Khan", EnrollmentDate=new DateTime()}
            //});

            var obj = new StudentsController();

            var actResult = obj.Index() as ViewResult;

            Assert.That(actResult.ViewName, Is.EqualTo("Index"));
        }
    }
}
