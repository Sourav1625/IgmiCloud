using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using IGMICloudApplication.Models;
using IGMICloudApplication.Models.ApiResponse.ListFolder;
using IGMICloudApplication.ViewModels;
using NLog;

namespace IGMICloudApplication.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public MainView()
        {
            InitializeComponent();
            DataContext = MainViewModel.Instance;
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        void OnInputFieldFocused(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TextBox)
            {
                var userNameField = sender as TextBox;
                if (userNameField.Text == "Username")
                {
                    userNameField.Text = "";
                }
                if (userNameField.Text == "Email")
                {
                    userNameField.Text = "";
                }
            }
            else
            {
                var passwordField = sender as PasswordBox;
                if (passwordField.Password == "Password")
                {
                    passwordField.Password = "";
                }
            }
        }
        void OnInputFieldFocusedLost(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is TextBox)
            {
                var inputField = sender as TextBox;
                if (inputField.Name == "UserNameField" && string.IsNullOrEmpty(inputField.Text))
                {
                    inputField.Text = "Username";
                }
                else if (inputField.Name == "SendEmailField" && string.IsNullOrEmpty(inputField.Text))
                {
                    inputField.Text = "Email";
                }
            }
            else
            {
                var passwordField = sender as PasswordBox;
                if (string.IsNullOrEmpty(passwordField.Password))
                {
                    passwordField.Password = "Password";
                }
            }
        }
        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            Logger.Info("Checking username and password for log in");
            if (string.IsNullOrEmpty(MainViewModel.Instance.LoginViewModel.UserName))
            {
                Console.WriteLine("Username is empty");
                Logger.Info("Can not Login as Username is empty");
            }
            else if (string.IsNullOrEmpty(MainViewModel.Instance.LoginViewModel.Password) || MainViewModel.Instance.LoginViewModel.Password == "Password")
            {
                Console.WriteLine("Password is empty");
                Logger.Info("Can not Login as Password is empty");
            }
            else
            {
                MainViewModel.Instance.LoginViewModel.LoginState = LoginState.LoggingIn;
                var progress = new Progress<int>(x => MainViewModel.Instance.LoginViewModel.ProgressbarPercentage = x);
                await Task.Run(() => MainViewModel.Instance.LoginViewModel.ProcessLoginAsync(progress));
            }
        }
        private void Window_MouseDown(object sender, RoutedEventArgs e)
        {
            var addFolderPopup = (Popup)mainLayout.ContentTemplate.FindName("AddAndEditFolderPopup", mainLayout);
            var dashboardPanel = (DockPanel)mainLayout.ContentTemplate.FindName("DashboardPanel", mainLayout);
            dashboardPanel.Opacity = 1;
            dashboardPanel.Focusable = true;
            dashboardPanel.IsHitTestVisible = true;
            addFolderPopup.IsOpen = false;
        }
        //Drag and Drop feature is ended here
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Mouse.OverrideCursor = null;
                if (WindowStyle == WindowStyle.None)
                {
                    WindowStyle = WindowStyle.ThreeDBorderWindow;
                }
                else
                {
                    WindowStyle = WindowStyle.None;
                }
            }
        }
        public void Open_Folder_Creation_Popup(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.FolderViewModel.FolderName = null;
            MainViewModel.Instance.FolderViewModel.FolderPassword = null;
            MainViewModel.Instance.FolderViewModel.PublicUrl = null;
            MainViewModel.Instance.FolderViewModel.ParentFolderId = 0;
            MainViewModel.Instance.FolderViewModel.EditedFolderId = 0;
            MainViewModel.Instance.FolderViewModel.FolderActionType = "Create";
            var addFolderPopup = (Popup)mainLayout.ContentTemplate.FindName("AddAndEditFolderPopup", mainLayout);
            var dashboardPanel = (DockPanel)mainLayout.ContentTemplate.FindName("DashboardPanel", mainLayout);
            var addEditFolderIcon = (Image)mainLayout.ContentTemplate.FindName("AddEditFolderIcon", mainLayout);
            var addEditFolderButton = (TextBlock)mainLayout.ContentTemplate.FindName("AddEditFolderButton", mainLayout);
            var addEditFolderHeader = (TextBlock)mainLayout.ContentTemplate.FindName("AddEditFolderHeader", mainLayout);

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri("pack://application:,,,/IGMICloudApplication;component/Views/Icons/FolderAddIcon.png");
            logo.EndInit();
            addEditFolderIcon.Source = logo;

            addEditFolderButton.Text = "Add Folder";
            addEditFolderHeader.Text = "Add Folder";
            dashboardPanel.Opacity = 0.8;
            dashboardPanel.IsHitTestVisible = false;
            addFolderPopup.IsOpen = true;

        }
        public void Open_Sub_Folder_Creation_Popup(object sender, RoutedEventArgs e)
        {
            MenuItem mnu = sender as MenuItem;
            if (mnu != null)
            {
                if (mnu.DataContext is FolderElement)
                {

                    MainViewModel.Instance.FolderViewModel.EditedFolderId = 0;
                    MainViewModel.Instance.FolderViewModel.ParentFolderId = ((FolderElement)mnu.DataContext).Id;
                }else if (mnu.Tag != null)
                {
                    MainViewModel.Instance.FolderViewModel.EditedFolderId = 0;
                    MainViewModel.Instance.FolderViewModel.ParentFolderId = Int32.Parse(mnu.Tag.ToString());
                }
            }
            MainViewModel.Instance.FolderViewModel.FolderName = null;
            MainViewModel.Instance.FolderViewModel.FolderPassword = null;
            MainViewModel.Instance.FolderViewModel.PublicUrl = null;
            MainViewModel.Instance.FolderViewModel.FolderActionType = "Create";
            var addFolderPopup = (Popup)mainLayout.ContentTemplate.FindName("AddAndEditFolderPopup", mainLayout);
            var dashboardPanel = (DockPanel)mainLayout.ContentTemplate.FindName("DashboardPanel", mainLayout);
            var addEditFolderIcon = (Image)mainLayout.ContentTemplate.FindName("AddEditFolderIcon", mainLayout);
            var addEditFolderButton = (TextBlock)mainLayout.ContentTemplate.FindName("AddEditFolderButton", mainLayout);
            var addEditFolderHeader = (TextBlock)mainLayout.ContentTemplate.FindName("AddEditFolderHeader", mainLayout);

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri("pack://application:,,,/IGMICloudApplication;component/Views/Icons/FolderAddIcon.png");
            logo.EndInit();
            addEditFolderIcon.Source = logo;

            addEditFolderButton.Text = "Add Folder";
            addEditFolderHeader.Text = "Add Folder";
            dashboardPanel.Opacity = 0.8;
            dashboardPanel.IsHitTestVisible = false;
            addFolderPopup.IsOpen = true;
        }
        private void Close_Folder_Creation_Popup(object sender, RoutedEventArgs e)
        {
            close_Folder_Creation_Popup();
        }

        public void Folder_Create_Update(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.Instance.FolderViewModel.FolderActionType.Equals("Create"))
            {
                MainViewModel.Instance.FolderViewModel.AddFolderCommand.Execute();
            }
            else
            {
                MainViewModel.Instance.FolderViewModel.EditFolderCommand.Execute();
            }
            close_Folder_Creation_Popup();
        }

        public void Delete_Folder(object sender, RoutedEventArgs e)
        {
            MenuItem mnu = sender as MenuItem;
            if (mnu != null)
            {
                if (mnu.DataContext is FolderElement)
                {

                    MainViewModel.Instance.FolderViewModel.EditedFolderId = ((FolderElement)mnu.DataContext).Id;
                }
            }
            MainViewModel.Instance.FolderViewModel.DeleteFolderCommand.Execute();
        }

        public void close_Folder_Creation_Popup()
        {
            var addFolderPopup = (Popup)mainLayout.ContentTemplate.FindName("AddAndEditFolderPopup", mainLayout);
            if (addFolderPopup.IsOpen)
            {
                MainViewModel.Instance.FolderViewModel.isFolderNameEmpty = false;
                Window_MouseDown(null, null);
            }
        }

        public void Open_Folder_Update_Popup(object sender, RoutedEventArgs e)
        {
            MenuItem mnu = sender as MenuItem;
            string folderName = null;
            if (mnu != null)
            {
                if (mnu.DataContext is FolderElement)
                {
                    MainViewModel.Instance.FolderViewModel.EditedFolderId = ((FolderElement)mnu.DataContext).Id;
                    MainViewModel.Instance.FolderViewModel.ParentFolderId = ((FolderElement)mnu.DataContext).ParentId == null ? 0 : Int32.Parse(((FolderElement)mnu.DataContext).ParentId.ToString());
                    MainViewModel.Instance.FolderViewModel.FolderName = ((FolderElement)mnu.DataContext).FolderName;
                    folderName = ((FolderElement)mnu.DataContext).FolderName;
                }
                else if (mnu.Tag != null)
                {
                    MainViewModel.Instance.FolderViewModel.EditedFolderId = Int32.Parse(mnu.Tag.ToString());
                }
            }


            MainViewModel.Instance.FolderViewModel.FolderActionType = "Update";
            var updateFolderPopup = (Popup)mainLayout.ContentTemplate.FindName("AddAndEditFolderPopup", mainLayout);
            var dashboardPanel = (DockPanel)mainLayout.ContentTemplate.FindName("DashboardPanel", mainLayout);
            var addEditFolderIcon = (Image)mainLayout.ContentTemplate.FindName("AddEditFolderIcon", mainLayout);
            var addEditFolderButton = (TextBlock)mainLayout.ContentTemplate.FindName("AddEditFolderButton", mainLayout);
            var addEditFolderHeader = (TextBlock)mainLayout.ContentTemplate.FindName("AddEditFolderHeader", mainLayout);

            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri("pack://application:,,,/IGMICloudApplication;component/Views/Icons/FolderEditIcon.png");
            logo.EndInit();
            addEditFolderIcon.Source = logo;

            addEditFolderButton.Text = "Update Folder";
            addEditFolderHeader.Text = "Edit Existing Folder (" + folderName + ")";
            dashboardPanel.Opacity = 0.8;
            dashboardPanel.IsHitTestVisible = false;
            updateFolderPopup.IsOpen = true;
            MainViewModel.Instance.FolderViewModel.GetSpecificFolder(MainViewModel.Instance.FolderViewModel.EditedFolderId);
        }

        public void Open_Account(object sender, RoutedEventArgs e)
        {
            MainViewModel.Instance.LoginViewModel.SwitchView = SwitchViewEnum.Account;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            if (comboBox.SelectedItem != null)
            {
                MainViewModel.Instance.FolderViewModel.ParentFolderId = ((FolderElement)comboBox.SelectedItem).Id;
            }

        }

        private void chkPwd_Click(object sender, RoutedEventArgs e)
        {
            var checkBox = sender as CheckBox;
            if (checkBox.IsChecked == true)
            {
                MainViewModel.Instance.FolderViewModel.EnablePassword = 1;
            }
            else
            {
                MainViewModel.Instance.FolderViewModel.EnablePassword = 0;
            }
        }

        public void SelectedItemChanged(object sender, MouseButtonEventArgs e)
        {
            if (sender is TreeView)
            {
                TreeView folderTreeView = sender as TreeView;
                if (folderTreeView.SelectedItem is FolderElement)
                {
                    FolderElement folderElement = (FolderElement)folderTreeView.SelectedItem;
                    MainViewModel.Instance.FolderViewModel.SelectedItem = folderElement;
                    MainViewModel.Instance.FolderViewModel.IsRootFolderSelected = false;
                    MainViewModel.Instance.FolderViewModel.IsChildFolderSelected = true;
                    MainViewModel.Instance.FolderViewModel.FolderNavigationCommand.Execute();
                }
                else
                {
                    MainViewModel.Instance.FolderViewModel.FolderCountMsg = "Root Folder - " + MainViewModel.Instance.FolderViewModel.FolderList.Count + " Folders";
                    MainViewModel.Instance.FolderViewModel.SelectedItem = null;
                    MainViewModel.Instance.FolderViewModel.IsRootFolderSelected = true;
                    MainViewModel.Instance.FolderViewModel.IsChildFolderSelected = false;
                    MainViewModel.Instance.LoginViewModel.SwitchView = SwitchViewEnum.FolderManagement;
                }
            }
            else
            {
                ListView listView = sender as ListView;
                if (listView!=null && listView.SelectedItem is FolderElement)
                {
                    FolderElement folderElement = (FolderElement)listView.SelectedItem;
                    MainViewModel.Instance.FolderViewModel.SelectedItem = folderElement;
                    MainViewModel.Instance.FolderViewModel.IsRootFolderSelected = false;
                    MainViewModel.Instance.FolderViewModel.IsChildFolderSelected = true;
                    MainViewModel.Instance.FolderViewModel.FolderNavigationCommand.Execute();
                }
            }
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu cm = this.FindResource("profileMenu") as ContextMenu;
            cm.PlacementTarget = sender as Button;
            cm.IsOpen = true;
        }
    }
}
