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
    public class TreatmentViewModel : INotifyPropertyChanged
    {
        private double price;
        public TreatmentType Type { get; set; }
        public double Price
        { get { return price; } set { }
            
        }
        private double totalPrice;
        public double TotalPrice
        {
            get => totalPrice; 
            set
            {
                totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }
        private TreatmentType selectedType;
        public TreatmentType SelectedType
        {
            get => selectedType;
            set
            {
                selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }
        public List<Treatment> types { get; set; }



        public List<Treatment> ReturnAllTypes()
        {
            return types;
        }
        public TreatmentViewModel()
        {
            types = new List<Treatment>
           {
               new Treatment(TreatmentType.HairCut, 130),
               new Treatment(TreatmentType.HairDyeing, 100),
               new Treatment(TreatmentType.Shaving, 75),
               new Treatment(TreatmentType.EyebrowPlucking, 50)
           };
            
        }

        
       public void CalculateTotalPrice(Treatment treatment)
        {
            TotalPrice += treatment.Price;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
