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

        public FolderViewModel()
        {
            

        }

        public ObservableCollection<FolderElement> GetFolderList(string access_token, int parent_folder_id)
        {           
            var cloudAPIFolderObj = new IGMICloudFolderAPIs();
            string response = cloudAPIFolderObj.GetFolderList(getFolderDetailsEndPoint, LoggedinProfile.accessToken, parent_folder_id);

            Folder folder = JsonConvert.DeserializeObject<Folder>(response);           
            FolderList = new ObservableCollection<FolderElement>();
            foreach (FolderElement folderElement in folder.Data.Folders)
            {

                FolderList.Add(folderElement);
            }
           
            return FolderList;
        }

    }
}
