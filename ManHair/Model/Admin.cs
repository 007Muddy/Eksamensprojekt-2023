using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model
{
    public class Admin
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Admin(string username, string password)
        {
            UserName = username;
            Password = password;


        }
    }
}
