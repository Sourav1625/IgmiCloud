using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGMICloudApplication.APIs
{
    class IGMICloudAPIs
    {
        private readonly RestClient _restClient;
        private readonly JsonSerializer _serializer;
        private string baseUrl = "https://igmigroup.com/ucloud/ucloud/api/v2";        
        private string userDetailsEndpoint = "/account/info";
        public IGMICloudAPIs()
        {
            var serviceBaseUri = $"{baseUrl}";
            _restClient = new RestClient(serviceBaseUri);
            _serializer = new JsonSerializer();

        }
        public bool DoLogin(string endpoint)
        {
            var request = new RestRequest($"{endpoint}");
            object dtObj = new
            {
                username = player_count,
                password = serverName
            };
            var bodyJson = _serializer.Serialize(dtObj);
            request.AddJsonBody(bodyJson);
            AddRequestBoilerplate(ref request);

            var response = _restClient.Post(request);
            if (!response.IsSuccessful)
            {
                NotifyRequestFailure(request, response);
                return false;
            }

            return true;
        }
        private static void AddRequestBoilerplate(ref RestRequest baseRequest)
        {
            baseRequest.AddHeader("accept", "application/json");
            baseRequest.AddHeader("User-Agent", "Apheleia/1.0");
        }
    }
}
