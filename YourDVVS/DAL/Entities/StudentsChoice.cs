using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public partial class StudentsChoice
    {
        public int UserId { get; set; }
        public int SubjectId { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual Student User { get; set; }
    }
}
