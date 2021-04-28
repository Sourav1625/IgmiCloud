using System;
using Newtonsoft.Json;
namespace IGMICloudApplication.Models
{    
    public partial class UserAuth
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
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("account_id")]       
        public long AccountId { get; set; }
    }
}
