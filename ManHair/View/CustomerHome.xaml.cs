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
        CustomerHomeViewModel CHM;
        public CustomerHome()
        {
            InitializeComponent();
            CHM = new CustomerHomeViewModel();
            DataContext = CHM;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            Booking booking = new Booking();
            booking.Show();
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            CHM.RemoveAuthentication();
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
