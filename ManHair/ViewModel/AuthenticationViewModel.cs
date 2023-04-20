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
        private CostumerRepo costumerRepo = new CostumerRepo(); 
        public AuthenticationViewModel(Authentication auth)
        {
            this.User = auth.User;
            this.Password = auth.Password;
        }

        public AuthenticationViewModel()
        {
        }

        public bool AccessGranted(string email, string password)
        {
            bool access= false;

            try
            {
                if (email != null && password != null)
                {
                    Customer customer = new Customer(email, password);
                    

                    if (authenticationRepo.AuthenticateUser(customer) == true)
                    {
                        foreach (Customer customers in costumerRepo.getCostumers())
                        {
                            string name = customers.Name;
                            LoginMessage = $"Login was successfull: Welcome {name}";
                        }
                        
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

        public bool CreateNewCustomer(string name, int phone ,string email, string password)
        {
            bool Registersucces = false;

            try
            {
                if (name != null && email != null && password != null)
                {
                    costumerRepo.Add(name, phone, email, password);
                    Registersucces = true;
                    LoginMessage = $"You have successfully registered:{name}";
                }
                else
                {
                    LoginMessage = $"Registration failed, please try again bro:{name}";
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return Registersucces;
            
        }
    }
}
