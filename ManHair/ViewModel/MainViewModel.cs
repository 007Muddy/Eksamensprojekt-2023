using ManHair.Model;
using ManHair.Model.Persistence;
using ManHair.ViewModel.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ManHair.Model.Treatment;

namespace ManHair.ViewModel
{
    public class MainViewModel
    {
        private AvailabilityRepo availabilityRepo = new AvailabilityRepo();
        private TreatmentRepo treatmentRepo = new TreatmentRepo();
        private OrdersRepo orderRepo = new OrdersRepo();
        public ObservableCollection<AvailabilityViewModel> AvailableVM { get; set; } = new();
        public ObservableCollection<TreatmentViewModel> TreatmentVM { get; set; } = new();
        
        public AuthenticationViewModel AVM = new AuthenticationViewModel();
        public string Email { get; set; }   
       

        public string SelectedTime { get; set; }
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                SelectedDateOnly = DateOnly.FromDateTime(_selectedDate);
                OnPropertyChanged(nameof(SelectedDate));
                OnPropertyChanged(nameof(SelectedDateOnly));
            }
        }
        public DateOnly SelectedDateOnly { get; set; }
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

        private TreatmentType selectedTypes;
        public TreatmentType SelectedTypes
        {
            get => selectedTypes;
            set
            {

                selectedTypes = value;
                OnPropertyChanged("SelectedType");
            }
        }

        public MainViewModel()
        {

            GetAvailability();
            foreach (Treatment item in treatmentRepo.getTreatments())
            {
                TreatmentViewModel treatmentViewModel = new(item);
                TreatmentVM.Add(treatmentViewModel);
            }
            AVM.Email = "3w4";
            
        }



        public void BookOrder()
        {
              
                    orderRepo.Add(AVM.getID(),SelectedDate.ToString("yyyy-MM-dd"), SelectedTime, TotalPrice, (int)SelectedTypes);
                
            
            
        }

        public void GetAvailability()
        {
            AvailableVM.Clear();
            foreach (Availability item in availabilityRepo.getAvailability(SelectedDateOnly))
            {
                AvailabilityViewModel availabilityViewModel = new(item);
                AvailableVM.Add(availabilityViewModel);
                
            }
        }
        public void CalculateTotalPrice(Treatment treatment)
        {
            TotalPrice += treatment.Price;
        }



        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
