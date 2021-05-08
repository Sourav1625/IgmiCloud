using IGMICloudApplication.APIs;
using IGMICloudApplication.Commands;
using IGMICloudApplication.Models;
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
    public enum WaterMarkPreview
    {
        Yes,
        No
    }
    public enum AllowDownloading
    {
        Yes,
        No
    }
     
    public class FolderViewModel : ViewModelBase
    {
        string getFolderDetailsEndPoint="/folder/listing";
        string createFolderEndPoint = "/folder/create";
        string editFolderEndPoint = "/folder/edit";
        string deleteFolderEndPoint = "/folder/delete";
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public DelegateCommand AddFolderCommand { get; private set; }

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

        private FolderElement selectedFolder;
        public FolderElement SelectedFolder
        {
            get { return selectedFolder; }
            set { SetProperty(ref selectedFolder, value); }
        }

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
        public IEnumerable<Tuple<string>> ValuesFolderPrivacy { get; }
        public FolderViewModel()
        {
            WaterMarkPreviews = Enum.GetValues(typeof(WaterMarkPreview)).OfType<WaterMarkPreview>().ToList();
            AllowDownloadings = Enum.GetValues(typeof(AllowDownloading)).OfType<AllowDownloading>().ToList();            
            FolderCreationRequest = new FolderCreationRequest();

            FirstValueFolderPrivacy = new Tuple<string>("Public Limited - access only if users know the sharing link.");
            SecondValueFolderPrivacy = new Tuple<string>("Private, no access outside of your account.");            
            ValuesFolderPrivacy = new Tuple<string>[2] { FirstValueFolderPrivacy, SecondValueFolderPrivacy };

            AddFolderCommand = new DelegateCommand(() =>
            {              
                Console.WriteLine("Invoking add folder command " + FolderCreationRequest.folder_name + " " + SelectedValueFolderPrivacy + " " + SelectedFolderId + " " + FolderCreationRequest.password + " " + IsWatermarkPreviews + " " + IsShowDownloadLinks);
                
                Logger.Info("Creating folder with name " + FolderCreationRequest.folder_name);
                try
                {
                    AddFolder(createFolderEndPoint, FolderCreationRequest.folder_name, SelectedFolderId, SelectedValueFolderPrivacy, 0, FolderCreationRequest.password, IsWatermarkPreviews, IsShowDownloadLinks);
                    Logger.Info("Folder created successfully with name " + FolderCreationRequest.folder_name);
                    Console.WriteLine("Folder created successfully with name " + FolderCreationRequest.folder_name);
                }
                catch(Exception e)
                {
                    Logger.Error("Error while creating folder with name " + FolderCreationRequest.folder_name + ","+e.Message.ToString());
                    Console.WriteLine("Error while creating folder with name " + FolderCreationRequest.folder_name + ","+e.Message.ToString());
                }
                //if (string.IsNullOrEmpty(userName))
                //{
                //    Console.WriteLine("Username is empty");
                //    Logger.Info("Can not Login as Username is empty");
                //}              
            });

        }

        public ObservableCollection<FolderElement> GetFolderList(string access_token, int parent_folder_id)
        {           
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.GetFolderList(getFolderDetailsEndPoint, LoggedinProfile.accessToken, parent_folder_id);

            Folder folder = JsonConvert.DeserializeObject<Folder>(response);           
            FolderList = new ObservableCollection<FolderElement>();
            FolderElement defaultSelected = new FolderElement();
            defaultSelected.FolderName = "-None-";
            defaultSelected.Id = 0;
            FolderList.Add(defaultSelected);
            if (folder != null && folder.Data != null)
            {
                foreach (FolderElement folderElement in folder.Data.Folders)
                {
                    FolderList.Add(folderElement);
                }
            }
            SelectedFolderId = 0;
            SelectedFolder = FolderList[0];
            return FolderList;
        }

        public void AddFolder(string endpoint, string folder_name, int parent_id, int is_public, int enablePassword, string password, int watermarkPreviews, int showDownloadLinks)
        {
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();            
            string response = cloudAPIFolderObj.CreateFolder(createFolderEndPoint, LoggedinProfile.accessToken, LoggedinProfile.accountId, folder_name, parent_id, is_public, enablePassword, password, watermarkPreviews, showDownloadLinks);

            if (response != null)
            {
                GetFolderList(LoggedinProfile.accessToken, LoggedinProfile.accountId);
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
    }
}
