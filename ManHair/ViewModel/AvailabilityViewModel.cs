using ManHair.Model;
using ManHair.Model.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class AvailabilityViewModel
    {
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public AvailabilityViewModel(Availability availability)
        {
            this.Date = availability.Date;
            this.Time = availability.Time;
        }

       
    }
}
