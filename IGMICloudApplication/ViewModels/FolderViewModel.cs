using IGMICloudApplication.APIs;
using IGMICloudApplication.Commands;
using IGMICloudApplication.Models;
using IGMICloudApplication.Models.ApiResponse.EditFolder;
using IGMICloudApplication.Models.ApiResponse.ListFolder;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IGMICloudApplication.ViewModels
{
    public class FolderPrivacyCombo
    {
        public string _Key { get; set; }
        public int _Value { get; set; }

        public FolderPrivacyCombo(string _key, int _value)
        {
            _Key = _key;
            _Value = _value;
        }
    }
    public class WaterMarkPreview
    {
        public string _Key { get; set; }
        public int _Value { get; set; }

        public WaterMarkPreview(string _key, int _value)
        {
            _Key = _key;
            _Value = _value;
        }
    }
    public class AllowDownloading
    {
        public string _Key { get; set; }
        public int _Value { get; set; }

        public AllowDownloading(string _key, int _value)
        {
            _Key = _key;
            _Value = _value;
        }
    }
     
    public class FolderViewModel : ViewModelBase
    {
        string getFolderDetailsEndPoint="/folder/listing";
        string createFolderEndPoint = "/folder/create";
        string editFolderEndPoint = "/folder/edit";
        string deleteFolderEndPoint = "/folder/delete";
        private string loginEndpoint = "/authorize";
        string uploadFileEndPoint = "/file/upload";
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public DelegateCommand AddFolderCommand { get; private set; }
        public DelegateCommand EditFolderCommand { get; private set; }
        public DelegateCommand DeleteFolderCommand { get; private set; }
        public DelegateCommand FolderNavigationCommand { get; private set; }
        public DelegateCommand ShowAndHideOptionsCommand { get; private set; }

        private bool m_isFolderNameEmpty = false;
        public bool isFolderNameEmpty
        {
            get
            {
                return m_isFolderNameEmpty;
            }
            set { SetProperty(ref m_isFolderNameEmpty, value); }
        }
        private Folder folderDetails;
        public Folder FolderDetails
        {
            get
            {
                return folderDetails;
            }
            set { SetProperty(ref folderDetails, value); }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get
            {
                return isExpanded;
            }
            set { SetProperty(ref isExpanded, value); }
        }

        private int trashCurentPage;
        public int TrashCurentPage
        {
            get
            {
                return trashCurentPage;
            }
            set { SetProperty(ref trashCurentPage, value); }
        }

        private int trashTotalPage;
        public int TrashTotalPage
        {
            get
            {
                return trashTotalPage;
            }
            set { SetProperty(ref trashTotalPage, value); }
        }

        private bool isTrashFirstButtonEnable;
        public bool IsTrashFirstButtonEnable
        {
            get
            {
                return isTrashFirstButtonEnable;
            }
            set { SetProperty(ref isTrashFirstButtonEnable, value); }
        }

        private bool isTrashPreviousButtonEnable;
        public bool IsTrashPreviousButtonEnable
        {
            get
            {
                return isTrashPreviousButtonEnable;
            }
            set { SetProperty(ref isTrashPreviousButtonEnable, value); }
        }

        private bool isTrashNextButtonEnable;
        public bool IsTrashNextButtonEnable
        {
            get
            {
                return isTrashNextButtonEnable;
            }
            set { SetProperty(ref isTrashNextButtonEnable, value); }
        }

        private bool isTrashLastButtonEnable;
        public bool IsTrashLastButtonEnable
        {
            get
            {
                return isTrashLastButtonEnable;
            }
            set { SetProperty(ref isTrashLastButtonEnable, value); }
        }

        private int curentPage;
        public int CurentPage
        {
            get
            {
                return curentPage;
            }
            set { SetProperty(ref curentPage, value); }
        }

        private int totalPage;
        public int TotalPage
        {
            get
            {
                return totalPage;
            }
            set { SetProperty(ref totalPage, value); }
        }

        private List<int> pageNumbers;
        public List<int> PageNumbers
        {
            get
            {
                return pageNumbers;
            }
            set { SetProperty(ref pageNumbers, value); }
        }

        private List<int> trashPageNumbers;
        public List<int> TrashPageNumbers
        {
            get
            {
                return trashPageNumbers;
            }
            set { SetProperty(ref trashPageNumbers, value); }
        }


        private bool isFirstButtonEnable;
        public bool IsFirstButtonEnable
        {
            get
            {
                return isFirstButtonEnable;
            }
            set { SetProperty(ref isFirstButtonEnable, value); }
        }

        private bool isPreviousButtonEnable;
        public bool IsPreviousButtonEnable
        {
            get
            {
                return isPreviousButtonEnable;
            }
            set { SetProperty(ref isPreviousButtonEnable, value); }
        }

        private bool isNextButtonEnable;
        public bool IsNextButtonEnable
        {
            get
            {
                return isNextButtonEnable;
            }
            set { SetProperty(ref isNextButtonEnable, value); }
        }

        private bool isLastButtonEnable;
        public bool IsLastButtonEnable
        {
            get
            {
                return isLastButtonEnable;
            }
            set { SetProperty(ref isLastButtonEnable, value); }
        }

        private FolderElement selectedItem;
        public FolderElement SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set { SetProperty(ref selectedItem, value); }
        }
        
        private ObservableCollection<FolderElement> folderList;
        public ObservableCollection<FolderElement> FolderList
        {
            get
            {
                return folderList;
            }
            set { SetProperty(ref folderList, value); }
        }
        private ObservableCollection<FolderElement> trashFolderList;
        public ObservableCollection<FolderElement> TrashFolderList
        {
            get
            {
                return trashFolderList;
            }
            set { SetProperty(ref trashFolderList, value); }
        }
        private ObservableCollection<FolderElement> subFolderList;
        public ObservableCollection<FolderElement> SubFolderList
        {
            get
            {
                return subFolderList;
            }
            set { SetProperty(ref subFolderList, value); }
        }
        private int editedFolderId;
        public int EditedFolderId
        {
            get { return editedFolderId; }
            set
            {
                editedFolderId = value;
                SetProperty(ref editedFolderId, value);
            }
        }

        private string folderActionType;
        public string FolderActionType
        {
            get { return folderActionType; }
            set
            {
                folderActionType = value;
                SetProperty(ref folderActionType, value);
            }
        }

        private string folderName;
        public string FolderName
        {
            get { return folderName; }
            set
            {
                folderName = value;
                this.NotifyPropertyChanged("FolderName");
            }
        }
        private string folderPassword;
        public string FolderPassword
        {
            get { return folderPassword; }
            set
            {
                folderPassword = value;
                this.NotifyPropertyChanged("FolderPassword");
            }
        }

        private int enablePassword;
        public int EnablePassword
        {
            get { return enablePassword; }
            set
            {
                SetProperty(ref enablePassword, value);
            }
        }

        private ObservableCollection<FolderElement> folderListForComboBox;
        public ObservableCollection<FolderElement> FolderListForComboBox
        {
            get
            {
                return folderListForComboBox;
            }
            set { SetProperty(ref folderListForComboBox, value); }
        }

        private ObservableCollection<FolderElement> folderListForFileUpload;
        public ObservableCollection<FolderElement> FolderListForFileUpload
        {
            get
            {
                return folderListForFileUpload;
            }
            set { SetProperty(ref folderListForFileUpload, value); }
        }

        private int parentFolderId;
        public int ParentFolderId
        {
            get { return parentFolderId; }
            set 
            {
                parentFolderId = value;
                this.NotifyPropertyChanged("ParentFolderId");
            }
        }
        public List<FolderPrivacyCombo> ValuesFolderPrivacy { get; }
        private int _selectedValueFolderPrivacy;
        public int SelectedValueFolderPrivacy
        {
            get { return _selectedValueFolderPrivacy; }
            set
            {
                SetProperty(ref _selectedValueFolderPrivacy, value);
            }
        }

        private string _publicUrl;
        public string PublicUrl
        {
            get { return _publicUrl; }
            set
            {
                _publicUrl = value;
                this.NotifyPropertyChanged("PublicUrl");
            }
        }

        private List<WaterMarkPreview> waterMarkPreviews;
        public List<WaterMarkPreview> WaterMarkPreviews
        {
            get { return waterMarkPreviews; }
            set { SetProperty(ref waterMarkPreviews, value); }
        }
        private int isWatermarkPreviews;
        public int IsWatermarkPreviews
        {
            get { return isWatermarkPreviews; }
            set { SetProperty(ref isWatermarkPreviews, value); }
        }

        private List<AllowDownloading> allowDownloadings;
        public List<AllowDownloading> AllowDownloadings
        {
            get { return allowDownloadings; }
            set { SetProperty(ref allowDownloadings, value); }
        }
        private int isShowDownloadLinks;
        public int IsShowDownloadLinks
        {
            get { return isShowDownloadLinks; }
            set { SetProperty(ref isShowDownloadLinks, value); }
        }

        private int isPublic;
        public int IsPublic
        {
            get { return isPublic; }
            set { SetProperty(ref isPublic, value); }
        }

        private FolderCreationRequest folderCreationRequest;
        public FolderCreationRequest FolderCreationRequest
        {
            get
            {
                return folderCreationRequest;
            }
            set { SetProperty(ref folderCreationRequest, value); }
        }
        private bool isRootFolderSelected;
        public bool IsRootFolderSelected
        {
            get { return isRootFolderSelected; }
            set
            {
                isRootFolderSelected = value;
                this.NotifyPropertyChanged("IsRootFolderSelected");
            }
        }
        private bool isChildFolderSelected;
        public bool IsChildFolderSelected
        {
            get { return isChildFolderSelected; }
            set
            {
                isChildFolderSelected = value;
                this.NotifyPropertyChanged("IsChildFolderSelected");
            }
        }
        private string folderCountMsg;
        public string FolderCountMsg
        {
            get { return folderCountMsg; }
            set
            {
                folderCountMsg = value;
                this.NotifyPropertyChanged("FolderCountMsg");
            }
        }
        private string trashFolderCountMsg;
        public string TrashFolderCountMsg
        {
            get { return trashFolderCountMsg; }
            set { SetProperty(ref trashFolderCountMsg, value); }
        }
        private int selectedFolderId;
        public int SelectedFolderId
        {
            get { return selectedFolderId; }
            set { SetProperty(ref selectedFolderId, value); }
        }
        private int previousSelectedFolderId;
        public int PreviousSelectedFolderId
        {
            get { return previousSelectedFolderId; }
            set { SetProperty(ref previousSelectedFolderId, value); }
        }
        private bool isOptionsVisible;
        public bool IsOptionsVisible
        {
            get { return isOptionsVisible; }
            set { SetProperty(ref isOptionsVisible, value); }
        }
        public FolderViewModel()
        {
            FolderCreationRequest = new FolderCreationRequest();

            ValuesFolderPrivacy = new List<FolderPrivacyCombo>();
            ValuesFolderPrivacy.Add(new FolderPrivacyCombo("Public Limited - access only if users know the sharing link.", 1));
            ValuesFolderPrivacy.Add(new FolderPrivacyCombo("Private, no access outside of your account.", 0));
            SelectedValueFolderPrivacy = 1;

            WaterMarkPreviews = new List<WaterMarkPreview>();
            WaterMarkPreviews.Add(new WaterMarkPreview("Yes", 1));
            WaterMarkPreviews.Add(new WaterMarkPreview("No", 0));
            IsWatermarkPreviews = 0;

            AllowDownloadings = new List<AllowDownloading>();
            AllowDownloadings.Add(new AllowDownloading("Yes", 1));
            AllowDownloadings.Add(new AllowDownloading("No", 0));
            IsShowDownloadLinks = 1;
            EditedFolderId = 0;
            EnablePassword = 1;
            CurentPage = 1;
            TrashCurentPage = 1;          
            IsFirstButtonEnable = false;
            IsPreviousButtonEnable = false;
            IsNextButtonEnable = false;
            IsLastButtonEnable = false;
            IsTrashFirstButtonEnable = false;
            IsTrashPreviousButtonEnable = false;
            IsTrashNextButtonEnable = false;
            IsTrashLastButtonEnable = false;
            AddFolderCommand = new DelegateCommand(() =>
            {              
                Logger.Info("Creating folder with name " + FolderName);
                try
                {
                    AddFolder(createFolderEndPoint, FolderName, ParentFolderId, SelectedValueFolderPrivacy,  FolderPassword, IsWatermarkPreviews, IsShowDownloadLinks);
                    PublicUrl = null;
                    Logger.Info("Folder created successfully with name " + FolderName);
                    MainViewModel.Instance.ToastViewModel.ShowSuccess("Folder: "+ FolderName+" is created successfully");
                }
                catch(Exception e)
                {
                    Logger.Error("Error while creating folder with name " + FolderName + ","+e.Message.ToString());
                }                          
            });           

            EditFolderCommand = new DelegateCommand(() =>
            {
                Logger.Info("Editing folder with name " + FolderName);
                try
                {
                    EditFolder(editFolderEndPoint, EditedFolderId, FolderName, ParentFolderId, SelectedValueFolderPrivacy, FolderPassword, IsWatermarkPreviews, IsShowDownloadLinks);
                    Logger.Info("Folder edited successfully with name " + FolderName);
                    MainViewModel.Instance.ToastViewModel.ShowSuccess("Folder: " + FolderName + " is updated successfully");
                }
                catch (Exception e)
                {
                    Logger.Error("Error while editing folder with name " + FolderName + "," + e.Message.ToString());
                }
            });

            DeleteFolderCommand = new DelegateCommand(() =>
            {
                Logger.Info("Deleting folder with name " + FolderName);
                try
                {
                    DeleteFolder(deleteFolderEndPoint, EditedFolderId);
                    Logger.Info("Folder deleted successfully with name " + FolderName);
                }
                catch (Exception e)
                {
                    Logger.Error("Error while deleting folder with name " + FolderName + "," + e.Message.ToString());                    
                }
            });
            FolderNavigationCommand = new DelegateCommand(() => {
                string apiResponse = getAccessToken();
                if (apiResponse.Equals("error"))
                {
                    Logger.Error("Could not validate access_token and account_id during GetFolderList call");
                    MainViewModel.Instance.ToastViewModel.ShowError("Could not validate access_token and account_id");
                }
                var cloudAPIFolderObj = new IGMICloudFolderAPIs();
                string response = cloudAPIFolderObj.GetFolderList(getFolderDetailsEndPoint, LoggedinProfile.accessToken, SelectedItem.Id, "active", CurentPage);
                Folder folder = JsonConvert.DeserializeObject<Folder>(response);
                SubFolderList = new ObservableCollection<FolderElement>();              
                if (folder != null && folder.Data != null)
                {
                    TotalPage = Convert.ToInt32(folder.TotalPage);
                    foreach (FolderElement folderElement in folder.Data.Folders)
                    {                       
                        SubFolderList.Add(folderElement);                        
                    }
                }
                FolderCountMsg = SelectedItem.FolderName;                
                MainViewModel.Instance.LoginViewModel.SwitchView = SwitchViewEnum.FolderManagement;
            });
            ShowAndHideOptionsCommand = new DelegateCommand(() => {
                IsOptionsVisible = !IsOptionsVisible;
            });
        }

        public string getAccessToken()
        {
            Logger.Info("Fetching access token ");
            var cloudAPIObj = new IGMICloudAPIs();
            string apiResponse = "error";
            string loginResponse = cloudAPIObj.DoLogin(loginEndpoint, LoggedinProfile.userName, LoggedinProfile.password);
            if (loginResponse != null)
            {
                var responseJson = SimpleJson.DeserializeObject(loginResponse);
                if (responseJson is JsonObject jObj)
                {
                    string responseStatus = (string)jObj["_status"];                  
                    Logger.Debug("response  status: " + responseStatus);
                    if (responseStatus == "success")
                    {
                        LoggedinProfile.accessToken = (string)((JsonObject)jObj["data"])[0];
                        LoggedinProfile.accountId = Int16.Parse((string)((JsonObject)jObj["data"])[1]);
                        apiResponse="success";
                        Logger.Info("Fetched access token successfully");
                    }
                    else
                    {
                        apiResponse="error";
                        Logger.Info("Could not able to fetched access token");
                    }
                }
                
            }
            return apiResponse;
        }

        public ObservableCollection<FolderElement> GetFolderList(int folder_id, int parent_folder_id, string direction, int pageNo)
        {            
            string apiResponse=getAccessToken();
            if (apiResponse.Equals("error"))
            {
                Logger.Error("Could not validate access_token and account_id during GetFolderList call");
                MainViewModel.Instance.ToastViewModel.ShowError("Could not validate access_token and account_id");
                return null;
            }
            if (pageNo == 0)
            {
                if (direction.Trim() == "")
                {
                    CurentPage = 1;
                }
                else if (direction.Trim().Equals("next"))
                {
                    CurentPage = CurentPage + 1;
                }
                else if (direction.Trim().Equals("previous"))
                {
                    CurentPage = CurentPage - 1;
                }
                else if (direction.Trim().Equals("first"))
                {
                    CurentPage = 1;
                }
                else if (direction.Trim().Equals("last"))
                {
                    CurentPage = TotalPage;
                }
            }
            else
            {
                CurentPage = pageNo;
            }
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.GetFolderList(getFolderDetailsEndPoint, LoggedinProfile.accessToken, parent_folder_id, "active", CurentPage);
            
            Folder folder = JsonConvert.DeserializeObject<Folder>(response);           
            FolderList = new ObservableCollection<FolderElement>();
            FolderListForComboBox = new ObservableCollection<FolderElement>();
            FolderElement defaultSelected = new FolderElement();
            defaultSelected.FolderName = "-None-";
            defaultSelected.Id = 0;
            FolderListForComboBox.Add(defaultSelected);
            if (folder != null && folder.Data != null)
            {
                TotalPage = Convert.ToInt32(folder.TotalPage);
                populatePageNumbers();
                enableDisableButton();
                foreach (FolderElement folderElement in folder.Data.Folders)
                {                   
                   FolderList.Add(folderElement);                    
                   FolderListForComboBox.Add(folderElement);
                }
            }
            ParentFolderId = 0;
            if (folder_id > 0)
            {
                ParentFolderId = folder_id;
            }
            if (FolderList.Count > 0)
            {
                IsExpanded = true;
            }
            FolderCountMsg = "Root Folder - "+ FolderList.Count+ " Folders";
            return FolderList;
        }

        public void populatePageNumbers()
        {
            if (PageNumbers == null)
            {
                PageNumbers = new List<int>();
            }

            if (PageNumbers!=null && PageNumbers.Count > 0)
            {
                PageNumbers.Clear();
            }

            int i = 1;
            while (i <= TotalPage)
            {
                PageNumbers.Add(i);
                if (i == 5)
                {
                    break;
                }
                i++;
            }
        }
        public void enableDisableButton()
        {           
            if (CurentPage > 1)
            {
                IsFirstButtonEnable = true;
                IsPreviousButtonEnable = true;
            }
            else
            {
                IsFirstButtonEnable = false;
                IsPreviousButtonEnable = false;
            }
            if (CurentPage < TotalPage)
            {
                IsNextButtonEnable = true;
                IsLastButtonEnable = true;
            }
            else
            {
                IsNextButtonEnable = false;
                IsLastButtonEnable = false;
            }
        }       

        public void GetSpecificFolder(int folder_id)
        {            
            string apiResponse = getAccessToken();
            if (apiResponse.Equals("error"))
            {
                Logger.Error("Could not validate access_token and account_id during GetSpecificFolder call");
                MainViewModel.Instance.ToastViewModel.ShowError("Could not validate access_token and account_id");
            }
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.GetSpecificFolder(editFolderEndPoint, LoggedinProfile.accessToken, LoggedinProfile.accountId, folder_id);
            if (response != null)
            {
                EditFolderResponse editFolderResponse = JsonConvert.DeserializeObject<EditFolderResponse>(response);
                if (editFolderResponse.Data != null)
                {
                    EditedFolderId = editFolderResponse.Data.Id;
                    FolderName = editFolderResponse.Data.FolderName;
                    ParentFolderId = editFolderResponse.Data.ParentId == null ? 0 : Int32.Parse(editFolderResponse.Data.ParentId.ToString());
                    SelectedValueFolderPrivacy = editFolderResponse.Data.IsPublic;
                    FolderPassword = editFolderResponse.Data.AccessPassword;
                }
                if (FolderPassword!=null && FolderPassword.ToString().Trim() != "")
                {
                    EnablePassword = 1;
                }
                else
                {
                    EnablePassword = 0;
                }
                PublicUrl = editFolderResponse.Data.url_folder;
                IsWatermarkPreviews = 0;//TODO this field is not available in details api
                IsShowDownloadLinks = 0;//TODO this field is not available in details api
            }
        }

        public void AddFolder(string endpoint, string folder_name, int parent_id, int is_public, string password, int watermarkPreviews, int showDownloadLinks)
        {
            if (EnablePassword == 0)
            {
                password = null;
            }
            string apiResponse = getAccessToken();
            if (apiResponse.Equals("error"))
            {
                Logger.Error("Could not validate access_token and account_id during AddFolder call");
                MainViewModel.Instance.ToastViewModel.ShowError("Could not validate access_token and account_id");
            }
            bool callCreateAPI = true;

            if (string.IsNullOrEmpty(folder_name))
            {
                isFolderNameEmpty = true;
                callCreateAPI = false;
            }
            else
                isFolderNameEmpty = false;

            if (callCreateAPI)
            {
                var cloudAPIFolderObj = new IGMICloudFolderAPIs();
                string response = cloudAPIFolderObj.CreateFolder(createFolderEndPoint, LoggedinProfile.accessToken, LoggedinProfile.accountId, folder_name, parent_id, is_public, password, watermarkPreviews, showDownloadLinks);

                if (response != null)
                {
                    GetFolderList( 0, 0,"", 0);
                }
            }
        }

        public void EditFolder(string endpoint, int folder_id, string folder_name,int parent_id, int is_public, string password, int watermarkPreviews, int showDownloadLinks)
        {
            if (EnablePassword == 0)
            {
                password = null;
            }
            string apiResponse = getAccessToken();
            if (apiResponse.Equals("error"))
            {
                Logger.Error("Could not validate access_token and account_id during EditFolder call");
                MainViewModel.Instance.ToastViewModel.ShowError("Could not validate access_token and account_id");
            }
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.EditFolder(endpoint, LoggedinProfile.accessToken, folder_id, LoggedinProfile.accountId, folder_name, parent_id, is_public, password);

            if (response != null)
            {
                GetFolderList(folder_id, 0,"", 0);
            }
        }

        public void DeleteFolder(string endpoint, int folder_id)
        {
            string apiResponse = getAccessToken();
            if (apiResponse.Equals("error"))
            {
                Logger.Error("Could not validate access_token and account_id during DeleteFolder call");
                MainViewModel.Instance.ToastViewModel.ShowError("Could not validate access_token and account_id");
            }
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.DeleteFolder(endpoint, LoggedinProfile.accessToken, LoggedinProfile.accountId, folder_id);

            if (response != null)
            {
                MainViewModel.Instance.ToastViewModel.ShowSuccess("Removed 1 file.");
                GetFolderList(folder_id, 0, "", 0);
            }
        }
        public void GetTrashFolders(string direction, int pageNo)
        {
            string apiResponse = getAccessToken();
            if (apiResponse.Equals("error"))
            {
                Logger.Error("Could not validate access_token and account_id during GetFolderList call");
                MainViewModel.Instance.ToastViewModel.ShowError("Could not validate access_token and account_id");
            }

            if (pageNo == 0)
            {
                if (direction.Trim() == "")
                {
                    TrashCurentPage = 1;
                }
                else if (direction.Trim().Equals("next"))
                {
                    TrashCurentPage = TrashCurentPage + 1;
                }
                else if (direction.Trim().Equals("previous"))
                {
                    TrashCurentPage = TrashCurentPage - 1;
                }
                else if (direction.Trim().Equals("first"))
                {
                    TrashCurentPage = 1;
                }
                else if (direction.Trim().Equals("last"))
                {
                    TrashCurentPage = TrashTotalPage;
                }
            }
            else
            {
                TrashCurentPage = pageNo;
            }
           
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.GetFolderList(getFolderDetailsEndPoint, LoggedinProfile.accessToken, 0, "Trash", TrashCurentPage);

            Folder folder = JsonConvert.DeserializeObject<Folder>(response);
            TrashFolderList = new ObservableCollection<FolderElement>();
            
            if (folder != null && folder.Data != null)
            {
                TrashTotalPage = Convert.ToInt32(folder.TotalPage);
                populateTrashPageNumbers();
                enableDisableTrashButton();
                foreach (FolderElement folderElement in folder.Data.Folders)
                {                   
                   TrashFolderList.Add(folderElement);                   
                }
            }
            TrashFolderCountMsg = "Trash Can - " + TrashFolderList.Count + " Folders";
        }

        public void populateTrashPageNumbers()
        {
            if (TrashPageNumbers == null)
            {
                TrashPageNumbers = new List<int>();
            }

            if (TrashPageNumbers != null && TrashPageNumbers.Count > 0)
            {
                TrashPageNumbers.Clear();
            }

            int i = 1;
            while (i <= TrashTotalPage)
            {
                TrashPageNumbers.Add(i);
                if (i == 5)
                {
                    break;
                }
                i++;
            }
        }

        public void enableDisableTrashButton()
        {
            if (TrashCurentPage > 1)
            {
                IsTrashFirstButtonEnable = true;
                IsTrashPreviousButtonEnable = true;
            }
            else
            {
                IsTrashFirstButtonEnable = false;
                IsTrashPreviousButtonEnable = false;
            }
            if (TrashCurentPage < TrashTotalPage)
            {
                IsTrashNextButtonEnable = true;
                IsTrashLastButtonEnable = true;
            }
            else
            {
                IsTrashNextButtonEnable = false;
                IsTrashLastButtonEnable = false;
            }
        }

        public ObservableCollection<FolderElement> GetFolderListForFileUpload(int folder_id, int parent_folder_id)
        {
            string apiResponse = getAccessToken();
            if (apiResponse.Equals("error"))
            {
                Logger.Error("Could not validate access_token and account_id during GetFolderList call");
                MainViewModel.Instance.ToastViewModel.ShowError("Could not validate access_token and account_id");
                return null;
            }
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.GetFolderList(getFolderDetailsEndPoint, LoggedinProfile.accessToken, parent_folder_id, "active", 1);

            Folder folder = JsonConvert.DeserializeObject<Folder>(response);
            FolderList = new ObservableCollection<FolderElement>();
            FolderListForFileUpload = new ObservableCollection<FolderElement>();
            FolderElement defaultSelected = new FolderElement();
            defaultSelected.FolderName = "-Default-";
            defaultSelected.Id = 0;
            FolderListForFileUpload.Add(defaultSelected);
            if (folder != null && folder.Data != null)
            {
                foreach (FolderElement folderElement in folder.Data.Folders)
                {                   
                    FolderList.Add(folderElement);                   
                    FolderListForFileUpload.Add(folderElement);
                }
            }
            ParentFolderId = 0;
            if (folder_id > 0)
            {
                ParentFolderId = folder_id;
            }
            if (FolderList.Count > 0)
            {
                IsExpanded = true;
            }
            FolderCountMsg = "Root Folder - " + FolderList.Count + " Folders";
            return FolderList;
        }

        public void UploadFile(string filePath)
        {
            Console.WriteLine("ParentFolderID : " + ParentFolderId.ToString());
            string apiResponse = getAccessToken();
            if (apiResponse.Equals("error"))
            {
                Logger.Error("Could not validate access_token and account_id during EditFolder call");
                MainViewModel.Instance.ToastViewModel.ShowError("Could not validate access_token and account_id");
            }
            var cloudAPIFileObj = new IGMICloudFileAPIs();
            string response = cloudAPIFileObj.UploadFile(uploadFileEndPoint, LoggedinProfile.accessToken, LoggedinProfile.accountId, ParentFolderId, filePath);

            if (response != null)
            {
                MainViewModel.Instance.ToastViewModel.ShowSuccess("Upload Complete");
                GetFolderList(0, ParentFolderId, "", 0);               
            }
        }
    }
}
