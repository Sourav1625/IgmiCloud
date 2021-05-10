using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGMICloudApplication.Models.ApiResponse.EditFolder
{
    class EditFolderResponse
    {
        public string Response { get; set; }
        public Data Data { get; set; }
        public string Status { get; set; }
        public DateTimeOffset Datetime { get; set; }
    }

    public partial class Data
    {
        public int  Id { get; set; }
        public Object ParentId { get; set; }        
        public string FolderName { get; set; }
        public long TotalSize { get; set; }
        public int IsPublic { get; set; }
        public string AccessPassword { get; set; }
        public string Status { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public object DateUpdated { get; set; }
        public string url_folder { get; set; }
        public long TotalDownloads { get; set; }
        public long ChildFolderCount { get; set; }
        public long FileCount { get; set; }
    }
}
