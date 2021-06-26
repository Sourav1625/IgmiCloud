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
    /// Interaction logic for TrashCan.xaml
    /// </summary>
    public partial class TrashCan : UserControl
    {
        public TrashCan()
        {
            InitializeComponent();
        }

        private void First_Click_Folder_Listing(object sender, RoutedEventArgs e)
        {            
            MainViewModel.Instance.FolderViewModel.GetTrashFolders("first");
        }
        private void Previous_Click_Folder_Listing(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.FolderViewModel.GetTrashFolders("previous");

        }
        private void Next_Click_Folder_Listing(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.FolderViewModel.GetTrashFolders("next");
        }

        private void Last_Click_Folder_Listing(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.FolderViewModel.GetTrashFolders("last");
        }

        private void Page_No_Button_Click(Object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            String page_No = btn.Content.ToString();
            int pageNo = 0;
            try
            {
                pageNo = Convert.ToInt32(page_No);
            }
            catch (Exception ex)
            {
                pageNo = 0;
            }
            //MainViewModel.Instance.FolderViewModel.GetFolderList(0, 0, "last", pageNo);
        }
    }
}
