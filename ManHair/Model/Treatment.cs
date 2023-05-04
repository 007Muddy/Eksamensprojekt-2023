using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.Model
{
    public class Treatment
    {
        public ObservableCollection<TreatmentType> Type { get; set; }
        public double Price { get; set; }
       
           
        public enum TreatmentType
        {
            HairCut = 1,
            HairDyeing = 2,
            Shaving =4,
            EyebrowPlucking = 8 
        }

        public Treatment(ObservableCollection<TreatmentType> type, double price)
        {
            Type = type;
            Price = price;
        }
    }
}
