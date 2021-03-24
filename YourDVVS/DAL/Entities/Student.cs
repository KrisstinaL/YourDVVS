using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public partial class Student
    {
        public int StudentId { get; set; }
        public int? Course { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<StudentsChoice> StudentsChoices { get; set; }
    }
}
