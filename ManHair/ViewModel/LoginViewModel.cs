using ManHair.Model;
using ManHair.Model.Persistence;
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
        private AdminRepo adminRepo = new AdminRepo();
        private CustomerRepo customerRepo = new CustomerRepo();

        public LoginViewModel() { }

        public bool AccessGranted(string email, string password)
        {

            bool access = false;

            Customer customer = new Customer(email, password);
            customerRepo.AddAuthentication(email, password);
            if (customerRepo.AuthenticateUser(customer) == true)
            {
                List<Customer> customers = customerRepo.GetCustomers();
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
            return access;
        }

        public bool AdminAccess(string userName, string password)
        {
            bool access = false;
            if (userName != null && password != null)
            {
                Admin admin = new Admin(userName, password);
                if (adminRepo.AdminAuthentication(admin) == true)
                {
                    access = true;
                    LoginMessage = $"Login was successful: Welcome Admin";
                }
                else
                {
                    LoginMessage = $"Login failed please try again";
                }
            }
            return access;
        }
    }
}
