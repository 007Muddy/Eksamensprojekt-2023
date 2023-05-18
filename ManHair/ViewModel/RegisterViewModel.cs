using ManHair.ViewModel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class RegisterViewModel
    {
        public string LoginMessage { get; set; }
        private CustomerRepo customerRepo = new CustomerRepo();

        public RegisterViewModel() { }

        public bool CreateNewCustomer(string name, int phone, string email, string password)
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
