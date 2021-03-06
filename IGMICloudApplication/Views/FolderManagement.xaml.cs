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
        private void Open_Folder_Creation_Popup(object sender, RoutedEventArgs e)
        {
            MainView parentWindow = (MainView)Window.GetWindow(this);
            parentWindow.Open_Folder_Creation_Popup(sender, e);
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
        private void SelectedItemChanged(object sender, MouseButtonEventArgs e)
        {
            MainView parentWindow = (MainView)Window.GetWindow(this);
            parentWindow.SelectedItemChanged(sender, e);
        }
        private void First_Click_Folder_Listing(object sender, RoutedEventArgs e)
        {            
            MainViewModel.Instance.FolderViewModel.GetFolderList(0, 0, "first", 0);
        }
        private void Previous_Click_Folder_Listing(object sender, RoutedEventArgs e)
        {            
            MainViewModel.Instance.FolderViewModel.GetFolderList(0, 0, "previous", 0);
        }
        private void Next_Click_Folder_Listing(object sender, RoutedEventArgs e)
        {           
            MainViewModel.Instance.FolderViewModel.GetFolderList(0, 0, "next", 0);
        }
        private void Last_Click_Folder_Listing(object sender, RoutedEventArgs e)
        {            
            MainViewModel.Instance.FolderViewModel.GetFolderList(0, 0, "last", 0);
        }
        private void Page_No_Button_Click(Object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            String page_No = btn.Content.ToString();
            int pageNo = 0;
            try
            {
                pageNo = Convert.ToInt32(page_No);
            }catch(Exception ex)
            {
                pageNo = 0;
            }
            MainViewModel.Instance.FolderViewModel.GetFolderList(0, 0, "last", pageNo);            
        }
    }
}
