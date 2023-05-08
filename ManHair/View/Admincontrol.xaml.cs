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

namespace ManHair
{
    /// <summary>
    /// Interaction logic for Admincontrol.xaml
    /// </summary>
    public partial class Admincontrol : Window
    {
        MainViewModel mvm;
        public Admincontrol()
        {
            InitializeComponent();
            mvm = new MainViewModel();
            DataContext= mvm;
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   

        }
    }
}
