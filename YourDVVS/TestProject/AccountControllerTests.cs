using System;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using YourDVVS.Controllers;
namespace TestProject
{
    public class AccountControllerTests
    {
        private User testuser = new User
        {
            FirstName = "TestUserFirstname",
            MiddleName = "testUserMiddlename",
            LastName = "testUserLastname",
            Role = 1
        };
        [Fact]
        public void ProfileViewResultNotNullTest()
        {
            var accmock = new Mock<IAccountManagement>();
            var sbjmock = new Mock<ISubjectManagement>();
            var logmock = new Mock<ILogger<AccountController>>();
            accmock.Setup(a => a.GetUser(It.IsAny<String>())).Returns(testuser);
            AccountController controller = new AccountController(accmock.Object, sbjmock.Object, logmock.Object);
            var mock = new Mock<HttpContext>();
            mock.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(true);
            controller.ControllerContext.HttpContext = mock.Object;
            var result = controller.Profile() as ViewResult;
            Assert.NotNull(result);
        }
        [Fact]
        public void ProfileViewDataIsNotNullTests()
        {
            var accmock = new Mock<IAccountManagement>();
            var sbjmock = new Mock<ISubjectManagement>();
            var logmock = new Mock<ILogger<AccountController>>();
            accmock.Setup(a => a.GetUser(It.IsAny<string>())).Returns(testuser);
            var subjMoc = new Mock<ISubjectManagement>();
            AccountController controller = new AccountController(accmock.Object, sbjmock.Object, logmock.Object);
            var mock = new Mock<HttpContext>();
            mock.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(true);
            mock.SetupGet(x => x.User.Identity.Name).Returns("sdjsl");
            controller.ControllerContext.HttpContext = mock.Object;
            Assert.NotNull((controller.Profile() as ViewResult)?.ViewData["FirstName"]);
        }
        [Fact]
        public void ProfileViewDataIsValidTests()
        {
            var accmock = new Mock<IAccountManagement>();
            var sbjmock = new Mock<ISubjectManagement>();
            var logmock = new Mock<ILogger<AccountController>>();
            accmock.Setup(a => a.GetUser(It.IsAny<string>())).Returns(testuser);
            var subjMoc = new Mock<ISubjectManagement>();
            AccountController controller = new AccountController(accmock.Object, sbjmock.Object, logmock.Object);
            var mock = new Mock<HttpContext>();
            mock.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(true);
            controller.ControllerContext.HttpContext = mock.Object;
            Assert.Equal(testuser.LastName, (controller.Profile() as ViewResult).ViewData["LastName"]);
        }
    }
}
