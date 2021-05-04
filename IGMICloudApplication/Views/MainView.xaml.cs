using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
                MainViewModel.Instance.LoginViewModel.ProgressbarPercentage = 10;
                MainViewModel.Instance.LoginViewModel.LoginState = LoginState.LoggingIn;                
                var progress = new Progress<int>(x => MainViewModel.Instance.LoginViewModel.ProgressbarPercentage = x);
                await Task.Run(() => MainViewModel.Instance.LoginViewModel.ProcessLoginAsync(progress));
            }
        }
    }
}
