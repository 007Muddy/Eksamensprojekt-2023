using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Customer(int id, string name, int phone, string email, string password)
        { 
            Name = name;
            Phone = phone;
            Email = email;
            Password = password;
            ID= id;
        }
        public Customer(string email, string password) 
        {
            Email = email;
            Password = password;
        }
    }
}
