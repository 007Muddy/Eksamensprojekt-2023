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
    /// Interaction logic for RegisterButton.xaml

    public partial class Register : Window
    {
        RegisterViewModel avm { get; set; }

        public Register()
        {
            InitializeComponent();
            avm = new RegisterViewModel();
            DataContext = avm;
        }

        private void RegisterButton(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            if (txtEmail != null && txtTlf != null && txtEmail != null && txtAdgangskode != null && txtBekræftAdgangskode != null)
            {
                if (txtAdgangskode.Password == txtBekræftAdgangskode.Password)
                {
                    if (avm.CreateNewCustomer(txtEmail.Text, Int32.Parse(txtTlf.Text), txtEmail.Text, txtAdgangskode.Password))
                    {
                        MessageBox.Show("You have successfully registered");
                        main.Show();
                        this.Hide();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields");
            }
        }



        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
