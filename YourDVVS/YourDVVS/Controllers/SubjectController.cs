using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BLL.Interfaces;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Threading.Tasks;
namespace YourDVVS.Controllers
{
    [Authorize]
    public class SubjectController : Controller
    {
        
            private readonly IAccountManagement accountManagement;
            private readonly ISubjectManagement subjectManagement;
            private readonly ILogger<SubjectController> logger;
            public SubjectController(IAccountManagement _accountManagement,
                                     ISubjectManagement _subjectManagement,
                                     ILogger<SubjectController> _logger)
            {
                accountManagement = _accountManagement;
                subjectManagement = _subjectManagement;
                logger = _logger;
            }

            [Authorize]
            public ViewResult SubjectsFirstTerm(string Search)
            {
                var user = accountManagement.GetUser(User.Identity.Name);
                Subject[] subjectsList;
                ViewData["Information"] = "";
                if (user.Role == 3)
                {
                    var student = accountManagement.GetStudent(user.Id);
                    if (student.Course == 1)
                    {
                        subjectsList = subjectManagement.GetSubjects(3);
                    }
                    else if (student.Course == 2)
                    {
                        subjectsList = subjectManagement.GetSubjects(5);
                    }
                    else
                    {
                        subjectsList = subjectManagement.GetSubjects(1);
                        ViewData["Information"] = "Вибіркові дисципліни для вас не опубліковані.";
                    }

                }
                else
                {
                    subjectsList = new List<Subject>()
                    .Concat(subjectManagement.GetSubjects(3))
                    .Concat(subjectManagement.GetSubjects(5))
                    .ToArray();
                }

                if (!String.IsNullOrEmpty(Search))
                {
                    subjectsList = subjectManagement.GetSubjectsByTitle(Search, subjectsList);
                }
                return View(subjectsList);
            }

            [Authorize]
            public ViewResult SubjectsSecondTerm(string Search)
            {
                var user = accountManagement.GetUser(User.Identity.Name);
                Subject[] subjectsList;
                ViewData["Information"] = "";
                if (user.Role == 3)
                {
                    var student = accountManagement.GetStudent(user.Id);
                    if (student.Course == 1)
                    {
                        subjectsList = subjectManagement.GetSubjects(4);
                    }
                    else if (student.Course == 2)
                    {
                        subjectsList = subjectManagement.GetSubjects(6);
                    }
                    else
                    {
                        subjectsList = subjectManagement.GetSubjects(1);
                        ViewData["Information"] = "Вибіркові дисципліни для вас не опубліковані.";
                    }

                }
                else
                {
                    subjectsList = new List<Subject>()
                    .Concat(subjectManagement.GetSubjects(4))
                    .Concat(subjectManagement.GetSubjects(6))
                    .ToArray();
                }

                if (!String.IsNullOrEmpty(Search))
                {
                    subjectsList = subjectManagement.GetSubjectsByTitle(Search, subjectsList);
                }
                return View(subjectsList);
            }

            [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
            public IActionResult Index()
            {
                return View();
            }

            

            [Authorize]
            public IActionResult SubjectInfo(int id)
            {
                try
                {
                    var subject = subjectManagement.GetSingleSubjectById(id);
                    ViewData["SubjectName"] = subject.Name;
                    ViewData["NumberOfStudents"] = subject.NumberOfStudents;
                    ViewData["MaxNumberOfStudents"] = subject.MaxNumberOfStudents;
                    ViewData["Description"] = subject.Description;
                    var lecturer = accountManagement.GetLecturer(subject.LecturerId);
                    ViewData["LecturerName"] = lecturer.LastName + " " + lecturer.FirstName[0] + ". " + lecturer.MiddleName[0] + ".";
                    ViewData["FacultyName"] = subject.Faculty;
                    
                }
                catch (System.Exception)
                {
                    return View("Error");
                }
                return View();
            }

            [Authorize(Roles = "3")]
            public void MakeSubjectChoice(int SubjId)
            {
                User user = accountManagement.GetUser(User.Identity.Name);
                subjectManagement.MakeSubjectChoice(user.Id, SubjId);
                logger.LogInformation("{@User} has made final choice - subject with id {Id} choosen", user, SubjId);
            }

            [Authorize(Roles = "1,2")]
            public async Task<IActionResult> AddNewSubject(Subject subject)
            {
                User user = accountManagement.GetUser(User.Identity.Name);
                if (user.Role == 2) { subject.LecturerId = user.Id; }
                if (!String.IsNullOrEmpty(subject.Name) && !String.IsNullOrEmpty(subject.Description) && !String.IsNullOrEmpty(subject.Faculty) && !String.IsNullOrEmpty(subject.Lecturer.User.LastName) && subject.Semester != 0)
                {
                    if (user.Role != 2) { subject.LecturerId = accountManagement.GetLecturerId(subject.Lecturer.User.LastName); }
                    subjectManagement.AddNewSubject(subject.Name, subject.Description, subject.Faculty, subject.LecturerId, subject.Semester);
                    return RedirectToAction("Profile", "Account");
                }
                return View();
            }

    }
}
