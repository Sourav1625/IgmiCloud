﻿using System;
using System.Collections.Generic;
using IGMICloudApplication.ViewModels;
using Newtonsoft.Json;
namespace IGMICloudApplication.Models.ApiResponse.ListFolder
{
    public partial class Folder
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("_status")]
        public string Status { get; set; }

        [JsonProperty("_datetime")]
        public DateTimeOffset Datetime { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("folders")]
        public List<FolderElement> Folders { get; set; }

        [JsonProperty("files")]
        public List<object> Files { get; set; }
    }

    public class FolderElement
    {
        [JsonProperty("id")]        
        public int Id { get; set; }

        [JsonProperty("parentId")]
        public object ParentId { get; set; }

        [JsonProperty("folderName")]
        public string FolderName { get; set; }

        [JsonProperty("totalSize")]        
        public long TotalSize { get; set; }

        [JsonProperty("isPublic")]        
        public int IsPublic { get; set; }

        [JsonProperty("date_added")]
        public DateTime? DateAdded { get; set; }

        [JsonProperty("date_updated")]
        public object DateUpdated { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("urlHash")]
        public object UrlHash { get; set; }

        [JsonProperty("url_folder")]
        public Uri UrlFolder { get; set; }

        [JsonProperty("total_downloads")]
        public long TotalDownloads { get; set; }

        [JsonProperty("child_folder_count")]
        public long ChildFolderCount { get; set; }

        [JsonProperty("file_count")]
        public long FileCount { get; set; }
    }
    public class FolderCreationRequest
    {
        public string access_token { get; set; }
        public int account_id { get; set; }
        public string folder_name { get; set; }
        public int parent_id { get; set; }
        public int isPublic { get; set; }
        public string password { get; set; }
        public int watermarkPreviews { get; set; }
        public int showDownloadLinks { get; set; }
    }
}
