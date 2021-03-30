using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.Entities;
using DAL.Context;
using BLL.Interfaces;

namespace BLL.Services
{
    public class SubjectManagement: ISubjectManagement
    {
        private AplicationContext database;
        public SubjectManagement(AplicationContext dm)
        {
            database = dm;
        }
        public void AddNewSubject(string name, string description, string faculty, int lecturer_id, int? semester)
        {
            database.Subject.Add(new Subject { Name = name, Description = description, Faculty = faculty, LecturerId = lecturer_id, NumberOfStudents = 0, MaxNumberOfStudents = 200, Semester = semester });
            database.SaveChanges();
        }

        public Subject GetSingleSubjectById(int id)
        {
            var subject = database.Subject.FirstOrDefault(s => s.SubjectId == id);
            return subject;
        }

        public Subject[] GetSubjects(int semester)
        {
            var subject = database.Subject.Where(s => s.Semester == semester).Select(s => s).ToArray();
            return subject;
        }
        public Subject[] GetSubjectsByTitle(string title, Subject[] subjects)
        {
            string[] titleParts = title.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var selectedSubjects = subjects.Where(r => titleParts.Any(t => r.Name.Contains(t))).Select(r => r).ToArray();
            return selectedSubjects;
        }

        public Subject[] GetStudentsChoice(int _UserId)
        {
            var subjectsList = database.Subject
                .Where(s => (database.StudentsChoice.Where(f => f.UserId == _UserId)
                .Any(f => f.SubjectId == s.SubjectId)))
                .OrderBy(s => s.Semester)
                .Select(s => s)
                .ToArray();
            return subjectsList;
        }

        public void MakeSubjectChoice(int _UserId, int _SubjId)
        {
            var subjectToAdd = database.Subject.FirstOrDefault(c => c.SubjectId == _SubjId);
            var choices = database.StudentsChoice.Where(c => c.UserId == _UserId).ToArray();
            foreach (StudentsChoice chs in choices)
            {
                var sbj = database.Subject.FirstOrDefault(c => c.SubjectId == chs.SubjectId);
                if (sbj.Semester == subjectToAdd.Semester)
                {
                    sbj.NumberOfStudents = sbj.NumberOfStudents - 1;
                    database.Remove(chs);
                }
            }
            subjectToAdd.NumberOfStudents = subjectToAdd.NumberOfStudents + 1;
            database.StudentsChoice.Add(new StudentsChoice { UserId = _UserId, SubjectId = _SubjId });
            database.SaveChanges();
        }
    }
}
