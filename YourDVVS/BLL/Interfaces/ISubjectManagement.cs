// <copyright file="ISubjectManagement.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BLL.Interfaces
{
    using DAL.Entities;

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
