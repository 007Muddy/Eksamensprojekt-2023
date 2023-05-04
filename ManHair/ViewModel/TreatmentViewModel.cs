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
        
        public ObservableCollection<TreatmentType> Type { get; set; }
        public double Price
        { get; set;
            
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
        private ObservableCollection<TreatmentType> selectedTypes;
        public ObservableCollection<TreatmentType> SelectedTypes
        {
            get => selectedTypes;
            set
            {
                selectedTypes = value;
                OnPropertyChanged("SelectedType");
            }
        }
        public TreatmentViewModel(Treatment treatment)
        {
            this.Type = treatment.Type;
            this.Price = treatment.Price;
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
