using ManHair.Model;
using ManHair.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManHair.View
{
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : Window
        
    {
        public double totalPrice;
        MainViewModel mvm;
        public Booking()
        {
            InitializeComponent();
            AvailableDates.BlackoutDates.AddDatesInPast();
            mvm = new MainViewModel();
            DataContext = mvm;

        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
            mvm.AVM.RemoveAuthentication();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            totalPrice = 0;
            int currentTypes = (int)mvm.SelectedTypes;
            foreach (TreatmentViewModel treatment in Type.SelectedItems)
            {
                totalPrice += treatment.Price;
                mvm.SelectedTypes |= treatment.Type;
                
            }
            mvm.TotalPrice = totalPrice;
            labelTotalPrice.Content = $"Total Pris: {totalPrice:C}";

            foreach (TreatmentViewModel treatment in e.RemovedItems.Cast<TreatmentViewModel>())
            {
                mvm.SelectedTypes &= ~treatment.Type;
            }
            

        }

        private void ListView_AvialableTime(object sender, SelectionChangedEventArgs e)
        {
            foreach (AvailabilityViewModel availableTime in AvailableTimeSlots.SelectedItems)
            {
                mvm.SelectedTime = availableTime.Time.ToString("HH:mm:ss");

            }
        }

        private void Test_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void AvailableDates_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                mvm.SelectedDate = (DateTime)e.AddedItems[0];
            }
            mvm.GetAvailability();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {   
            mvm.BookOrder();
            MessageBox.Show("Din bookning er bestilt :)");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            CustomerHome customerHome = new CustomerHome();
            customerHome.Show();
            this.Hide();
        }
    }
}
