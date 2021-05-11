using IGMICloudApplication.APIs;
using IGMICloudApplication.Commands;
using IGMICloudApplication.Models;
using IGMICloudApplication.Models.ApiResponse.UserProfile;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGMICloudApplication.ViewModels
{
    class UserProfileViewModel : ViewModelBase
    {
        string getUserDetailsEndPoint = "/account/info";
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private string title;
        public string Title
        {
            get { return title; }
            set
            {                
                SetProperty(ref title, value);
            }
        }

        private string firstname;
        public string Firstname
        {
            get { return firstname; }
            set
            {
                SetProperty(ref firstname, value);
            }
        }

        private string lastname;
        public string Lastname
        {
            get { return lastname; }
            set
            {
                SetProperty(ref lastname, value);
            }
        }

        private int languageId;
        public int LanguageId
        {
            get { return languageId; }
            set
            {
                SetProperty(ref languageId, value);
            }
        }        

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                SetProperty(ref email, value);
            }
        }

        public DelegateCommand GetUserDetailsCommand { get; private set; }

        public UserProfileViewModel()
        {
            GetUserDetailsCommand = new DelegateCommand(() =>
            {
                Logger.Info("Fetching user details");
                try
                {
                    GetUserDetails();                  
                    Logger.Info("User details fetched successfully");                   
                }
                catch (Exception e)
                {
                    Logger.Error("Error while Fetching user details");
                }
            });
        }

        public void GetUserDetails()
        {
            FolderViewModel folderViewModel = new FolderViewModel();
            string apiResponse = folderViewModel.getAccessToken();
            if (apiResponse.Equals("error"))
            {
                Logger.Error("Could not validate access_token and account_id during GetUserDetails call");
                MainViewModel.Instance.ToastViewModel.ShowError("Could not validate access_token and account_id");
            }
            var cloudAPIFolderObj = new IGMICloudAPIs();
            string response = cloudAPIFolderObj.FetchUserDetails(getUserDetailsEndPoint, LoggedinProfile.accessToken, LoggedinProfile.accountId);
            if (response != null)
            {
                UserProfile userProfile = JsonConvert.DeserializeObject<UserProfile>(response);
                if (userProfile.Data != null)
                {
                    Title = userProfile.Data.Title;
                    Firstname = userProfile.Data.Firstname;
                    Lastname = userProfile.Data.Lastname;
                    Email = userProfile.Data.Email;
                    LanguageId = userProfile.Data.LanguageId==null?0:Int32.Parse(userProfile.Data.LanguageId.ToString());
                }
            }
            else
            {
                MainViewModel.Instance.ToastViewModel.ShowError("Could not able to fetch user profile");
            }
        }

    }
}
