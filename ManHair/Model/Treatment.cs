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
       
           
        [Flags]public enum TreatmentType
        {
            HairCut = 1,
            HairDyeing = 2,
            Shaving = 4,
            EyebrowPlucking = 8
        }

        public Treatment(TreatmentType type, double price)
        {
            Type = type;
            Price = price;
        }
    }
}
