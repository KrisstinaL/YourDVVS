using System;
using System.Collections.Generic;
using System.Text;
using DAL.Entities;
using DAL;

namespace BLL.Interfaces
{
    public interface ISubjectManagement
    {
        void AddNewSubject(string name, string description, string faculty, int lecturer_id, int? semester);
        Subject GetSingleSubjectById(int id);
        Subject[] GetSubjects(int semester);
        Subject[] GetSubjectsByTitle(string title, Subject[] subjects);
        Subject[] GetStudentsChoice(int _UserId);

        void MakeSubjectChoice(int _UserId, int _SubjId);
    }
}
