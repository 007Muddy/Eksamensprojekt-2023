using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model
{
    public class Authentication
    {
        public string User { get; set; }
        public string Password { get; set; }

        public Authentication(string user, string password)
        {
            User = user;
            Password = password;
        }
    }
}
