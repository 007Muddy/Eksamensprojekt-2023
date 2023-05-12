using ManHair.Model;
using ManHair.ViewModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class LoginViewModel
    {
        public string LoginMessage { get; set; }
        private AuthenticationRepo authenticationRepo = new AuthenticationRepo();
        private CustomerRepo customerRepo = new CustomerRepo();

        public LoginViewModel() { }

        public bool AccessGranted(string email, string password)
        {

            bool access = false;

            try
            {
                if (email != null && password != null)
                {

                    Customer customer = new Customer(email, password);
                    authenticationRepo.Add(email, password);
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
            bool access = false;
            try
            {
                if (userName != null && password != null)
                {
                    Admin admin = new Admin(userName, password);

                    if (authenticationRepo.AdminAuthentication(admin) == true)
                    {
                        access = true;
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
    }
}
