using ManHair.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class TreatmentViewModel
    {
        public List<Treatment.TreatmentType> types { get; set; }

        public List<Treatment.TreatmentType> ReturnAllTypes()
        {
            return types;
        }
    }
}
