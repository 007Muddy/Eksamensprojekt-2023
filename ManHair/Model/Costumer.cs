using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model
{
    public class Costumer
    {
        public int CostumerID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Costumer(int costumerID, string name, string phone, string email, string password)
        {
            CostumerID = costumerID;
            Name = name;
            Phone = phone;
            Email = email;
            Password = password;
        }
    }
}
