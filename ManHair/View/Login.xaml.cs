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
                    CustomerHome customerWindow = new CustomerHome();  
                    customerWindow.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show(avm.LoginMessage);
                    avm.RemoveAuthentication();
                }
            }
            else
            {
                MessageBox.Show(avm.LoginMessage);
            }
        }

        private void Button_Forside(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        }
    }
}
