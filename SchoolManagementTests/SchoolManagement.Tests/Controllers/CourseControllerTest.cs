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
    public class CourseControllerTest
    {
        List<Student> students;
        List<Course> courses;
        List<Enrollment> enrollments;

        [SetUp]
        public void Setup()
        {
            //assembly = Assembly.Load("SchoolManagement");
            students = new List<Student>
            {
                new Student {ID=1, FirstMidName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2020-09-01") },
                new Student {ID=2, FirstMidName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2020-09-01") },
                new Student {ID=3, FirstMidName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2020-09-01") },
                new Student {ID=4, FirstMidName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2020-09-01") },
                new Student {ID=5, FirstMidName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2020-09-01") },
                new Student {ID=6, FirstMidName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2020-09-01") },
                new Student {ID=7, FirstMidName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2020-09-01") },
                new Student {ID=8, FirstMidName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2020-09-01") }
            };

            courses = new List<Course>
            {
                new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3 },
                new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3 },
                new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3 },
                new Course {CourseID = 1045, Title = "Calculus",       Credits = 4 },
                new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4 },
                new Course {CourseID = 2021, Title = "Composition",    Credits = 3 },
                new Course {CourseID = 2042, Title = "Literature",     Credits = 4 },
            };

            enrollments = new List<Enrollment>
            {
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                    Grade = Grade.A
                },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                    Grade = Grade.C
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alexander").ID,
                    CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                     StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                    CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Anand").ID,
                    CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                    Grade = Grade.B
                 },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                    CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Li").ID,
                    CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                    Grade = Grade.B
                 },
                 new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Justice").ID,
                    CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                    Grade = Grade.B
                 }
            };
        }

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
        public void TestCourseIndex()
        {
            var mock = GetMockContext(courses, enrollments, students);

            var courseController = new CourseController(mock);

            Assert.IsNotNull(courseController);
            Assert.IsInstanceOf(typeof(CourseController), courseController);

            var actResult = courseController.Index() as ViewResult;

            var courseList = actResult.Model as List<Course>;
            Assert.AreEqual(courseList[0].CourseID, 1050);
            Assert.AreEqual(courseList[0].Title, "Chemistry");
        }

        [Test]
        public void TestCourseDetailsNotFound()
        {
            var mock = GetMockContext(courses, enrollments, students);

            var courseController = new CourseController(mock);

            Assert.IsNotNull(courseController);
            Assert.IsInstanceOf(typeof(CourseController), courseController);

            var actResult = courseController.Details(2) as ViewResult;

            Assert.IsNull(actResult);
        }

        [Test]
        public void TestCourseCreate()
        {
            var mock = GetMockContext(courses, enrollments, students);

            var courseController = new CourseController(mock);

            Assert.IsNotNull(courseController);
            Assert.IsInstanceOf(typeof(CourseController), courseController);

            var actResult = courseController.Create() as ViewResult;

            Assert.IsNotNull(actResult);
        }

        [Test]
        public void TestCourseCreate_WithStudentParam_RedirectRoute()
        {
            var mock = GetMockContext(courses, enrollments, students);

            var courseController = new CourseController(mock);

            Assert.IsNotNull(courseController);
            Assert.IsInstanceOf(typeof(CourseController), courseController);

            Course course = new Course { CourseID = 1050, Title = "Chemistry", Credits = 3 };

            RedirectToRouteResult redirectRoute = courseController.Create(course) as RedirectToRouteResult;

            Assert.IsNotNull(redirectRoute);
        }

        [Test]
        public void TestCourseEdit()
        {
            var mock = GetMockContext(courses, enrollments, students);

            var courseController = new CourseController(mock);

            Assert.IsNotNull(courseController);
            Assert.IsInstanceOf(typeof(CourseController), courseController);

            var actResult = courseController.Edit(2) as HttpNotFoundResult;

            Assert.IsNotNull(actResult);
            Assert.AreEqual(actResult.StatusCode, 404);
        }

        [Test]
        public void TestCourseDelete()
        {
            var mock = GetMockContext(courses, enrollments, students);

            var courseController = new CourseController(mock);

            Assert.IsNotNull(courseController);
            Assert.IsInstanceOf(typeof(CourseController), courseController);

            var actResult = courseController.Delete(2) as HttpNotFoundResult;

            Assert.IsNotNull(actResult);
            Assert.AreEqual(actResult.StatusCode, 404);
        }
    }
}
