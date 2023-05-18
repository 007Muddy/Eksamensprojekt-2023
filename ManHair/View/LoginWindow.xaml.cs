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
        LoginViewModel lvm;
        MainWindow mv;
        public Login()
        {
            InitializeComponent();           
            lvm = new LoginViewModel();
            DataContext = lvm;
        }
        public Login(MainWindow main)
        {
            InitializeComponent();
            mv = main;
            mv.Hide();
            lvm = new LoginViewModel();
            DataContext = lvm;
        }
        private void ButtonLogin(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text != null && txtPassword.Password != null)
            {
                if (lvm.AccessGranted(txtEmail.Text, txtPassword.Password))
                {
                    MessageBox.Show(lvm.LoginMessage);

                    CustomerHome customerWindow = new CustomerHome();
                    customerWindow.Show();
                    this.Close();

                }
                else if (lvm.AdminAccess(txtEmail.Text, txtPassword.Password))
                {
                    MessageBox.Show(lvm.LoginMessage);
                    AdminControl control = new AdminControl();
                    control.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(lvm.LoginMessage);
                }
            }
            else
            {
                MessageBox.Show(lvm.LoginMessage);
            }
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
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
            mv.Show();
            this.Close();
            
        }
    }
}
