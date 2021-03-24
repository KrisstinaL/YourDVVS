using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourDVVS.Models
{
    public class RoleModel
    {
        static Dictionary<string, string> map = new Dictionary<string, string>() {
            {"1", "Адмін"},
            {"2", "Викладач"},
            {"3", "Студент"}
        };
    }
}
