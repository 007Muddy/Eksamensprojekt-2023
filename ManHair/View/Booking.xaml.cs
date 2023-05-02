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

        protected void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            totalPrice = 0;
            foreach(TreatmentViewModel treatment in Type.SelectedItems)
            {
                totalPrice += treatment.Price;
            }
            labelTotalPrice.Content = $"Total Pris: {totalPrice:C}";
        }

        private void ListView_AvialableDates(object sender, SelectionChangedEventArgs e)
        {
            
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

        }
    }
}
