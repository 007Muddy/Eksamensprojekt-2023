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
    public partial class AdminControl : Window
    {
        public AdminControlViewModel ACM { get; set; }

        public AdminControl()
        {
            InitializeComponent();
            ACM = new AdminControlViewModel();
            DataContext = ACM;
        }


        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

