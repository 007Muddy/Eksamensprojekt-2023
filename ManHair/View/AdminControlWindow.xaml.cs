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
    /// Interaction logic for AdminControl.xaml
    /// </summary>
    public partial class AdminControlWindow : Window
    {
        public AdminControlViewModel acvm { get; set; }

        public AdminControlWindow()
        {
            InitializeComponent();
            acvm = new AdminControlViewModel();
            DataContext = acvm;
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            acvm.RemoveAuthentication();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            acvm.RemoveAuthentication();
            LoginWindow login = new LoginWindow();
            login.Show();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            acvm.CancelOrder();
            acvm.UpdateOrdersVM();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

