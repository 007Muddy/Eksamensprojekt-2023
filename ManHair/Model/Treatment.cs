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
        public enum TreatmentType
        {
            HairCut = 130,
            HairDyeing = 100,
            Shaving= 75,
            EyebrowPlucking= 50
        }

        public Treatment(TreatmentType type)
        {
            Type = type;
        }
    }
}
