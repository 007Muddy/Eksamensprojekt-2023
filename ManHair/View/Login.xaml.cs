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
        public Login()
        {
            InitializeComponent();
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
          MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void ButtonBack(object sender, RoutedEventArgs e)
        {
MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Hide();
        }
    }
}
