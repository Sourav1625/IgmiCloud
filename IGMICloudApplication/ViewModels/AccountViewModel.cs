using IGMICloudApplication.APIs;
using IGMICloudApplication.Commands;
using IGMICloudApplication.Models;
using IGMICloudApplication.Models.ApiResponse.UserProfile;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
namespace IGMICloudApplication.ViewModels
{
    public class LanguageyCombo
    {
        public string _Key { get; set; }
        public int _Value { get; set; }

        public LanguageyCombo(string _key, int _value)
        {
            _Key = _key;
            _Value = _value;
        }
    }

    public class TitleCombo
    {
        public string _Key { get; set; }       

        public TitleCombo(string _key)
        {
            _Key = _key;
           
        }
    }

    public class WatermarkPositionCombo
    {
        public string _Key { get; set; }
        public int _Value { get; set; }

        public WatermarkPositionCombo(string _key, int _value)
        {
            _Key = _key;
            _Value = _value;
        }
    }

    public class AccountViewModel : ViewModelBase
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

        public List<TitleCombo> ValuesForTitle { get; }
       
        public List<LanguageyCombo> ValuesForLanguage { get; }
        private int _selectedValueForLanguage;
        public int SelectedValueForLanguage
        {
            get { return _selectedValueForLanguage; }
            set
            {
                SetProperty(ref _selectedValueForLanguage, value);
            }
        }
      
        public List<WatermarkPositionCombo> ValuesForWatermark { get; }
        private int _selectedValueForWatermark;
        public int SelectedValueForWatermark
        {
            get { return _selectedValueForWatermark; }
            set
            {
                SetProperty(ref _selectedValueForWatermark, value);
            }
        }

        public DelegateCommand GetUserDetailsCommand { get; private set; }

        public AccountViewModel()
        {
            ValuesForLanguage = new List<LanguageyCombo>();
            ValuesForLanguage.Add(new LanguageyCombo("English (en)", 1));
            SelectedValueForLanguage = 1;
            ValuesForTitle = new List<TitleCombo>();
            ValuesForTitle.Add(new TitleCombo("Mr"));
            ValuesForTitle.Add(new TitleCombo("Ms"));
            ValuesForTitle.Add(new TitleCombo("Mrs"));
            ValuesForTitle.Add(new TitleCombo("Miss"));
            ValuesForTitle.Add(new TitleCombo("Dr"));
            ValuesForTitle.Add(new TitleCombo("Pro"));
            _selectedValueForWatermark = 0;

            ValuesForWatermark = new List<WatermarkPositionCombo>();
            ValuesForWatermark.Add(new WatermarkPositionCombo("Top-Left",0));
            ValuesForWatermark.Add(new WatermarkPositionCombo("Top-Middle", 1));
            ValuesForWatermark.Add(new WatermarkPositionCombo("Top-Right", 2));
            ValuesForWatermark.Add(new WatermarkPositionCombo("Right",3));
            ValuesForWatermark.Add(new WatermarkPositionCombo("Bottom-Right", 4));
            ValuesForWatermark.Add(new WatermarkPositionCombo("Bottom-Middle", 5));
            ValuesForWatermark.Add(new WatermarkPositionCombo("Bottom-Left", 6));
            ValuesForWatermark.Add(new WatermarkPositionCombo("Left", 7));
            ValuesForWatermark.Add(new WatermarkPositionCombo("Middle", 8));

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
