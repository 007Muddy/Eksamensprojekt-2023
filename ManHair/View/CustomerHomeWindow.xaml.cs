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
    public partial class CustomerHome : Window
    {
        CustomerHomeViewModel chvm;
        public CustomerHome()
        {
            InitializeComponent();
            chvm = new CustomerHomeViewModel();
            DataContext = chvm;
        }
        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            chvm.RemoveAuthentication();
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = new Booking(this);
            booking.Show();
            this.Hide();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            chvm.RemoveAuthentication();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            chvm.CancelOrder();
            chvm.UpdateOrdersVM();
        }
    }
}
