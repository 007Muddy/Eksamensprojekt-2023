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
        public string User { get; set; }
        public string Password { get; set; }
        public string LoginMessage { get; set; }
        private AuthenticationRepo authenticationRepo = new AuthenticationRepo();
        public AuthenticationViewModel(Authentication auth)
        {
            this.User = auth.User;
            this.Password = auth.Password;
        }

        public bool AccessGranted(string email, string password)
        {
            bool access= false;

            try
            {
                if (email != null && password != null)
                {
                    Customer customer = new Customer("Kenneth", 44224533, email, password);

                    if (authenticationRepo.AuthenticateUser(customer) == true)
                    {
                        LoginMessage = $"Login was successfull: Welcome {customer.Name}";
                        access = true;
                    }
                    else
                    {
                        LoginMessage = $"Login failed please try again";
                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception("Der opstod en fejl under login" + e);
            }
           
            return access;
        }
    }
}
