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
        private ObservableCollection<TreatmentType> type { get; set; }
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
        new Treatment(new ObservableCollection<TreatmentType> { TreatmentType.HairCut }, 130),
        new Treatment(new ObservableCollection<TreatmentType> { TreatmentType.HairDyeing }, 100),
        new Treatment(new ObservableCollection<TreatmentType> { TreatmentType.Shaving }, 75),
        new Treatment(new ObservableCollection<TreatmentType> { TreatmentType.EyebrowPlucking }, 50)
    };
            return types;
        }
    }
}
