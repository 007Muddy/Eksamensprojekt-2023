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
    public partial class BookingWindow : Window
    {
        private double totalPrice;
        BookingViewModel bvm;
        CustomerHomeWindow chw;
        public BookingWindow()
        {
            InitializeComponent();
            AvailableDates.BlackoutDates.AddDatesInPast();
            bvm = new BookingViewModel();
            DataContext = bvm;
        }
        public BookingWindow(CustomerHomeWindow customerHome)
        {
            InitializeComponent();
            chw = customerHome;
            chw.Hide();
            AvailableDates.BlackoutDates.AddDatesInPast();
            bvm = new BookingViewModel();
            DataContext = bvm;
        }
        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            totalPrice = 0;
            int currentTypes = (int)bvm.SelectedTypes;
            foreach (TreatmentViewModel treatment in Type.SelectedItems)
            {
                totalPrice += treatment.Price;
                bvm.SelectedTypes |= treatment.Type;
                
            }
            bvm.TotalPrice = totalPrice;
            labelTotalPrice.Content = $"Total Pris: {totalPrice:C}";

            foreach (TreatmentViewModel treatment in e.RemovedItems.Cast<TreatmentViewModel>())
            {
                bvm.SelectedTypes &= ~treatment.Type;
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            bvm.RemoveAuthentication();
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ListView_AvailableTime(object sender, SelectionChangedEventArgs e)
        {
            foreach (AvailabilityViewModel availableTime in AvailableTimeSlots.SelectedItems)
            {
                bvm.SelectedTime = availableTime.Time.ToString("HH:mm:ss");
            }
        }

        private void AvailableDates_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                bvm.SelectedDate = (DateTime)e.AddedItems[0];
            }
            bvm.GetAvailability();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {   
            bvm.BookOrder();
            MessageBox.Show("Din bookning er bestilt :)");
            CustomerHomeWindow customerHome = new CustomerHomeWindow();
            customerHome.Show();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            chw.Show();
            this.Close();
        }

        
    }
}
