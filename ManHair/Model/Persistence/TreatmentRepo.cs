using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ManHair.Model.Treatment;

namespace ManHair.Model.Persistence
{
    public class TreatmentRepo
    {
        private TreatmentType type { get; set; }
        private double price { get; set; }
        public List<Treatment> types { get; set; }
      


        public List<Treatment> ReturnAllTypes()
        {
            return types;
        }

        public List<Treatment> getTreatments()
        {
            types = new List<Treatment>
    {
        new Treatment(TreatmentType.HairCut, 130),
        new Treatment(TreatmentType.HairDyeing, 100),
        new Treatment(TreatmentType.Shaving, 75),
        new Treatment(TreatmentType.EyebrowPlucking, 50)
    };
            return types;
        }
    }
}
