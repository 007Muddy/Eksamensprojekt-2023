using ManHair.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ManHair.Model.Treatment;

namespace ManHair.ViewModel
{
    public class TreatmentViewModel 
    {

        public TreatmentType Type { get; set; }
        public double Price { get; set;}

        public TreatmentViewModel(Treatment treatment)
        {
            this.Type = treatment.Types;
            this.Price = treatment.Price;
        }
    }
}
