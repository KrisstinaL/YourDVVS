using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public partial class Lecturer
    {        public Lecturer()
        {
            Subjects = new HashSet<Subject>();
        }
        public int UserId { get; set; }
        public int Id { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
