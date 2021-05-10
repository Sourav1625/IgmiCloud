using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGMICloudApplication.Models
{
    class EditUserResponse
    {
        public string Response { get; set; }
        public Data Data { get; set; }
        public string Status { get; set; }
        public DateTimeOffset Datetime { get; set; }
    }

    public partial class Data
    {
        public long Id { get; set; }
        public object ParentId { get; set; }
        public string FolderName { get; set; }
        public long TotalSize { get; set; }
        public long IsPublic { get; set; }
        public object AccessPassword { get; set; }
        public string Status { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public object DateUpdated { get; set; }
        public Uri UrlFolder { get; set; }
        public long TotalDownloads { get; set; }
        public long ChildFolderCount { get; set; }
        public long FileCount { get; set; }
    }
}
