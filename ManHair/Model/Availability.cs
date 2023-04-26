using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model
{
    public class Availability
    {
        public DateOnly Date;
        public TimeOnly Time;
       public Availability(DateOnly date, TimeOnly time)
        {
            Date = date;
            Time = time;
        }
    }
}
