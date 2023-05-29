using ManHair.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for CustomerHome.xaml
    /// </summary>
    public partial class CustomerHomeWindow : Window
    {
        CustomerHomeViewModel chvm;
        public CustomerHomeWindow()
        {
            InitializeComponent();
            chvm = new CustomerHomeViewModel();
            DataContext = chvm;
        }
        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            chvm.RemoveAuthentication();
        }

        private void GoToOrder_Click(object sender, RoutedEventArgs e)
        {
            BookingWindow booking = new BookingWindow(this);
            booking.Show();
            this.Hide();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            chvm.RemoveAuthentication();
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            chvm.CancelOrder();
            chvm.UpdateOrdersVM();
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
