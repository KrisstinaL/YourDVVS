using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Microsoft.Extensions.Logging;
using DAL.Entities;
using Moq;
using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Web.Mvc;
using YourDVVS.Controllers;
using System.Linq;
using Autofac.Extras.Moq;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using BLL.Services;
using static BLL.Services.AccountManagement;

namespace TestProject
{
    public class TestLogin
    {
        private List<User> TestUsers = new List<User>
                {
                    new User
                    {
                        Login = "testLogin",
                        FirstName = "testFirstName",
                        MiddleName ="testMiddlrName",
                        LastName = "testLastName",
                        Password="Qqwerty",
                        Role=1,
                        UserId=1
                    },
                    new User
                    {
                        Login = "testLogin1",
                        FirstName = "testFirstName1",
                        MiddleName ="testMiddlrName1",
                        LastName = "testLastName1",
                        Password="Qqwerty",
                        Role=1,
                        UserId=2
                    },
                    new User
                    {
                        Login = "testLogin2",
                        FirstName = "testFirstName2",
                        MiddleName ="testMiddlrName2",
                        LastName = "testLastName2",
                        Password="Qqwerty",
                        Role=1,
                        UserId=3
                    },
                    new User
                    {
                        Login = "testLogin3",
                        FirstName = "testFirstName3",
                        MiddleName ="testMiddlrName3",
                        LastName = "testLastName3",
                        Password="Qqwerty",
                        Role=1,
                        UserId=4
                    },
                    new User
                    {
                        Login = "testLogin4",
                        FirstName = "testFirstName4",
                        MiddleName ="testMiddlrName4",
                        LastName = "testLastName4",
                        Password="Qqwerty",
                        Role=1,
                        UserId=5
                    },
                    new User
                    {
                        Login = "testLogin5",
                        FirstName = "testFirstName5",
                        MiddleName ="testMiddlrName5",
                        LastName = "testLastName5",
                        Password="Qqwerty",
                        Role=1,
                        UserId=6
                    }
                };
        //OddTest
        //ToDelete\
        Mock<DbSet<User>> mockSet = new Mock<DbSet<User>>();

        [Theory]
        [InlineData("testLogin", 1)]
        [InlineData("testLogin1", 2)]
        public void GetUserTest(string actualLogin, int expectedId)
        {
            using (var mock = AutoMock.GetLoose())
            {
                IQueryable<User> userlist = TestUsers.AsQueryable();
                mockSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(userlist.Provider);
                mockSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(userlist.Expression);
                mock.Mock<AplicationContext>().SetupGet(x => x.User).Returns(mockSet.Object);
                var AccountManagementMock = mock.Create<AccountManagement>();
                var actualUser = AccountManagementMock.GetUser(actualLogin);
                Assert.Equal(expectedId, actualUser.UserId);
            }
        }
        [Fact]
        public void GetUserUserNotFoundExceptionTest()
        {
            using (var mock = AutoMock.GetLoose())
            {
                IQueryable<User> userlist = TestUsers.AsQueryable();
                mockSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(userlist.Provider);
                mockSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(userlist.Expression);
                mock.Mock<AplicationContext>().SetupGet(x => x.User).Returns(mockSet.Object);
                var AccountManagementMock = mock.Create<AccountManagement>();
                string unexistingLogin = "unexistingLogin";
                Action tryToFindUser = () => AccountManagementMock.GetUser(unexistingLogin);
                Assert.Throws<UserNotFoundException>(tryToFindUser);
            }
        }
        [Theory]
        [InlineData("testLogin", "Qqwerty", true)]
        [InlineData("testLogin", "WrongPassword", false)]
        public void VerifyTest(string Login, string Password, bool expectedVerificationValue)
        {
            using (var mock = AutoMock.GetLoose())
            {
                IQueryable<User> userlist = TestUsers.AsQueryable();
                mockSet.As<IQueryable<User>>().Setup(x => x.Provider).Returns(userlist.Provider);
                mockSet.As<IQueryable<User>>().Setup(x => x.Expression).Returns(userlist.Expression);
                mock.Mock<AplicationContext>().SetupGet(x => x.User).Returns(mockSet.Object);
                var AccountManagementMock = mock.Create<AccountManagement>();
                bool actualVerificationValue = AccountManagementMock.Verify(Login, Password);
                Assert.Equal(expectedVerificationValue, actualVerificationValue);
            }
        }
    }
}

