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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGMICloudApplication.Views
{
    /// <summary>
    /// Interaction logic for SideMenuPanel.xaml
    /// </summary>
    public partial class SideMenuPanel : UserControl
    {
        public SideMenuPanel()
        {
            InitializeComponent();
        }

        private void mnuDashboard_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Dashboard");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //convert datasource to datatable and pass it to the method that converts datatable to Excel  
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked on Menu Item 1...", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
