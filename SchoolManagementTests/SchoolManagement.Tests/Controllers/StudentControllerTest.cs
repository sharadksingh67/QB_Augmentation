using Moq;
using NUnit.Framework;
using SchoolManagement.Controllers;
using SchoolManagement.Data;
using SchoolManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SchoolManagement.Tests.Controllers
{
    [TestFixture]
    public class StudentControllerTest
    {
        protected virtual ISchoolContext GetMockContext(IList<Course> courses, IList<Enrollment> enrollments, IList<Student> students)
        {
            var dbSetCourses = GetMockDbSet<Course>(courses);
            var dbSetEnrollments = GetMockDbSet<Enrollment>(enrollments);
            var dbSetStudents = GetMockDbSet<Student>(students);

            var mockContext = new Mock<ISchoolContext>();
            mockContext.Setup(m => m.Courses).Returns(dbSetCourses);
            mockContext.Setup(m => m.Enrollments).Returns(dbSetEnrollments);
            mockContext.Setup(m => m.Students).Returns(dbSetStudents);

            return mockContext.Object;
        }

        protected virtual DbSet<T> GetMockDbSet<T>(IList<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            mockSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => data.Add(s));
            return mockSet.Object;
        }
        
        [Test]
        public void TestStudentIndex()
        {
            var mock = GetMockContext(new List<Course>() { }, new List<Enrollment>() { }, new List<Student>() { });

            var obj = new StudentsController(mock);

            var actResult = obj.Index() as ViewResult;

            Assert.That(actResult.ViewName, Is.EqualTo("Index"));
        }
    }
}
