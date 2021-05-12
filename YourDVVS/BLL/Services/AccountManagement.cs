namespace BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BLL.Interfaces;
    using DAL.Context;
    using DAL.Entities;

    public class AccountManagement : IAccountManagement
    {
        public class UserNotFoundException : Exception
        {
            public UserNotFoundException() { }

            public UserNotFoundException(string message)
                : base(message) { }

            public UserNotFoundException(string message, Exception inner)
                : base(message, inner) { }

            protected UserNotFoundException(
              System.Runtime.Serialization.SerializationInfo info,
              System.Runtime.Serialization.StreamingContext context)
                : base(info, context) { }
        }

        private readonly AplicationContext database;

        public AccountManagement(AplicationContext dm)
        {
            database = dm;
        }

        public void AddNewUser(string lastName, string firstName, string middleName, int role, string faculty, string login, string password)
        {
            if (role == 2 || role == 1)
            {
                database.User.Add(new User { LastName = lastName, FirstName = firstName, MiddleName = middleName, Role = role, Faculty = faculty, Login = login, Password = password, Lecturer = new Lecturer { } });
            }
            else {
                database.User.Add(new User { LastName = lastName, FirstName = firstName, MiddleName = middleName, Role = role, Faculty = faculty, Login = login, Password = password, Student = new Student { } });
            }
            database.SaveChanges();
        }

        public string GetRoleName(int roleValue)
        {
            switch (roleValue)
            {
                case 1:
                    return "Адмін";
                case 2:
                    return "Викладач";
                default:
                    return "Студент";
            }
        }

        public User GetUser(string username)
        {
            var user = database.User.FirstOrDefault(u => u.Login == username);
            if (user == null)
            {
                throw new UserNotFoundException("Користувач з таким логіном не знайдений");
            }

            return user;
        }

        public User GetLecturer(int id)
        {
            var user = database.User.FirstOrDefault(u => u.UserId == id);
            return user;
        }

        public string[] GetLecturers()
        {
            var user = database.User.ToArray().Where(s => s.Role == 2).Select(s => s).ToArray();
            string[] lecturersList = new string[user.Length];
            for (int i = 0; i < user.Length; i++)
            {
                lecturersList[i] = user[i].LastName + " " + user[i].FirstName + " " + user[i].MiddleName;
            }

            return lecturersList;
        }

        public int GetLecturerId(string name)
        {
            var lecturersList = database.User.ToArray().Where(s => s.Role == 2).Select(s => s).ToArray();
            string[] titleParts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int lId = lecturersList.Where(r => name.Any(t => (r.LastName + r.FirstName + r.MiddleName).Contains(t))).Select(r => r).FirstOrDefault().UserId;
            return lId;
        }

        public Student GetStudent(int id)
        {
            var user = database.Student.FirstOrDefault(u => u.UserId == id);
            return user;
        }

        public bool Verify(string username, string password)
        {
            return GetUser(username).Password == password;
        }
    }
}
