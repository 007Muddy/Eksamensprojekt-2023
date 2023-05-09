using ManHair.Model;
using ManHair.Model.Persistence;
using ManHair.ViewModel.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHair.ViewModel
{
    public class MainViewModel
    {
        private AvailabilityRepo availabilityRepo = new AvailabilityRepo();
        private TreatmentRepo treatmentRepo = new TreatmentRepo();
        private OrdersRepo ordersRepo = new OrdersRepo(); 
        
        public ObservableCollection<AvailabilityViewModel> AvailableVM { get; set; } = new();
        public ObservableCollection<TreatmentViewModel> TreatmentVM { get; set; }= new ();
        public ObservableCollection<OrdersViewModel> OrdersVM { get; set; } = new();

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                SelectedDateOnly = DateOnly.FromDateTime(value);
                OnPropertyChanged(nameof(SelectedDate));
                OnPropertyChanged(nameof(SelectedDateOnly));
            }
        }
        public DateOnly SelectedDateOnly { get; set; }
        //private DateOnly _selectedDateOnly;
        //public DateOnly SelectedDateOnly
        //{
        //    get { return _selectedDateOnly; }
        //    set
        //    {
        //        _selectedDateOnly = value;
        //        OnPropertyChanged(nameof(SelectedDateOnly));
        //    }
        //}
        public MainViewModel()
        {

            foreach (Availability item in availabilityRepo.getAvailability(SelectedDateOnly))
            {
                AvailabilityViewModel availabilityViewModel = new(item);
                AvailableVM.Add(availabilityViewModel);
            }

            foreach (Orders item1 in ordersRepo.GetOrders())
            {
                OrdersViewModel ordersViewModel = new(item1);
                OrdersVM.Add(ordersViewModel);

            }

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
