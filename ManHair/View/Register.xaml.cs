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
        public RegisterViewModel rvm { get; set; }
       
        public Register()
        {
            InitializeComponent();
            rvm = new RegisterViewModel();
            DataContext = rvm;
        }

        private void RegisterButton(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            if (txtName != null && txtTlf != null && txtEmail != null && txtAdgangskode != null && txtBekræftAdgangskode != null)
            {
                if (txtAdgangskode.Password == txtBekræftAdgangskode.Password)
                {
                    if (rvm.CreateNewCustomer(txtName.Text,Int32.Parse(txtTlf.Text),txtEmail.Text,txtAdgangskode.Password))
                    {
                        MessageBox.Show("You have successfully registered");
                        main.Show();
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill all the fields");
            }
        }
    }
}
