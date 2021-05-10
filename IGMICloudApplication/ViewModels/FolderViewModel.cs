using IGMICloudApplication.APIs;
using IGMICloudApplication.Commands;
using IGMICloudApplication.Models;
using IGMICloudApplication.Models.ApiResponse.EditFolder;
using IGMICloudApplication.Models.ApiResponse.ListFolder;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

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
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public DelegateCommand AddFolderCommand { get; private set; }
        public DelegateCommand EditFolderCommand { get; private set; }
        public DelegateCommand DeleteFolderCommand { get; private set; }

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

        private ObservableCollection<FolderElement> folderList;
        public ObservableCollection<FolderElement> FolderList
        {
            get
            {
                return folderList;
            }
            set { SetProperty(ref folderList, value); }
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
        
        private string folderName;
        public string FolderName
        {
            get { return folderName; }
            set
            {
                folderName = value;
                SetProperty(ref folderName, value);
            }
        }
        private string folderPassword;
        public string FolderPassword
        {
            get { return folderPassword; }
            set
            {
                folderPassword = value;
                SetProperty(ref folderPassword, value);
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
        private int selectedFolderId;
        public int SelectedFolderId
        {
            get { return selectedFolderId; }
            set { SetProperty(ref selectedFolderId, value); }
        }

        private int isWatermarkPreviews;
        public int IsWatermarkPreviews
        {
            get { return isWatermarkPreviews; }
            set { SetProperty(ref isWatermarkPreviews, value); }
        }

        private List<WaterMarkPreview> waterMarkPreviews;
        public List<WaterMarkPreview> WaterMarkPreviews
        {
            get { return waterMarkPreviews; }
            set { SetProperty(ref waterMarkPreviews, value); }
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

        /*private FolderElement selectedFolder;
        public FolderElement SelectedFolder
        {
            get { return selectedFolder; }
            set { SetProperty(ref selectedFolder, value); }
        }*/

        private List<AllowDownloading> allowDownloadings;
        public List<AllowDownloading> AllowDownloadings
        {
            get { return allowDownloadings; }
            set { SetProperty(ref allowDownloadings, value); }
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
        private string folderCountMsg;
        public string FolderCountMsg
        {
            get { return folderCountMsg; }
            set { SetProperty(ref folderCountMsg, value); }
        }
        private Tuple<string> firstValueFolderPrivacy;
        public Tuple<string> FirstValueFolderPrivacy
        {
            get { return firstValueFolderPrivacy; }
            set
            {
                firstValueFolderPrivacy = value;
                SetProperty(ref firstValueFolderPrivacy, value);
            }
        }

        private Tuple<string> secondValueFolderPrivacy;
        public Tuple<string> SecondValueFolderPrivacy
        {
            get { return secondValueFolderPrivacy; }
            set
            {
                secondValueFolderPrivacy = value;
                SetProperty(ref secondValueFolderPrivacy, value);
            }
        }

        private int _selectedValueFolderPrivacy;
        public int SelectedValueFolderPrivacy
        {
            get { return _selectedValueFolderPrivacy; }
            set
            {
                _selectedValueFolderPrivacy = value;
                SetProperty(ref _selectedValueFolderPrivacy, value);
            }
        }
        public List<FolderPrivacyCombo> ValuesFolderPrivacy { get; }
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
            AddFolderCommand = new DelegateCommand(() =>
            {              
                Logger.Info("Creating folder with name " + FolderName);
                try
                {
                    AddFolder(createFolderEndPoint, FolderName, SelectedFolderId, SelectedValueFolderPrivacy,  FolderPassword, IsWatermarkPreviews, IsShowDownloadLinks);
                    Logger.Info("Folder created successfully with name " + FolderName);
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
                    EditFolder(editFolderEndPoint, EditedFolderId, FolderName, SelectedFolderId, SelectedValueFolderPrivacy, FolderPassword, IsWatermarkPreviews, IsShowDownloadLinks);
                    Logger.Info("Folder edited successfully with name " + FolderName);
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
                    DeleteFolder(deleteFolderEndPoint, SelectedFolderId);
                    Logger.Info("Folder deleted successfully with name " + FolderName);
                }
                catch (Exception e)
                {
                    Logger.Error("Error while deleting folder with name " + FolderName + "," + e.Message.ToString());                    
                }
            });

        }

        public ObservableCollection<FolderElement> GetFolderList(int folder_id, int parent_folder_id)
        {           
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.GetFolderList(getFolderDetailsEndPoint, LoggedinProfile.accessToken, parent_folder_id);

            Folder folder = JsonConvert.DeserializeObject<Folder>(response);           
            FolderList = new ObservableCollection<FolderElement>();
            FolderListForComboBox = new ObservableCollection<FolderElement>();
            FolderElement defaultSelected = new FolderElement();
            defaultSelected.FolderName = "-None-";
            defaultSelected.Id = 0;
            FolderListForComboBox.Add(defaultSelected);
            if (folder != null && folder.Data != null)
            {
                foreach (FolderElement folderElement in folder.Data.Folders)
                {
                    if (folderElement.ParentId == null)
                    {
                        FolderList.Add(folderElement);
                    }
                    FolderListForComboBox.Add(folderElement);
                }
            }
            SelectedFolderId = 0;
            if (folder_id > 0)
            {
                SelectedFolderId = folder_id;
            }
            FolderCountMsg = "Root Folder - "+ FolderList.Count+ " Folders";
            return FolderList;
        }

        public void GetSpecificFolder(int folder_id)
        {
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.GetSpecificFolder(editFolderEndPoint, LoggedinProfile.accessToken, LoggedinProfile.accountId, folder_id);
            if (response != null)
            {
                EditFolderResponse editFolderResponse = JsonConvert.DeserializeObject<EditFolderResponse>(response);
                EditedFolderId = editFolderResponse.Data.Id;
                FolderName = editFolderResponse.Data.FolderName;
                SelectedFolderId = editFolderResponse.Data.ParentId == null ? 0 : Int32.Parse(editFolderResponse.Data.ParentId.ToString());
                SelectedValueFolderPrivacy = editFolderResponse.Data.IsPublic;                
                FolderPassword = editFolderResponse.Data.AccessPassword;
                IsWatermarkPreviews = 0;//TODO this field is not available in details api
                IsShowDownloadLinks = 0;//TODO this field is not available in details api
            }
        }

        public void AddFolder(string endpoint, string folder_name, int parent_id, int is_public, string password, int watermarkPreviews, int showDownloadLinks)
        {
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
                    GetFolderList( 0, 0);
                }
            }
            //Folder folder = JsonConvert.DeserializeObject<Folder>(response);
            ////FolderList = new ObservableCollection<FolderElement>();
            //FolderElement defaultSelected = new FolderElement();
            //defaultSelected.FolderName = "-None-";
            //defaultSelected.Id = 0;
            //FolderList.Add(defaultSelected);
            //foreach (FolderElement folderElement in folder.Data.Folders)
            //{
            //    FolderList.Add(folderElement);
            //}
            //SelectedFolderId = 0;
            //SelectedFolder = FolderList[0];
            //return FolderList;
        }

        public void EditFolder(string endpoint, int folder_id, string folder_name,int parent_id, int is_public, string password, int watermarkPreviews, int showDownloadLinks)
        {            
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.EditFolder(endpoint, LoggedinProfile.accessToken, folder_id, LoggedinProfile.accountId, folder_name, parent_id, is_public, password);

            if (response != null)
            {
                GetFolderList(folder_id, 0);
            }

            //var folder = JsonConvert.DeserializeObject<Folder>(response.Content);

            //return folder;

        }

        public void DeleteFolder(string endpoint, int folder_id)
        {
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.DeleteFolder(endpoint, LoggedinProfile.accessToken, LoggedinProfile.accountId, folder_id);

            if (response != null)
            {
                GetFolderList(folder_id, 0);
            }

            //var folder = JsonConvert.DeserializeObject<Folder>(response.Content);

            //return folder;

        }
    }
}
