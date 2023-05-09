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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        AuthenticationViewModel avm;
        MainViewModel mvm;
        public Login()
        {
            InitializeComponent();
            mvm = new MainViewModel();
            avm = new AuthenticationViewModel();
            DataContext = avm;
        }

        private void ButtonLogin(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text != null && txtPassword.Password != null)
            {
                if (avm.AccessGranted(txtEmail.Text, txtPassword.Password))
                {
                    MessageBox.Show(avm.LoginMessage);
                    mvm.AVM.Email = txtEmail.Text;
                    mvm.Email = txtEmail.Text;
                    Booking bookingWindow = new Booking();  
                    bookingWindow.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show(avm.LoginMessage);
                }
            }
            else
            {
                MessageBox.Show(avm.LoginMessage);
            }
        }

        private void Button_Forside(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
