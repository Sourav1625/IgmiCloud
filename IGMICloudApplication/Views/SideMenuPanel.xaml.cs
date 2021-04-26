using IGMICloudApplication.Models;
using IGMICloudApplication.ViewModels;
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
            /*Dashboard dashboard = new Dashboard() { DataContext = new MainViewModel() };
            dashboard.Show();

            App.Current.MainWindow.Close();
            App.Current.MainWindow = dashboard;*/
        }

        private void mnuWorkspaces_Click(object sender, RoutedEventArgs e)
        {
            Workspace dashboard = new Workspace() { DataContext = new MainViewModel() };
            dashboard.Show();

            App.Current.MainWindow.Close();
            App.Current.MainWindow = dashboard;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //convert datasource to datatable and pass it to the method that converts datatable to Excel  
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked on Menu Item 1...", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LeftMenu_Loaded(object sender, RoutedEventArgs e)
        {
            lblName.Content = UserProfile.userName;

            string[] multiArray = UserProfile.userName.Split(new Char[] { ' ', ',', '.', '-', '\n', '\t' });
            string uname = UserProfile.userName.Replace(",", " ");
            string initialLetter = "";
            int count = 0;
            if (multiArray.Length > 1)
            {
                while (count < 2)
                {
                    initialLetter = initialLetter + multiArray[count].Substring(0, 1);
                    count++;
                }
            }
            initial.Text = initialLetter;
        }
    }
}
