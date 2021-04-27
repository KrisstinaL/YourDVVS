using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public partial class Student
    {
        public Student()
        {
            StudentsChoices = new HashSet<StudentsChoice>();
        }
        public int UserId { get; set; }
        public int? Course { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<StudentsChoice> StudentsChoices { get; set; }
    }
}
