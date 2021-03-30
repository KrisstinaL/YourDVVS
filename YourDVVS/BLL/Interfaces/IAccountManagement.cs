using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;

namespace BLL.Interfaces
{
    public interface IAccountManagement
    {
        void AddNewUser(string lastName, string firstName, string middleName, int role,string faculty, string login, string password);
        string GetRoleName(int roleValue);
        User GetLecturer(int id);
        public string[] GetLecturers();
        public int GetLecturerId(string name);
        Student GetStudent(int id);
        User GetUser(string username);
        bool Verify(string username, string password);
    }
}
