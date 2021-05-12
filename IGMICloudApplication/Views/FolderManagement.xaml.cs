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

namespace IGMICloudApplication.Views
{
    /// <summary>
    /// Interaction logic for FolderManagementCommand.xaml
    /// </summary>
    public partial class FolderManagement : UserControl
    {
        public FolderManagement()
        {
            InitializeComponent();
        }
        private void Open_Sub_Folder_Creation_Popup(object sender, RoutedEventArgs e)
        {
            MainView parentWindow = (MainView)Window.GetWindow(this);
            parentWindow.Open_Sub_Folder_Creation_Popup(sender, e);
        }
        private void Open_Folder_Update_Popup(object sender, RoutedEventArgs e)
        {
            MainView parentWindow = (MainView)Window.GetWindow(this);
            parentWindow.Open_Folder_Update_Popup(sender, e);
        }
        private void Delete_Folder(object sender, RoutedEventArgs e)
        {
            MainView parentWindow = (MainView)Window.GetWindow(this);
            parentWindow.Delete_Folder(sender, e);
        }
    }
}
