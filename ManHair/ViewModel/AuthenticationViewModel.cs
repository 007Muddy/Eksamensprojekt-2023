using ManHair.Model;
using ManHair.Model.Persistence;
using ManHair.ViewModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ManHair.ViewModel
{
    public class AuthenticationViewModel
    {
        public string User { get; set; }
        public string Password { get; set; }
        public string LoginMessage { get; set; }
        private AuthenticationRepo authenticationRepo = new AuthenticationRepo();
        private CostumerRepo customerRepo = new CostumerRepo();
        public AdminRepo adminRepo { get; set; }
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
                        List<Customer> customers = customerRepo.getCostumers();
                        List<Customer> filteredCustomers = customers.Where(customer => customer.Email == email).ToList();

                        foreach (Customer customerName in filteredCustomers)
                        {
                            string name = customerName.Name;
                            LoginMessage = $"Login was successful: Welcome {name}";
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

        public bool AdminAccess(string userName, String password)
        {
            bool access= false;
            try
            {
                if (userName != null && password != null)
                {
                    Admin admin = new Admin(userName, password);

                    if (authenticationRepo.AdminAuthentication(admin)==true)
                    {
                        access= true;
                        LoginMessage = $"Login was successful: Welcome Admin";
                    }
                    else
                    {
                        LoginMessage = $"Login failed please try again";
                    }
                }
            }
            catch (Exception)
            {

                throw;
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
                    customerRepo.Add(name, phone, email, password);
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
