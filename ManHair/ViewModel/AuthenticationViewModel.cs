using ManHair.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class AuthenticationViewModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        public AuthenticationViewModel(Authentication auth)
        {
            this.User = auth.User;
            this.Password = auth.Password;
        }
    }
}
