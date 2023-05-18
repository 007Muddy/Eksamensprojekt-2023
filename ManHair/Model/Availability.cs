using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model
{
    public class Availability
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public Availability(DateOnly date, TimeOnly time)
        {
            Date = date;
            Time = time;
        }
    }
}
