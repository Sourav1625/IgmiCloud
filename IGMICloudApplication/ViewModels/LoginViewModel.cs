using IGMICloudApplication.APIs;
using IGMICloudApplication.Commands;
using IGMICloudApplication.Models;
using IGMICloudApplication.Views;
using NLog;
using RestSharp;
using System;
using System.ComponentModel;

namespace IGMICloudApplication.ViewModels
{
    public enum LoginState
    {
        LoggedOut,
        LoggedIn,
        LoggingIn,
        LoggingOut
    }
    public enum SwitchViewEnum
    {
        Dashboard,
        Workspace,
        UserProfile
    }
    public class LoginViewModel : ViewModelBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public string userName;
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
                OnPropertyChanged("userName");
            }
        }
        public string initial;
        public string Initial
        {
            get
            {
                return initial;
            }
            set
            {
                initial = value;
                OnPropertyChanged("initial");
            }
        }
        public string password { get; set; }
        public string sendPasswordEmail { get; set; }
        private LoginState loginState;
        public LoginState LoginState
        {
            get
            {
                return loginState;
            }
            set
            {
                loginState = value;
                OnPropertyChanged("LoginState");
            }
        }
        private bool isForgotPasswordFormVisible;
        public bool IsForgotPasswordFormVisible
        {
            get
            {
                return isForgotPasswordFormVisible;
            }
            set
            {
                isForgotPasswordFormVisible = value;
                OnPropertyChanged("IsForgotPasswordFormVisible");
            }
        }
        private SwitchViewEnum switchView;
        public SwitchViewEnum SwitchView
        {
            get
            {
                return switchView;
            }
            set
            {
                switchView = value;
                OnPropertyChanged("SwitchView");
            }
        }
        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand DashboardCommand { get; private set; }
        public DelegateCommand WorkspaceCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }
        public DelegateCommand SettingsCommand { get; private set; }
        
        public DelegateCommand ShowAndHideForgotPasswordForm { get; private set; }        
        private string loginEndpoint = "/authorize";
        public LoginViewModel()
        {
            if (string.IsNullOrEmpty(password))
            {
                //password = "Password";
                password = "igmi@123";
            }
            if (string.IsNullOrEmpty(userName))
            {
                //userName = "Username";
                userName = "niloy.bauri";
            }
            if (string.IsNullOrEmpty(sendPasswordEmail))
            {
                sendPasswordEmail = "Email";
            }
            ShowAndHideForgotPasswordForm = new DelegateCommand(() =>
            {
                IsForgotPasswordFormVisible = !IsForgotPasswordFormVisible;
            });
            LoginCommand = new DelegateCommand(() =>
            {
                Logger.Info("Checking username and password for log in");
                if (string.IsNullOrEmpty(userName))
                {
                    Console.WriteLine("Username is empty");
                    Logger.Info("Can not Login as Username is empty");
                }
                else if (string.IsNullOrEmpty(password) || password == "Password")
                {
                    Console.WriteLine("Password is empty");
                    Logger.Info("Can not Login as Password is empty");
                }
                try
                {
                    LoginState = LoginState.LoggingIn;
                    //Now Calling Login API
                    Logger.Debug("username and password is ok..now calling login api...");
                    Logger.Debug("loginEndpoint: " + loginEndpoint);
                    Logger.Debug("userName: " + userName);
                    var cloudAPIObj = new IGMICloudAPIs();
                    string loginResponse = cloudAPIObj.DoLogin(loginEndpoint, userName, password);
                    if (loginResponse != null)
                    {
                        var responseJson = SimpleJson.DeserializeObject(loginResponse);
                        if (responseJson is JsonObject jObj)
                        {
                            string responseStatus = (string)jObj["_status"];
                            Logger.Debug("response  status: " + responseStatus);
                            if (responseStatus == "success")
                            {
                               // UserProfile.userName = userName;
                                string access_token = (string)((JsonObject)jObj["data"])[0];
                                string account_id = (string)((JsonObject)jObj["data"])[1];
                                Console.WriteLine("User Access Token: " + access_token);
                                Console.WriteLine("User Account id: " + account_id);
                                Logger.Info("User Account id: " + account_id);
                                Initial = getInitial(userName);
                                LoginState = LoginState.LoggedIn;
                                SwitchView = SwitchViewEnum.Dashboard;
                            }
                            else
                            {                             
                                LoginState = LoginState.LoggedOut;
                            }
                        }
                        else
                        {                          
                            LoginState = LoginState.LoggedOut;
                        }
                    }
                    else
                    {
                        LoginState = LoginState.LoggedOut;
                    }
                }catch(Exception ex)
                {
                    Logger.Debug("Error occurred while loggin....Error: "+ex.Message);
                    LoginState = LoginState.LoggedOut;
                }
            });
            DashboardCommand = new DelegateCommand(() =>
            {
                SwitchView = SwitchViewEnum.Dashboard;
            });
            WorkspaceCommand = new DelegateCommand(() =>
            {
                SwitchView = SwitchViewEnum.Workspace;
            });
            SettingsCommand = new DelegateCommand(() =>
            {
                SwitchView = SwitchViewEnum.UserProfile;
            });
            LogoutCommand = new DelegateCommand(() =>
            {
                LoginState = LoginState.LoggedOut;
            });
           
        }
        private string getInitial(string userName)
        {
            string[] multiArray = userName.Split(new Char[] { ' ', ',', '.', '-', '\n', '\t' });
            string uname = userName.Replace(",", " ");
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
            return initialLetter;
        }
    }
}
