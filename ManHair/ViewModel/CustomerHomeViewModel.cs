using ManHair.ViewModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class CustomerHomeViewModel
    {
        private AuthenticationRepo authenticationRepo = new AuthenticationRepo();
        public CustomerHomeViewModel() 
        { 
        
        }

        public void RemoveAuthentication()
        {
            authenticationRepo.Remove();
        }
    }
}
