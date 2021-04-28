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
       
      
        private void LeftMenu_Loaded(object sender, RoutedEventArgs e)
        {        

            //string[] multiArray = UserProfile.userName.Split(new Char[] { ' ', ',', '.', '-', '\n', '\t' });
            //string uname = UserProfile.userName.Replace(",", " ");
            //string initialLetter = "";
            //int count = 0;
            //if (multiArray.Length > 1)
            //{
            //    while (count < 2)
            //    {
            //        initialLetter = initialLetter + multiArray[count].Substring(0, 1);
            //        count++;
            //    }
            //}
            //initial.Text = initialLetter;
        }
    }
}
