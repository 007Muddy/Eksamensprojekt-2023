using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model
{
    public class Treatment
    {
        public TreatmentType Type { get; set; }
        public double Price { get; set; }
       
           
        public enum TreatmentType
        {
            HairCut,
            HairDyeing,
            Shaving,
            EyebrowPlucking
        }

        public Treatment(TreatmentType type, double price)
        {
            Type = type;
            Price = price;
        }
    }
}
