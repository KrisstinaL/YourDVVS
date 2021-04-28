using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using Xunit;
using YourDVVS.Controllers;
using static BLL.Services.AccountManagement;

namespace TestProject
{
    public class LoginControllerTests
    {
        private User testuser = new User
        {
            FirstName = "TestUserFirstname",
            MiddleName = "testUserMiddlename",
            LastName = "testUserLastname",
            Login = "testLogin",
            Password = "testPassword",
            Role = 1
        };
        [Fact]
        public void AuthorizeAthenticatedUserTest()
        {
            var accmock = new Mock<IAccountManagement>();
            var logmock = new Mock<ILogger<LoginController>>();
            string expectedPageName = "Profile";
            LoginController controller = new LoginController(accmock.Object, logmock.Object);
            var mock = new Mock<HttpContext>();
            mock.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(true);
            controller.ControllerContext.HttpContext = mock.Object;
            var result = controller.Authorize(It.IsAny<User>()).Result as RedirectToActionResult;
            string actualPageName = result.ActionName;
            Assert.Equal(expectedPageName, actualPageName);
        }
        [Fact]
        public void AuthorizeUnveryfiebleUserTest()
        {
            var accmock = new Mock<IAccountManagement>();
            accmock.Setup(x => x.Verify(It.IsAny<String>(), It.IsAny<String>())).Returns(false);
            var logmock = new Mock<ILogger<LoginController>>();
            string expectedPageName = "Error";
            LoginController controller = new LoginController(accmock.Object, logmock.Object);
            var mock = new Mock<HttpContext>();
            mock.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(false);
            controller.ControllerContext.HttpContext = mock.Object;
            var result = controller.Authorize(testuser).Result as ViewResult;
            string actualPageName = result.ViewName;
            Assert.Equal(expectedPageName, actualPageName);
        }
        [Fact]
        public void AuthorizeUserWithWrongPasswordTest()
        {
            var accmock = new Mock<IAccountManagement>();
            accmock.Setup(x => x.Verify(It.IsAny<String>(), It.IsAny<String>())).Throws(new UserNotFoundException());
            var logmock = new Mock<ILogger<LoginController>>();
            string expectedPageName = "Error";
            LoginController controller = new LoginController(accmock.Object, logmock.Object);
            var mock = new Mock<HttpContext>();
            mock.SetupGet(x => x.User.Identity.IsAuthenticated).Returns(false);
            controller.ControllerContext.HttpContext = mock.Object;
            var result = controller.Authorize(testuser).Result as ViewResult;
            string actualPageName = result.ViewName;
            Assert.Equal(expectedPageName, actualPageName);
        }
    }

}
