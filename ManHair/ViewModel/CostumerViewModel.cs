using ManHair.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class CostumerViewModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public CostumerViewModel(Customer costumer)
        {
            Name= costumer.Name;
            Phone= costumer.Phone;
            Email= costumer.Email;
            Password= costumer.Password;    
        }
    }
}
