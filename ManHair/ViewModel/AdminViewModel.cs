using ManHair.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class AdminViewModel
    {
        public string Username { get; set; }
        public string  Password { get; set; }

        public AdminViewModel(Admin admin) 
        
        {
        Username=admin.UserName; Password=admin.Password;
        
        
        
        }
    }
}
