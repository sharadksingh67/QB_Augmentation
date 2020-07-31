using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SchoolManagement.UITests
{
    public class SchoolManagementSystemTests : IDisposable
    {
        private readonly IWebDriver _driver;

        public SchoolManagementSystemTests()
        {
            _driver = new ChromeDriver(@"D:\Assignments\Cognizant\ICT\Now\SchoolManagementTests\SchoolManagement.UITests\bin\Debug");
        }

        [Fact]
        public void Test_Index()
        {
            _driver.Navigate().GoToUrl("https://localhost:44316/Student/index");

            Assert.Equal("Index - Student Management Application", _driver.Title);
            var links = _driver.FindElements(By.TagName("a"));


            Assert.Equal("School Management System", links[0].Text);

            Assert.Equal("Home", links[1].Text);

            Assert.Equal("Create New", links[2].Text);

        }

        [Fact]
        public void Test_Details()
        {
            _driver.Navigate().GoToUrl("https://localhost:44316/Students/Details/1");

            Assert.Equal("Details - Restaurant Management Application", _driver.Title);
            var links =
                _driver.FindElements(By.TagName("a"));

            Assert.Equal("Back to List", links[2].Text);
        }

        [Fact]
        public void Test_AddStudent_RequiredFields()
        {
            _driver.Navigate().GoToUrl("https://localhost:44316/Students/Create");

            _driver.FindElement(By.Id("Create")).Click();

            IWebElement nameErrorMessage =
                _driver.FindElement(By.Id("LastName-error"));

            Assert.Equal("The Last Name field is required.", nameErrorMessage.Text);
        }

        [Fact]
        public void Test_AddStudent_RequiredFields_FirstName()
        {
            _driver.Navigate().GoToUrl("https://localhost:44316/Students/Create");

            _driver.FindElement(By.Id("Create")).Click();

            IWebElement nameErrorMessage =
                _driver.FindElement(By.Id("FirstMidName-error"));

            Assert.Equal("The First Name field is required.", nameErrorMessage.Text);
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
