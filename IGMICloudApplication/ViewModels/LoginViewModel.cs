using IGMICloudApplication.APIs;
using IGMICloudApplication.Commands;
using IGMICloudApplication.Models;
using IGMICloudApplication.Views;
using NLog;
using RestSharp;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

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
        FolderManagement,
        MySharedResources,
        RecentFiles,
        AllFiles,
        TrashCan,
        Account
    }
    public class LoginViewModel : ViewModelBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
        private string sendPasswordEmail;
        public string SendPasswordEmail
        {
            get { return sendPasswordEmail; }
            set { SetProperty(ref sendPasswordEmail, value); }
        }
        private string displayName;
        public string DisplayName
        {
            get { return displayName; }
            set { SetProperty(ref displayName, value); }
        }
        private LoginState loginState;
        public LoginState LoginState
        {
            get { return loginState; }
            set { SetProperty(ref loginState, value); }
        }
        private int progressbarPercentage;
        public int ProgressbarPercentage
        {
            get { return progressbarPercentage; }
            set
            {
                progressbarPercentage = value;
                base.NotifyPropertyChanged("ProgressbarPercentage");
            }
        }
        private bool isForgotPasswordFormVisible;
        public bool IsForgotPasswordFormVisible
        {
            get { return isForgotPasswordFormVisible; }
            set { SetProperty(ref isForgotPasswordFormVisible, value); }
        }
        private SwitchViewEnum switchView;
        public SwitchViewEnum SwitchView
        {
            get { return switchView; }
            set { SetProperty(ref switchView, value); }
        }
        public DelegateCommand LoginCommand { get; private set; }
        public DelegateCommand FolderManagementCommand { get; private set; }
        public DelegateCommand MySharedResourcesManagementCommand { get; private set; }
        public DelegateCommand RecentFilesManagementCommand { get; private set; }
        public DelegateCommand AllFilesManagementCommand { get; private set; }
        public DelegateCommand TrashCanManagementCommand { get; private set; }
        public DelegateCommand LogoutCommand { get; private set; }
        
        
        public DelegateCommand ShowAndHideForgotPasswordForm { get; private set; }        
        private string loginEndpoint = "/authorize";
        private string userDetailsEndpoint = "/account/info";
        public LoginViewModel()
        {
            if (string.IsNullOrEmpty(userName))
            {
                //UserName = "Username";
                UserName = "niloy.bauri";
            }
            if (string.IsNullOrEmpty(password))
            {
                //Password = "Password";
                Password = "igmi@123";
            }
            if (string.IsNullOrEmpty(sendPasswordEmail))
            {
                SendPasswordEmail = "Email";
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
                else
                {
                    LoginState = LoginState.LoggingIn;
                }
            });
            FolderManagementCommand = new DelegateCommand(() =>
            {
                MainViewModel.Instance.FolderViewModel.IsRootFolderSelected = true;
                SwitchView = SwitchViewEnum.FolderManagement;
            });
            MySharedResourcesManagementCommand = new DelegateCommand(() =>
            {
                MainViewModel.Instance.FolderViewModel.IsRootFolderSelected = false;
                SwitchView = SwitchViewEnum.MySharedResources;
            });
            RecentFilesManagementCommand = new DelegateCommand(() =>
            {
                MainViewModel.Instance.FolderViewModel.IsRootFolderSelected = false;
                SwitchView = SwitchViewEnum.RecentFiles;
            });
            AllFilesManagementCommand = new DelegateCommand(() =>
            {
                MainViewModel.Instance.FolderViewModel.IsRootFolderSelected = false;
                SwitchView = SwitchViewEnum.AllFiles;
            });
            TrashCanManagementCommand = new DelegateCommand(() =>
            {
                MainViewModel.Instance.FolderViewModel.IsRootFolderSelected = false;
                MainViewModel.Instance.FolderViewModel.GetTrashFolders("");
                SwitchView = SwitchViewEnum.TrashCan;
            });
            LogoutCommand = new DelegateCommand(() =>
            {
                LoginState = LoginState.LoggedOut;
            });
           
        }
        public async Task ProcessLoginAsync(IProgress<int> progress)
        {
            try
            {
                progress.Report(10);
                await Task.Delay(400);
                //Now Calling Login API
                Logger.Debug("username and password is ok..now calling login api...");
                Logger.Debug("loginEndpoint: " + loginEndpoint);
                Logger.Debug("userName: " + UserName);
                var cloudAPIObj = new IGMICloudAPIs();
                progress.Report(20);
                string loginResponse = cloudAPIObj.DoLogin(loginEndpoint, userName, password);
                if (loginResponse != null)
                {
                    progress.Report(30);
                    await Task.Delay(400);
                    var responseJson = SimpleJson.DeserializeObject(loginResponse);
                    if (responseJson is JsonObject jObj)
                    {
                        progress.Report(40);
                        await Task.Delay(400);
                        string responseStatus = (string)jObj["_status"];
                        Logger.Debug("response  status: " + responseStatus);
                        if (responseStatus == "success")
                        {
                            progress.Report(50);
                            await Task.Delay(400);
                            string access_token = (string)((JsonObject)jObj["data"])[0];
                            int account_id = Int16.Parse((string)((JsonObject)jObj["data"])[1]);
                            Console.WriteLine("User Access Token: " + access_token);
                            Console.WriteLine("User Account id: " + account_id);
                            Logger.Info("User Account id: " + account_id);
                            //Calling User details API
                            LoggedinProfile.userName = userName;
                            LoggedinProfile.password = password;
                            LoggedinProfile.accountId = account_id;
                            progress.Report(60);
                            string userDetailsResponse = cloudAPIObj.FetchUserDetails(userDetailsEndpoint, access_token, account_id);
                            progress.Report(70);
                            await Task.Delay(400);
                            if (userDetailsResponse != null)
                            {
                                var userResponseJson = SimpleJson.DeserializeObject(userDetailsResponse);
                                if (userResponseJson is JsonObject userObj)
                                {
                                    progress.Report(80);
                                    await Task.Delay(400);
                                    string useresponseStatus = (string)userObj["_status"];
                                    Logger.Debug("response  status: " + useresponseStatus);
                                    if (useresponseStatus == "success")
                                    {
                                        progress.Report(90);
                                        await Task.Delay(400);
                                        DisplayName = (string)((JsonObject)userObj["data"])["firstname"];
                                        Logger.Info("User Display Name: " + displayName);
                                        progress.Report(100);
                                        await Task.Delay(400);
                                        LoginState = LoginState.LoggedIn;
                                        SwitchView = SwitchViewEnum.FolderManagement;
                                        MainViewModel.Instance.FolderViewModel.IsRootFolderSelected = true;
                                        MainViewModel.Instance.FolderViewModel.GetFolderList(0, 0,"", 0);
                                        MainViewModel.Instance.FolderViewModel.GetFolderListForFileUpload(0, 0);                                        
                                        MainViewModel.Instance.AccountViewModel.GetUserDetails();
                                    }
                                }
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
                }
                else
                {
                    LoginState = LoginState.LoggedOut;
                }
            }
            catch (Exception ex)
            {
                Logger.Debug("Error occurred while loggin....Error: " + ex.Message);
                LoginState = LoginState.LoggedOut;
            }
        }
    }
}
