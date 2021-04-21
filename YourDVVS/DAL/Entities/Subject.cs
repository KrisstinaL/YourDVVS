using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public partial class Subject
    {
        public Subject()
        {
            StudentsChoices = new HashSet<StudentsChoice>();
        }
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Faculty { get; set; }
        public int LecturerId { get; set; }
        public int? NumberOfStudents { get; set; }
        public int? MaxNumberOfStudents { get; set; }
        public int? Semester { get; set; }

        public virtual Lecturer Lecturer { get; set; }
        public virtual ICollection<StudentsChoice> StudentsChoices { get; set; }
    }
}
