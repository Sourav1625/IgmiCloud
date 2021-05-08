using IGMICloudApplication.APIs;
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
   public class FolderViewModel : ViewModelBase
    {
        string getFolderDetailsEndPoint="/folder/listing";
        string createFolderEndPoint = "/folder/create";
        string editFolderEndPoint = "/folder/edit";
        string deleteFolderEndPoint = "/folder/delete";
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
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

        private FolderElement selectedFolder;
        public FolderElement SelectedFolder
        {
            get { return selectedFolder; }
            set { SetProperty(ref selectedFolder, value); }
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
        public FolderViewModel()
        {
            

        }

        public ObservableCollection<FolderElement> GetFolderList(string access_token, int parent_folder_id)
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
            foreach (FolderElement folderElement in folder.Data.Folders)
            {
                FolderList.Add(folderElement);
                FolderListForComboBox.Add(folderElement);
            }
            SelectedFolderId = 0;
            SelectedFolder = FolderList[0];
            return FolderList;
        }

    }
}
