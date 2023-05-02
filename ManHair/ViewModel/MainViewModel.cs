﻿using ManHair.Model;
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
        public ObservableCollection<AvailabilityViewModel> AvailableVM { get; set; } = new();
        public ObservableCollection<TreatmentViewModel> TreatmentVM { get; set; } = new ();
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
        
        public MainViewModel()
        {

            GetAvailability();
            foreach (Treatment item in treatmentRepo.getTreatments())
            {
                TreatmentViewModel treatmentViewModel = new(item);
                TreatmentVM.Add(treatmentViewModel);
            }


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

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}
