using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace YourDVVS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountManagement accountManagement;
        private readonly ISubjectManagement subjectManagement;
        private readonly ILogger<AccountController> logger;
        public AccountController(IAccountManagement _accountManagement, ISubjectManagement _subjectManagement,
                                 ILogger<AccountController> _logger)
        {
            accountManagement = _accountManagement;
            subjectManagement = _subjectManagement;
            logger = _logger;
        }
        [Authorize]
        public IActionResult Profile()
        {
            var user = accountManagement.GetUser(User.Identity.Name);
            var subjects = subjectManagement.GetStudentsChoice(user.Id);
            ViewData["FirstName"] = user.FirstName;
            ViewData["LastName"] = user.LastName;
            ViewData["MiddleName"] = user.MiddleName;
            ViewData["Role"] = accountManagement.GetRoleName(user.Role);
            return View(subjects);
        }
        [Authorize(Roles = "1")]
        public async Task<IActionResult> AddNewUser(User user)
        {
            if (!String.IsNullOrEmpty(user.LastName) && !String.IsNullOrEmpty(user.FirstName) && !String.IsNullOrEmpty(user.MiddleName) && !String.IsNullOrEmpty(user.Login) && !String.IsNullOrEmpty(user.Password) && user.Role != 0)
            {
                accountManagement.AddNewUser(user.LastName, user.FirstName, user.MiddleName, user.Role, user.Faculty, user.Login, user.Password);
                return RedirectToAction("Profile", "Account");
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            logger.LogInformation("{User} logged out", User.Identity.Name);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }
    }
}