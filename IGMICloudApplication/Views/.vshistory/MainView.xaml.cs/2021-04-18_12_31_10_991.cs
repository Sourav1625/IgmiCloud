using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IGMICloudApplication.ViewModels;

namespace IGMICloudApplication.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        void OnInputFieldFocused(object sender, RoutedEventArgs e)
        {
            var joinLobbyPasswordBox = sender as PasswordBox;
            SolidColorBrush myBrush = new SolidColorBrush();

            myBrush.Color = Color.FromRgb(195, 234, 244);
            joinLobbyPasswordBox.Foreground = myBrush;
            joinLobbyPasswordBox.Password = "";
        }
        void OnInputFieldFocusedLost(object sender, RoutedEventArgs e)
        {
            var joinLobbyPasswordBox = sender as PasswordBox;
            if (joinLobbyPasswordBox.Password == null || joinLobbyPasswordBox.Password == "")
            {
                SolidColorBrush myBrush = new SolidColorBrush();
                myBrush.Color = Color.FromRgb(50, 64, 71);
                joinLobbyPasswordBox.Foreground = myBrush;
                joinLobbyPasswordBox.Password = "********";
                joinLobbyPasswordBox.FontSize = 12;
            }
        }
    }
}
