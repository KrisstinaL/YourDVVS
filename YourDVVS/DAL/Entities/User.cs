using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public partial class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Role { get; set; }
        public string Faculty { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public virtual Lecturer Lecturer { get; set; }
        public virtual Student Student { get; set; }
    }
}