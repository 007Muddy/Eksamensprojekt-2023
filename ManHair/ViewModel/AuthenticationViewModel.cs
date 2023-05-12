using ManHair.Model;
using ManHair.ViewModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class AuthenticationViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
       
        public AuthenticationViewModel(Authentication auth)
        {
            this.Email = auth.Email;
            this.Password = auth.Password;
            
        }

    }
}
