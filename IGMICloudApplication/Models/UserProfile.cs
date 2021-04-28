using Newtonsoft.Json;
namespace IGMICloudApplication.Models
{    
    public partial class UserProfile
    {
        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("_status")]
        public string Status { get; set; }

        [JsonProperty("_datetime")]
        public System.DateTimeOffset Datetime { get; set; }
    }

    public partial class Data
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("level_id")]
        public long LevelId { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("lastlogindate")]
        public System.DateTimeOffset Lastlogindate { get; set; }

        [JsonProperty("lastloginip")]
        public string Lastloginip { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("lastname")]
        public string Lastname { get; set; }

        [JsonProperty("languageId")]
        public object LanguageId { get; set; }

        [JsonProperty("datecreated")]
        public object Datecreated { get; set; }

        [JsonProperty("lastPayment")]
        public object LastPayment { get; set; }

        [JsonProperty("paidExpiryDate")]
        public string PaidExpiryDate { get; set; }

        [JsonProperty("storageLimitOverride")]
        public object StorageLimitOverride { get; set; }
    }
}
