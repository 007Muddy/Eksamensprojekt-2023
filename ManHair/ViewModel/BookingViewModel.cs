using ManHair.Model;
using ManHair.Model.Persistence;
using ManHair.ViewModel.Repositories;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static ManHair.Model.Treatment;

namespace ManHair.ViewModel
{
    public class BookingViewModel
    {
        private AvailabilityRepo availabilityRepo = new AvailabilityRepo();
        private TreatmentRepo treatmentRepo = new TreatmentRepo();
        private OrderRepo orderRepo = new OrderRepo();
        private CustomerRepo customerRepo = new CustomerRepo();
        public ObservableCollection<AvailabilityViewModel> AvailabilityVM { get; set; } = new();
        public ObservableCollection<TreatmentViewModel> TreatmentVM { get; set; } = new();
  

        public string SelectedTime { get; set; }
        private DateTime selectedDate;
        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                selectedDate = value;
                SelectedDateOnly = DateOnly.FromDateTime(selectedDate);
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

        public BookingViewModel()
        {
            foreach (Treatment item in treatmentRepo.GetTreatments())
            {
                TreatmentViewModel treatmentViewModel = new(item);
                TreatmentVM.Add(treatmentViewModel);
            }         
        }
        public void BookOrder()
        {
            orderRepo.AddOrder(customerRepo.GetID(customerRepo.getEmail()),
                SelectedDate.ToString("yyyy-MM-dd"),
                SelectedTime,
                TotalPrice,
                (int)SelectedTypes);
            availabilityRepo.RemoveAvailability(SelectedDateOnly, TimeOnly.Parse(SelectedTime));
        }

        public void GetAvailability()
        {
            AvailabilityVM.Clear();
            foreach (Availability item in availabilityRepo.GetAvailabilities(SelectedDateOnly))
            {
                AvailabilityViewModel availabilityViewModel = new(item);
                AvailabilityVM.Add(availabilityViewModel);

            }
        }
        public void RemoveAuthentication()
        {
            customerRepo.RemoveAuthentication();
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
