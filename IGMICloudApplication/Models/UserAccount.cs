using System;
using Newtonsoft.Json;


namespace IGMICloudApplication.Models
{
    public partial class UserAccount
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
        //[JsonProperty("id")]      
        //public long Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("max_upload_size")]
        public string MaxUploadSize { get; set; }

        [JsonProperty("can_upload")]
      
        public long CanUpload { get; set; }

        [JsonProperty("wait_between_downloads")]
      
        public long WaitBetweenDownloads { get; set; }

        [JsonProperty("download_speed")]
      
        public long DownloadSpeed { get; set; }

        [JsonProperty("max_storage_bytes")]
      
        public long MaxStorageBytes { get; set; }

        [JsonProperty("show_site_adverts")]
      
        public long ShowSiteAdverts { get; set; }

        [JsonProperty("show_upgrade_screen")]
      
        public long ShowUpgradeScreen { get; set; }

        [JsonProperty("days_to_keep_inactive_files")]
      
        public long DaysToKeepInactiveFiles { get; set; }

        [JsonProperty("concurrent_uploads")]
      
        public long ConcurrentUploads { get; set; }

        [JsonProperty("concurrent_downloads")]
      
        public long ConcurrentDownloads { get; set; }

        [JsonProperty("downloads_per_24_hours")]
      
        public long DownloadsPer24_Hours { get; set; }

        [JsonProperty("max_download_filesize_allowed")]
      
        public long MaxDownloadFilesizeAllowed { get; set; }

        [JsonProperty("max_remote_download_urls")]
      
        public long MaxRemoteDownloadUrls { get; set; }

        [JsonProperty("level_type")]
        public string LevelType { get; set; }

        [JsonProperty("on_upgrade_page")]
      
        public long OnUpgradePage { get; set; }
    }
}
