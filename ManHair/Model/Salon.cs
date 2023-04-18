using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model
{
    public class Salon
    {
        public int SalonID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public Salon() { }
    }
}
